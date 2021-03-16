using Autofac;
using OpenBots.Core.Command;
using OpenBots.Core.Enums;
using OpenBots.Core.Project;
using OpenBots.Core.Script;
using OpenBots.Core.Settings;
using OpenBots.Core.UI.Controls;
using OpenBots.Studio.Utilities;
using OpenBots.UI.CustomControls.Controls;
using OpenBots.UI.CustomControls.CustomUIControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using IContainer = Autofac.IContainer;

namespace OpenBots.UI.Forms.Sequence_Forms
{
    public partial class frmSequence : Form
    {
        #region Instance Variables
        //engine context variables
        private List<ListViewItem> _rowsSelectedForCopy;
        public List<ScriptVariable> ScriptVariables { get; set; }
        public List<ScriptArgument> ScriptArguments { get; set; }
        public List<ScriptElement> ScriptElements { get; set; }
        public Project ScriptProject { get; set; }
        public string ScriptProjectPath { get; set; }       

        //notification variables
        private List<Tuple<string, Color>> _notificationList = new List<Tuple<string, Color>>();
        private DateTime _notificationExpires;
        private bool _isDisplaying;
        private string _notificationText;
        private Color _notificationColor;
        private string _notificationPaintedText;             

        //command search variables
        private TreeView _tvCommandsCopy;
        private string _txtCommandWatermark = "Type Here to Search";       

        //package manager variables
        public IContainer AContainer { get; set; }
        private Dictionary<string, List<Type>> _groupedTypes { get; set; }

        //variable/argument tab variables
        private List<string> _existingVarArgSearchList;
        private string _preEditVarArgName;
        private Type _preEditVarArgType;
        public TypeContext TypeContext { get; set; }

        //other scriptbuilder form variables 
        public string HTMLElementRecorderURL { get; set; }
        private List<AutomationCommand> _automationCommands;
        private ImageList _uiImages;
        private ApplicationSettings _appSettings;
        private DateTime _lastAntiIdleEvent;
        public UIListView SelectedTabScriptActions { get; set; }
        public List<ScriptCommand> MoveToParentCommands { get; set; } = new List<ScriptCommand>();
        #endregion

        #region Form Events
        public frmSequence()
        {
            SelectedTabScriptActions = NewLstScriptActions();
            InitializeComponent();

            //rendering for variable/argument tabs
            dgvVariables.AutoGenerateColumns = false;
            dgvArguments.AutoGenerateColumns = false;

            //vertical control splitter default location
            splitContainerScript.SplitterDistance = (int)(splitContainerScript.Size.Height * 0.7);
            //horizontal control splitter default location
            splitContainerStudioControls.SplitterDistance = (int)(splitContainerStudioControls.Size.Width * 0.2);

            direction.DataSource = Enum.GetValues(typeof(ScriptArgumentDirection));
        }

        private void frmSequence_Load(object sender, EventArgs e)
        {
            var defaultTypesBinding = new BindingSource(TypeContext.DefaultTypes, null);

            VariableType.DataSource = defaultTypesBinding;
            VariableType.DisplayMember = "Key";
            VariableType.ValueMember = "Value";

            ArgumentType.DataSource = defaultTypesBinding;
            ArgumentType.DisplayMember = "Key";
            ArgumentType.ValueMember = "Value";

            //set controls double buffered
            foreach (Control control in Controls)
            {
                typeof(Control).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null, control, new object[] { true });
            }

            //get app settings
            _appSettings = new ApplicationSettings();
            _appSettings = _appSettings.GetOrCreateApplicationSettings();           

            //no height for status bar
            HideNotificationRow();

            //set listview column size
            frmSequence_SizeChanged(null, null);
        }

        public void LoadCommands()
        {
            //load all commands           
            _automationCommands = TypeMethods.GenerateAutomationCommands(AContainer).Where(x => x.Command.CommandName != "SequenceCommand").ToList();

            //instantiate and populate display icons for commands
            _uiImages = UIImage.UIImageList(_automationCommands);

            var groupedCommands = _automationCommands.GroupBy(f => f.DisplayGroup);

            tvCommands.Nodes.Clear();
            foreach (var cmd in groupedCommands)
            {
                TreeNode newGroup = new TreeNode(cmd.Key);

                foreach (var subcmd in cmd)
                {
                    TreeNode subNode = new TreeNode(subcmd.ShortName);
                    subNode.ToolTipText = subcmd.Description;
                    newGroup.Nodes.Add(subNode);
                }

                tvCommands.Nodes.Add(newGroup);
            }

            tvCommands.Sort();

            _tvCommandsCopy = new TreeView();
            _tvCommandsCopy.ShowNodeToolTips = true;
            CopyTreeView(tvCommands, _tvCommandsCopy);
            txtCommandSearch.Text = _txtCommandWatermark;
        }

        private void frmSequence_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;

            if (uiScriptTabControl.TabPages[0].Text.Contains(" *") && DialogResult == DialogResult.Cancel)
            {
                result = MessageBox.Show($"Would you like to save the sequence before closing?",
                                         $"Save Sequence", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                    DialogResult = DialogResult.OK;
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;

                return;
            }
        }

        private void frmSequence_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyTray.Dispose();
        }

        private void frmSequence_SizeChanged(object sender, EventArgs e)
        {
            SelectedTabScriptActions.Columns[2].Width = Width - 340;
        }

        private void frmSequence_Resize(object sender, EventArgs e)
        {
            //check when minimized
            if ((WindowState == FormWindowState.Minimized) && (_appSettings.ClientSettings.MinimizeToTray))
            {
                _appSettings = new ApplicationSettings().GetOrCreateApplicationSettings();
                if (_appSettings.ClientSettings.MinimizeToTray)
                {
                    notifyTray.Visible = true;
                    notifyTray.ShowBalloonTip(3000);
                    ShowInTaskbar = false;
                }
            }
        }
        #endregion

        #region Bottom Notification Panel
        private void tmrNotify_Tick(object sender, EventArgs e)
        {
            if (_appSettings == null)
            {
                return;
            }

            if ((_notificationExpires < DateTime.Now) && (_isDisplaying))
            {
                HideNotification();
            }

            if ((_appSettings.ClientSettings.AntiIdleWhileOpen) && (DateTime.Now > _lastAntiIdleEvent.AddMinutes(1)))
            {
                PerformAntiIdle();
            }

            //check if notification is required
            if ((_notificationList.Count > 0) && (_notificationExpires < DateTime.Now))
            {
                var itemToDisplay = _notificationList[0];
                _notificationList.RemoveAt(0);
                _notificationExpires = DateTime.Now.AddSeconds(2);
                ShowNotification(itemToDisplay.Item1, itemToDisplay.Item2);
            }
        }

        public void Notify(string notificationText, Color notificationColor)
        {
            _notificationList.Add(new Tuple<string, Color>(notificationText, notificationColor));
        }

        private void ShowNotification(string textToDisplay, Color textColor)
        {
            _notificationText = textToDisplay;
            _notificationColor = textColor;

            pnlStatus.SuspendLayout();

            ShowNotificationRow();
            pnlStatus.ResumeLayout();
            _isDisplaying = true;
        }

        private void HideNotification()
        {
            pnlStatus.SuspendLayout();

            HideNotificationRow();
            pnlStatus.ResumeLayout();
            _isDisplaying = false;
        }

        private void HideNotificationRow()
        {
            tlpControls.RowStyles[3].Height = 0;
        }

        private void ShowNotificationRow()
        {
            tlpControls.RowStyles[3].Height = 30;
        }

        private void PerformAntiIdle()
        {
            _lastAntiIdleEvent = DateTime.Now;
            Notify("Anti-Idle Triggered", Color.White);
        }

        private void notifyTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_appSettings.ClientSettings.MinimizeToTray)
            {
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
                notifyTray.Visible = false;
            }
        }
        #endregion

        #region Create Command Logic
        private void AddNewCommand(string specificCommand = "")
        {
            //bring up new command configuration form
            frmCommandEditor newCommandForm = new frmCommandEditor(_automationCommands, GetConfiguredCommands(), TypeContext)
            {
                CreationModeInstance = CreationMode.Add
            };

            if (specificCommand != "")
                newCommandForm.DefaultStartupCommand = specificCommand;

            newCommandForm.ScriptEngineContext.Variables = ScriptVariables;
            newCommandForm.ScriptEngineContext.Elements = ScriptElements;
            newCommandForm.ScriptEngineContext.Arguments = ScriptArguments;
            newCommandForm.ScriptEngineContext.Container = AContainer;

            newCommandForm.ScriptEngineContext.ProjectPath = ScriptProjectPath;
            newCommandForm.HTMLElementRecorderURL = HTMLElementRecorderURL;

            //if a command was selected
            if (newCommandForm.ShowDialog() == DialogResult.OK)
            {
                //add to listview
                CreateUndoSnapshot();
                AddCommandToListView(newCommandForm.SelectedCommand);

                ScriptVariables = newCommandForm.ScriptEngineContext.Variables;
                ScriptArguments = newCommandForm.ScriptEngineContext.Arguments;
                ResetVariableArgumentBindings();
            }

            if (newCommandForm.SelectedCommand.CommandName == "SeleniumElementActionCommand")
            {
                CreateUndoSnapshot();
                ScriptElements = newCommandForm.ScriptEngineContext.Elements;
                HTMLElementRecorderURL = newCommandForm.HTMLElementRecorderURL;
            }

            newCommandForm.Dispose();
        }

        private List<ScriptCommand> GetConfiguredCommands()
        {
            List<ScriptCommand> ConfiguredCommands = new List<ScriptCommand>();
            foreach (ListViewItem item in SelectedTabScriptActions.Items)
            {
                ConfiguredCommands.Add(item.Tag as ScriptCommand);
            }
            return ConfiguredCommands;
        }
        #endregion

        #region TreeView Events
        private void tvCommands_DoubleClick(object sender, EventArgs e)
        {
            //handle double clicks outside
            if (tvCommands.SelectedNode == null)
                return;

            //exit if parent node is clicked
            if (tvCommands.SelectedNode.Parent == null)
                return;

            AddNewCommand(tvCommands.SelectedNode.Parent.Text + " - " + tvCommands.SelectedNode.Text);
        }

        private void tvCommands_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                tvCommands_DoubleClick(this, null);
            }
        }

        private void tvCommands_ItemDrag(object sender, ItemDragEventArgs e)
        {
            tvCommands.DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        public void CopyTreeView(TreeView originalTreeView, TreeView copiedTreeView)
        {
            TreeNode copiedTreeNode;
            foreach (TreeNode originalTreeNode in originalTreeView.Nodes)
            {
                copiedTreeNode = new TreeNode(originalTreeNode.Text);
                CopyTreeViewNodes(copiedTreeNode, originalTreeNode);
                copiedTreeView.Nodes.Add(copiedTreeNode);
            }
        }

        public void CopyTreeViewNodes(TreeNode copiedTreeNode, TreeNode originalTreeNode)
        {
            TreeNode copiedChildNode;
            foreach (TreeNode originalChildNode in originalTreeNode.Nodes)
            {
                copiedChildNode = new TreeNode(originalChildNode.Text);
                copiedChildNode.ToolTipText = originalChildNode.ToolTipText;
                copiedTreeNode.Nodes.Add(copiedChildNode);
            }
        }

        private void txtCommandSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtCommandSearch.Text == _txtCommandWatermark)
                return;

            bool childNodefound = false;

            //blocks repainting tree until all controls are loaded
            tvCommands.BeginUpdate();
            tvCommands.Nodes.Clear();
            if (txtCommandSearch.Text != string.Empty)
            {
                foreach (TreeNode parentNodeCopy in _tvCommandsCopy.Nodes)
                {
                    TreeNode searchedParentNode = new TreeNode(parentNodeCopy.Text);
                    tvCommands.Nodes.Add(searchedParentNode);

                    foreach (TreeNode childNodeCopy in parentNodeCopy.Nodes)
                    {
                        if (childNodeCopy.Text.ToLower().Contains(txtCommandSearch.Text.ToLower()))
                        {
                            var searchedChildNode = new TreeNode(childNodeCopy.Text);
                            searchedChildNode.ToolTipText = childNodeCopy.ToolTipText;
                            searchedParentNode.Nodes.Add(searchedChildNode);
                            childNodefound = true;
                        }
                    }
                    if (!childNodefound && !(parentNodeCopy.Text.ToLower().Contains(txtCommandSearch.Text.ToLower())))
                    {
                        tvCommands.Nodes.Remove(searchedParentNode);
                    }
                    else if (parentNodeCopy.Text.ToLower().Contains(txtCommandSearch.Text.ToLower()))
                    {
                        searchedParentNode.Nodes.Clear();
                        foreach (TreeNode childNodeCopy in parentNodeCopy.Nodes)
                        {
                            var searchedChildNode = new TreeNode(childNodeCopy.Text);
                            searchedChildNode.ToolTipText = childNodeCopy.ToolTipText;
                            searchedParentNode.Nodes.Add(searchedChildNode);
                        }
                    }
                    childNodefound = false;
                }
                tvCommands.ExpandAll();
            }
            else
            {
                foreach (TreeNode parentNodeCopy in _tvCommandsCopy.Nodes)
                {
                    tvCommands.Nodes.Add((TreeNode)parentNodeCopy.Clone());
                }
                tvCommands.CollapseAll();
            }

            //enables redrawing tree after all controls have been added
            tvCommands.EndUpdate();
        }

        private void txtCommandSearch_Enter(object sender, EventArgs e)
        {
            if (txtCommandSearch.Text == _txtCommandWatermark)
            {
                txtCommandSearch.Text = "";
                txtCommandSearch.ForeColor = Color.Black;
            }
        }

        private void txtCommandSearch_Leave(object sender, EventArgs e)
        {
            if (txtCommandSearch.Text == "")
            {
                txtCommandSearch.Text = _txtCommandWatermark;
                txtCommandSearch.ForeColor = Color.LightGray;
            }
        }

        private void uiBtnClearCommandSearch_Click(object sender, EventArgs e)
        {
            txtCommandSearch.Clear();
        }
        #endregion
    }
}

