namespace OpenBots.UI.Forms
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.lblManageSettings = new System.Windows.Forms.Label();
            this.uiBtnOpen = new OpenBots.Core.UI.Controls.UIPictureButton();
            this.lblMainLogo = new System.Windows.Forms.Label();
            this.tlpSettings = new System.Windows.Forms.TableLayoutPanel();
            this.uiSettingTabs = new System.Windows.Forms.TabControl();
            this.tabAppSettings = new System.Windows.Forms.TabPage();
            this.chkSlimActionBar = new System.Windows.Forms.CheckBox();
            this.chkPreloadCommands = new System.Windows.Forms.CheckBox();
            this.lblStartupMode = new System.Windows.Forms.Label();
            this.cboStartUpMode = new System.Windows.Forms.ComboBox();
            this.btnSelectAttendedTaskFolder = new System.Windows.Forms.Button();
            this.lblAttendedTasksFolder = new System.Windows.Forms.Label();
            this.txtAttendedTaskFolder = new System.Windows.Forms.TextBox();
            this.txtAppFolderPath = new System.Windows.Forms.TextBox();
            this.btnLaunchAttendedMode = new System.Windows.Forms.Button();
            this.chkMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.chkSequenceDragDrop = new System.Windows.Forms.CheckBox();
            this.btnGenerateWikiDocs = new System.Windows.Forms.Button();
            this.chkInsertCommandsInline = new System.Windows.Forms.CheckBox();
            this.btnClearMetrics = new System.Windows.Forms.Button();
            this.lblScriptExecutionMetrics = new System.Windows.Forms.Label();
            this.lblGettingMetrics = new System.Windows.Forms.Label();
            this.tvExecutionTimes = new System.Windows.Forms.TreeView();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.lblRootFolder = new System.Windows.Forms.Label();
            this.lblApplicationSettings = new System.Windows.Forms.Label();
            this.chkAntiIdle = new System.Windows.Forms.CheckBox();
            this.btnUpdates = new System.Windows.Forms.Button();
            this.tabDebugSettings = new System.Windows.Forms.TabPage();
            this.btnFileManager = new System.Windows.Forms.Button();
            this.cbxMinLogLevel = new System.Windows.Forms.ComboBox();
            this.lblMinLogLevel = new System.Windows.Forms.Label();
            this.txtLogging1 = new System.Windows.Forms.TextBox();
            this.txtCommandDelay = new System.Windows.Forms.TextBox();
            this.lblLogging1 = new System.Windows.Forms.Label();
            this.cbxSinkType = new System.Windows.Forms.ComboBox();
            this.lblSinkType = new System.Windows.Forms.Label();
            this.lblLoggingSettings = new System.Windows.Forms.Label();
            this.chkAutoCalcVariables = new System.Windows.Forms.CheckBox();
            this.lblEndScriptHotKey = new System.Windows.Forms.Label();
            this.cbxCancellationKey = new System.Windows.Forms.ComboBox();
            this.chkOverrideInstances = new System.Windows.Forms.CheckBox();
            this.lblDelay = new System.Windows.Forms.Label();
            this.chkTrackMetrics = new System.Windows.Forms.CheckBox();
            this.lblAutomationEngine = new System.Windows.Forms.Label();
            this.chkShowDebug = new System.Windows.Forms.CheckBox();
            this.chkAdvancedDebug = new System.Windows.Forms.CheckBox();
            this.chkAutoCloseWindow = new System.Windows.Forms.CheckBox();
            this.chkEnableLogging = new System.Windows.Forms.CheckBox();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.bgwMetrics = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.uiBtnOpen)).BeginInit();
            this.tlpSettings.SuspendLayout();
            this.uiSettingTabs.SuspendLayout();
            this.tabAppSettings.SuspendLayout();
            this.tabDebugSettings.SuspendLayout();
            this.pnlSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblManageSettings
            // 
            this.lblManageSettings.BackColor = System.Drawing.Color.Transparent;
            this.lblManageSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManageSettings.ForeColor = System.Drawing.Color.White;
            this.lblManageSettings.Location = new System.Drawing.Point(8, 53);
            this.lblManageSettings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblManageSettings.Name = "lblManageSettings";
            this.lblManageSettings.Size = new System.Drawing.Size(603, 34);
            this.lblManageSettings.TabIndex = 14;
            this.lblManageSettings.Text = "Manage settings used by the application";
            // 
            // uiBtnOpen
            // 
            this.uiBtnOpen.BackColor = System.Drawing.Color.Transparent;
            this.uiBtnOpen.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.uiBtnOpen.DisplayText = "Ok";
            this.uiBtnOpen.DisplayTextBrush = System.Drawing.Color.White;
            this.uiBtnOpen.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.uiBtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("uiBtnOpen.Image")));
            this.uiBtnOpen.IsMouseOver = false;
            this.uiBtnOpen.Location = new System.Drawing.Point(4, 885);
            this.uiBtnOpen.Margin = new System.Windows.Forms.Padding(4);
            this.uiBtnOpen.Name = "uiBtnOpen";
            this.uiBtnOpen.Size = new System.Drawing.Size(60, 60);
            this.uiBtnOpen.TabIndex = 13;
            this.uiBtnOpen.TabStop = false;
            this.uiBtnOpen.Text = "Ok";
            this.uiBtnOpen.Click += new System.EventHandler(this.uiBtnOpen_Click);
            // 
            // lblMainLogo
            // 
            this.lblMainLogo.AutoSize = true;
            this.lblMainLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblMainLogo.Font = new System.Drawing.Font("Segoe UI Semilight", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainLogo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblMainLogo.Location = new System.Drawing.Point(4, 1);
            this.lblMainLogo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMainLogo.Name = "lblMainLogo";
            this.lblMainLogo.Size = new System.Drawing.Size(156, 54);
            this.lblMainLogo.TabIndex = 14;
            this.lblMainLogo.Text = "settings";
            // 
            // tlpSettings
            // 
            this.tlpSettings.BackColor = System.Drawing.Color.Transparent;
            this.tlpSettings.ColumnCount = 1;
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSettings.Controls.Add(this.uiSettingTabs, 0, 1);
            this.tlpSettings.Controls.Add(this.uiBtnOpen, 0, 2);
            this.tlpSettings.Controls.Add(this.pnlSettings, 0, 0);
            this.tlpSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSettings.Location = new System.Drawing.Point(0, 0);
            this.tlpSettings.Margin = new System.Windows.Forms.Padding(4);
            this.tlpSettings.Name = "tlpSettings";
            this.tlpSettings.RowCount = 3;
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSettings.Size = new System.Drawing.Size(853, 955);
            this.tlpSettings.TabIndex = 26;
            // 
            // uiSettingTabs
            // 
            this.uiSettingTabs.Controls.Add(this.tabAppSettings);
            this.uiSettingTabs.Controls.Add(this.tabDebugSettings);
            this.uiSettingTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiSettingTabs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiSettingTabs.Location = new System.Drawing.Point(4, 90);
            this.uiSettingTabs.Margin = new System.Windows.Forms.Padding(4);
            this.uiSettingTabs.Name = "uiSettingTabs";
            this.uiSettingTabs.SelectedIndex = 0;
            this.uiSettingTabs.Size = new System.Drawing.Size(845, 787);
            this.uiSettingTabs.TabIndex = 25;
            // 
            // tabAppSettings
            // 
            this.tabAppSettings.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabAppSettings.Controls.Add(this.chkSlimActionBar);
            this.tabAppSettings.Controls.Add(this.chkPreloadCommands);
            this.tabAppSettings.Controls.Add(this.lblStartupMode);
            this.tabAppSettings.Controls.Add(this.cboStartUpMode);
            this.tabAppSettings.Controls.Add(this.btnSelectAttendedTaskFolder);
            this.tabAppSettings.Controls.Add(this.lblAttendedTasksFolder);
            this.tabAppSettings.Controls.Add(this.txtAttendedTaskFolder);
            this.tabAppSettings.Controls.Add(this.txtAppFolderPath);
            this.tabAppSettings.Controls.Add(this.btnLaunchAttendedMode);
            this.tabAppSettings.Controls.Add(this.chkMinimizeToTray);
            this.tabAppSettings.Controls.Add(this.chkSequenceDragDrop);
            this.tabAppSettings.Controls.Add(this.btnGenerateWikiDocs);
            this.tabAppSettings.Controls.Add(this.chkInsertCommandsInline);
            this.tabAppSettings.Controls.Add(this.btnClearMetrics);
            this.tabAppSettings.Controls.Add(this.lblScriptExecutionMetrics);
            this.tabAppSettings.Controls.Add(this.lblGettingMetrics);
            this.tabAppSettings.Controls.Add(this.tvExecutionTimes);
            this.tabAppSettings.Controls.Add(this.btnSelectFolder);
            this.tabAppSettings.Controls.Add(this.lblRootFolder);
            this.tabAppSettings.Controls.Add(this.lblApplicationSettings);
            this.tabAppSettings.Controls.Add(this.chkAntiIdle);
            this.tabAppSettings.Controls.Add(this.btnUpdates);
            this.tabAppSettings.Location = new System.Drawing.Point(4, 37);
            this.tabAppSettings.Margin = new System.Windows.Forms.Padding(4);
            this.tabAppSettings.Name = "tabAppSettings";
            this.tabAppSettings.Padding = new System.Windows.Forms.Padding(4);
            this.tabAppSettings.Size = new System.Drawing.Size(837, 746);
            this.tabAppSettings.TabIndex = 0;
            this.tabAppSettings.Text = "Application";
            // 
            // chkSlimActionBar
            // 
            this.chkSlimActionBar.AutoSize = true;
            this.chkSlimActionBar.BackColor = System.Drawing.Color.Transparent;
            this.chkSlimActionBar.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSlimActionBar.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkSlimActionBar.Location = new System.Drawing.Point(13, 153);
            this.chkSlimActionBar.Margin = new System.Windows.Forms.Padding(4);
            this.chkSlimActionBar.Name = "chkSlimActionBar";
            this.chkSlimActionBar.Size = new System.Drawing.Size(197, 32);
            this.chkSlimActionBar.TabIndex = 42;
            this.chkSlimActionBar.Text = "Use Slim Action Bar";
            this.chkSlimActionBar.UseVisualStyleBackColor = false;
            // 
            // chkPreloadCommands
            // 
            this.chkPreloadCommands.AutoSize = true;
            this.chkPreloadCommands.BackColor = System.Drawing.Color.Transparent;
            this.chkPreloadCommands.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPreloadCommands.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkPreloadCommands.Location = new System.Drawing.Point(13, 178);
            this.chkPreloadCommands.Margin = new System.Windows.Forms.Padding(4);
            this.chkPreloadCommands.Name = "chkPreloadCommands";
            this.chkPreloadCommands.Size = new System.Drawing.Size(409, 32);
            this.chkPreloadCommands.TabIndex = 41;
            this.chkPreloadCommands.Text = "Load Commands at Startup (Reduces Flicker)";
            this.chkPreloadCommands.UseVisualStyleBackColor = false;
            this.chkPreloadCommands.Visible = false;
            // 
            // lblStartupMode
            // 
            this.lblStartupMode.AutoSize = true;
            this.lblStartupMode.BackColor = System.Drawing.Color.Transparent;
            this.lblStartupMode.Font = new System.Drawing.Font("Segoe UI Semilight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartupMode.ForeColor = System.Drawing.Color.SlateGray;
            this.lblStartupMode.Location = new System.Drawing.Point(16, 347);
            this.lblStartupMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartupMode.Name = "lblStartupMode";
            this.lblStartupMode.Size = new System.Drawing.Size(111, 23);
            this.lblStartupMode.TabIndex = 39;
            this.lblStartupMode.Text = "Startup Mode";
            // 
            // cboStartUpMode
            // 
            this.cboStartUpMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStartUpMode.FormattingEnabled = true;
            this.cboStartUpMode.Items.AddRange(new object[] {
            "Builder Mode",
            "Attended Task Mode"});
            this.cboStartUpMode.Location = new System.Drawing.Point(16, 374);
            this.cboStartUpMode.Margin = new System.Windows.Forms.Padding(4);
            this.cboStartUpMode.Name = "cboStartUpMode";
            this.cboStartUpMode.Size = new System.Drawing.Size(291, 36);
            this.cboStartUpMode.TabIndex = 38;
            // 
            // btnSelectAttendedTaskFolder
            // 
            this.btnSelectAttendedTaskFolder.Location = new System.Drawing.Point(672, 306);
            this.btnSelectAttendedTaskFolder.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectAttendedTaskFolder.Name = "btnSelectAttendedTaskFolder";
            this.btnSelectAttendedTaskFolder.Size = new System.Drawing.Size(56, 37);
            this.btnSelectAttendedTaskFolder.TabIndex = 37;
            this.btnSelectAttendedTaskFolder.Text = "...";
            this.btnSelectAttendedTaskFolder.UseVisualStyleBackColor = true;
            this.btnSelectAttendedTaskFolder.Click += new System.EventHandler(this.btnSelectAttendedTaskFolder_Click);
            // 
            // lblAttendedTasksFolder
            // 
            this.lblAttendedTasksFolder.AutoSize = true;
            this.lblAttendedTasksFolder.BackColor = System.Drawing.Color.Transparent;
            this.lblAttendedTasksFolder.Font = new System.Drawing.Font("Segoe UI Semilight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttendedTasksFolder.ForeColor = System.Drawing.Color.SlateGray;
            this.lblAttendedTasksFolder.Location = new System.Drawing.Point(16, 282);
            this.lblAttendedTasksFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAttendedTasksFolder.Name = "lblAttendedTasksFolder";
            this.lblAttendedTasksFolder.Size = new System.Drawing.Size(209, 23);
            this.lblAttendedTasksFolder.TabIndex = 36;
            this.lblAttendedTasksFolder.Text = "Attended Tasks Folder Path";
            // 
            // txtAttendedTaskFolder
            // 
            this.txtAttendedTaskFolder.Location = new System.Drawing.Point(16, 307);
            this.txtAttendedTaskFolder.Margin = new System.Windows.Forms.Padding(4);
            this.txtAttendedTaskFolder.Name = "txtAttendedTaskFolder";
            this.txtAttendedTaskFolder.Size = new System.Drawing.Size(652, 34);
            this.txtAttendedTaskFolder.TabIndex = 35;
            // 
            // txtAppFolderPath
            // 
            this.txtAppFolderPath.Location = new System.Drawing.Point(16, 242);
            this.txtAppFolderPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtAppFolderPath.Name = "txtAppFolderPath";
            this.txtAppFolderPath.Size = new System.Drawing.Size(652, 34);
            this.txtAppFolderPath.TabIndex = 23;
            // 
            // btnLaunchAttendedMode
            // 
            this.btnLaunchAttendedMode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLaunchAttendedMode.Location = new System.Drawing.Point(300, 420);
            this.btnLaunchAttendedMode.Margin = new System.Windows.Forms.Padding(4);
            this.btnLaunchAttendedMode.Name = "btnLaunchAttendedMode";
            this.btnLaunchAttendedMode.Size = new System.Drawing.Size(276, 33);
            this.btnLaunchAttendedMode.TabIndex = 34;
            this.btnLaunchAttendedMode.Text = "Launch Attended Mode";
            this.btnLaunchAttendedMode.UseVisualStyleBackColor = true;
            this.btnLaunchAttendedMode.Click += new System.EventHandler(this.btnLaunchAttendedMode_Click);
            // 
            // chkMinimizeToTray
            // 
            this.chkMinimizeToTray.AutoSize = true;
            this.chkMinimizeToTray.BackColor = System.Drawing.Color.Transparent;
            this.chkMinimizeToTray.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMinimizeToTray.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkMinimizeToTray.Location = new System.Drawing.Point(13, 126);
            this.chkMinimizeToTray.Margin = new System.Windows.Forms.Padding(4);
            this.chkMinimizeToTray.Name = "chkMinimizeToTray";
            this.chkMinimizeToTray.Size = new System.Drawing.Size(236, 32);
            this.chkMinimizeToTray.TabIndex = 33;
            this.chkMinimizeToTray.Text = "Minimize to System Tray";
            this.chkMinimizeToTray.UseVisualStyleBackColor = false;
            // 
            // chkSequenceDragDrop
            // 
            this.chkSequenceDragDrop.AutoSize = true;
            this.chkSequenceDragDrop.BackColor = System.Drawing.Color.Transparent;
            this.chkSequenceDragDrop.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSequenceDragDrop.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkSequenceDragDrop.Location = new System.Drawing.Point(13, 98);
            this.chkSequenceDragDrop.Margin = new System.Windows.Forms.Padding(4);
            this.chkSequenceDragDrop.Name = "chkSequenceDragDrop";
            this.chkSequenceDragDrop.Size = new System.Drawing.Size(438, 32);
            this.chkSequenceDragDrop.TabIndex = 32;
            this.chkSequenceDragDrop.Text = "Allow Drag and Drop into Sequence Commands";
            this.chkSequenceDragDrop.UseVisualStyleBackColor = false;
            // 
            // btnGenerateWikiDocs
            // 
            this.btnGenerateWikiDocs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateWikiDocs.Location = new System.Drawing.Point(16, 461);
            this.btnGenerateWikiDocs.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerateWikiDocs.Name = "btnGenerateWikiDocs";
            this.btnGenerateWikiDocs.Size = new System.Drawing.Size(276, 33);
            this.btnGenerateWikiDocs.TabIndex = 31;
            this.btnGenerateWikiDocs.Text = "Generate Documentation";
            this.btnGenerateWikiDocs.UseVisualStyleBackColor = true;
            this.btnGenerateWikiDocs.Click += new System.EventHandler(this.btnGenerateWikiDocs_Click);
            // 
            // chkInsertCommandsInline
            // 
            this.chkInsertCommandsInline.AutoSize = true;
            this.chkInsertCommandsInline.BackColor = System.Drawing.Color.Transparent;
            this.chkInsertCommandsInline.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInsertCommandsInline.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkInsertCommandsInline.Location = new System.Drawing.Point(13, 71);
            this.chkInsertCommandsInline.Margin = new System.Windows.Forms.Padding(4);
            this.chkInsertCommandsInline.Name = "chkInsertCommandsInline";
            this.chkInsertCommandsInline.Size = new System.Drawing.Size(452, 32);
            this.chkInsertCommandsInline.TabIndex = 30;
            this.chkInsertCommandsInline.Text = "New Commands Insert Below Selected Command";
            this.chkInsertCommandsInline.UseVisualStyleBackColor = false;
            // 
            // btnClearMetrics
            // 
            this.btnClearMetrics.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearMetrics.Location = new System.Drawing.Point(15, 688);
            this.btnClearMetrics.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearMetrics.Name = "btnClearMetrics";
            this.btnClearMetrics.Size = new System.Drawing.Size(144, 31);
            this.btnClearMetrics.TabIndex = 29;
            this.btnClearMetrics.Text = "Clear Metrics";
            this.btnClearMetrics.UseVisualStyleBackColor = true;
            this.btnClearMetrics.Visible = false;
            this.btnClearMetrics.Click += new System.EventHandler(this.btnClearMetrics_Click);
            // 
            // lblScriptExecutionMetrics
            // 
            this.lblScriptExecutionMetrics.AutoSize = true;
            this.lblScriptExecutionMetrics.BackColor = System.Drawing.Color.Transparent;
            this.lblScriptExecutionMetrics.Font = new System.Drawing.Font("Segoe UI Semilight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScriptExecutionMetrics.ForeColor = System.Drawing.Color.SlateGray;
            this.lblScriptExecutionMetrics.Location = new System.Drawing.Point(16, 498);
            this.lblScriptExecutionMetrics.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScriptExecutionMetrics.Name = "lblScriptExecutionMetrics";
            this.lblScriptExecutionMetrics.Size = new System.Drawing.Size(186, 23);
            this.lblScriptExecutionMetrics.TabIndex = 28;
            this.lblScriptExecutionMetrics.Text = "Script Execution Metrics";
            // 
            // lblGettingMetrics
            // 
            this.lblGettingMetrics.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDownGrid;
            this.lblGettingMetrics.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGettingMetrics.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblGettingMetrics.Location = new System.Drawing.Point(16, 525);
            this.lblGettingMetrics.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGettingMetrics.Name = "lblGettingMetrics";
            this.lblGettingMetrics.Size = new System.Drawing.Size(712, 156);
            this.lblGettingMetrics.TabIndex = 27;
            this.lblGettingMetrics.Text = "Metrics";
            // 
            // tvExecutionTimes
            // 
            this.tvExecutionTimes.Location = new System.Drawing.Point(16, 525);
            this.tvExecutionTimes.Margin = new System.Windows.Forms.Padding(4);
            this.tvExecutionTimes.Name = "tvExecutionTimes";
            this.tvExecutionTimes.Size = new System.Drawing.Size(711, 155);
            this.tvExecutionTimes.TabIndex = 26;
            this.tvExecutionTimes.Visible = false;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(672, 241);
            this.btnSelectFolder.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(56, 37);
            this.btnSelectFolder.TabIndex = 25;
            this.btnSelectFolder.Text = "...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // lblRootFolder
            // 
            this.lblRootFolder.AutoSize = true;
            this.lblRootFolder.BackColor = System.Drawing.Color.Transparent;
            this.lblRootFolder.Font = new System.Drawing.Font("Segoe UI Semilight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRootFolder.ForeColor = System.Drawing.Color.SlateGray;
            this.lblRootFolder.Location = new System.Drawing.Point(16, 217);
            this.lblRootFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRootFolder.Name = "lblRootFolder";
            this.lblRootFolder.Size = new System.Drawing.Size(133, 23);
            this.lblRootFolder.TabIndex = 24;
            this.lblRootFolder.Text = "Root Folder Path";
            // 
            // lblApplicationSettings
            // 
            this.lblApplicationSettings.AutoSize = true;
            this.lblApplicationSettings.BackColor = System.Drawing.Color.Transparent;
            this.lblApplicationSettings.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationSettings.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblApplicationSettings.Location = new System.Drawing.Point(8, 5);
            this.lblApplicationSettings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApplicationSettings.Name = "lblApplicationSettings";
            this.lblApplicationSettings.Size = new System.Drawing.Size(239, 37);
            this.lblApplicationSettings.TabIndex = 21;
            this.lblApplicationSettings.Text = "Application Settings";
            // 
            // chkAntiIdle
            // 
            this.chkAntiIdle.AutoSize = true;
            this.chkAntiIdle.BackColor = System.Drawing.Color.Transparent;
            this.chkAntiIdle.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAntiIdle.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkAntiIdle.Location = new System.Drawing.Point(13, 44);
            this.chkAntiIdle.Margin = new System.Windows.Forms.Padding(4);
            this.chkAntiIdle.Name = "chkAntiIdle";
            this.chkAntiIdle.Size = new System.Drawing.Size(268, 32);
            this.chkAntiIdle.TabIndex = 20;
            this.chkAntiIdle.Text = "Anti-Idle (while app is open)";
            this.chkAntiIdle.UseVisualStyleBackColor = false;
            // 
            // btnUpdates
            // 
            this.btnUpdates.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdates.Location = new System.Drawing.Point(16, 420);
            this.btnUpdates.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdates.Name = "btnUpdates";
            this.btnUpdates.Size = new System.Drawing.Size(276, 33);
            this.btnUpdates.TabIndex = 22;
            this.btnUpdates.Text = "Check For Updates";
            this.btnUpdates.UseVisualStyleBackColor = true;
            this.btnUpdates.Click += new System.EventHandler(this.btnUpdateCheck_Click);
            // 
            // tabDebugSettings
            // 
            this.tabDebugSettings.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabDebugSettings.Controls.Add(this.btnFileManager);
            this.tabDebugSettings.Controls.Add(this.cbxMinLogLevel);
            this.tabDebugSettings.Controls.Add(this.lblMinLogLevel);
            this.tabDebugSettings.Controls.Add(this.txtLogging1);
            this.tabDebugSettings.Controls.Add(this.txtCommandDelay);
            this.tabDebugSettings.Controls.Add(this.lblLogging1);
            this.tabDebugSettings.Controls.Add(this.cbxSinkType);
            this.tabDebugSettings.Controls.Add(this.lblSinkType);
            this.tabDebugSettings.Controls.Add(this.lblLoggingSettings);
            this.tabDebugSettings.Controls.Add(this.chkAutoCalcVariables);
            this.tabDebugSettings.Controls.Add(this.lblEndScriptHotKey);
            this.tabDebugSettings.Controls.Add(this.cbxCancellationKey);
            this.tabDebugSettings.Controls.Add(this.chkOverrideInstances);
            this.tabDebugSettings.Controls.Add(this.lblDelay);
            this.tabDebugSettings.Controls.Add(this.chkTrackMetrics);
            this.tabDebugSettings.Controls.Add(this.lblAutomationEngine);
            this.tabDebugSettings.Controls.Add(this.chkShowDebug);
            this.tabDebugSettings.Controls.Add(this.chkAdvancedDebug);
            this.tabDebugSettings.Controls.Add(this.chkAutoCloseWindow);
            this.tabDebugSettings.Controls.Add(this.chkEnableLogging);
            this.tabDebugSettings.Location = new System.Drawing.Point(4, 37);
            this.tabDebugSettings.Margin = new System.Windows.Forms.Padding(4);
            this.tabDebugSettings.Name = "tabDebugSettings";
            this.tabDebugSettings.Padding = new System.Windows.Forms.Padding(4);
            this.tabDebugSettings.Size = new System.Drawing.Size(837, 746);
            this.tabDebugSettings.TabIndex = 1;
            this.tabDebugSettings.Text = "Automation Engine";
            // 
            // btnFileManager
            // 
            this.btnFileManager.Location = new System.Drawing.Point(787, 444);
            this.btnFileManager.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFileManager.Name = "btnFileManager";
            this.btnFileManager.Size = new System.Drawing.Size(36, 34);
            this.btnFileManager.TabIndex = 52;
            this.btnFileManager.Text = "...";
            this.btnFileManager.UseVisualStyleBackColor = true;
            this.btnFileManager.Click += new System.EventHandler(this.btnFileManager_Click);
            // 
            // cbxMinLogLevel
            // 
            this.cbxMinLogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMinLogLevel.FormattingEnabled = true;
            this.cbxMinLogLevel.Location = new System.Drawing.Point(595, 397);
            this.cbxMinLogLevel.Margin = new System.Windows.Forms.Padding(4);
            this.cbxMinLogLevel.Name = "cbxMinLogLevel";
            this.cbxMinLogLevel.Size = new System.Drawing.Size(188, 36);
            this.cbxMinLogLevel.TabIndex = 51;
            this.cbxMinLogLevel.SelectedIndexChanged += new System.EventHandler(this.cbxMinLogLevel_SelectedIndexChanged);
            // 
            // lblMinLogLevel
            // 
            this.lblMinLogLevel.AutoSize = true;
            this.lblMinLogLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblMinLogLevel.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinLogLevel.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblMinLogLevel.Location = new System.Drawing.Point(391, 400);
            this.lblMinLogLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMinLogLevel.Name = "lblMinLogLevel";
            this.lblMinLogLevel.Size = new System.Drawing.Size(180, 28);
            this.lblMinLogLevel.TabIndex = 50;
            this.lblMinLogLevel.Text = "Minimum Log Level:";
            // 
            // txtLogging1
            // 
            this.txtLogging1.Location = new System.Drawing.Point(189, 442);
            this.txtLogging1.Margin = new System.Windows.Forms.Padding(4);
            this.txtLogging1.Name = "txtLogging1";
            this.txtLogging1.Size = new System.Drawing.Size(592, 34);
            this.txtLogging1.TabIndex = 43;
            // 
            // txtCommandDelay
            // 
            this.txtCommandDelay.Location = new System.Drawing.Point(444, 251);
            this.txtCommandDelay.Margin = new System.Windows.Forms.Padding(4);
            this.txtCommandDelay.Name = "txtCommandDelay";
            this.txtCommandDelay.Size = new System.Drawing.Size(101, 34);
            this.txtCommandDelay.TabIndex = 33;
            // 
            // lblLogging1
            // 
            this.lblLogging1.AutoSize = true;
            this.lblLogging1.BackColor = System.Drawing.Color.Transparent;
            this.lblLogging1.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogging1.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblLogging1.Location = new System.Drawing.Point(8, 446);
            this.lblLogging1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogging1.Name = "lblLogging1";
            this.lblLogging1.Size = new System.Drawing.Size(86, 28);
            this.lblLogging1.TabIndex = 42;
            this.lblLogging1.Text = "File Path:";
            // 
            // cbxSinkType
            // 
            this.cbxSinkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSinkType.FormattingEnabled = true;
            this.cbxSinkType.Location = new System.Drawing.Point(189, 397);
            this.cbxSinkType.Margin = new System.Windows.Forms.Padding(4);
            this.cbxSinkType.Name = "cbxSinkType";
            this.cbxSinkType.Size = new System.Drawing.Size(188, 36);
            this.cbxSinkType.TabIndex = 41;
            this.cbxSinkType.SelectedIndexChanged += new System.EventHandler(this.cbxSinkType_SelectedIndexChanged);
            // 
            // lblSinkType
            // 
            this.lblSinkType.AutoSize = true;
            this.lblSinkType.BackColor = System.Drawing.Color.Transparent;
            this.lblSinkType.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSinkType.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblSinkType.Location = new System.Drawing.Point(8, 400);
            this.lblSinkType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSinkType.Name = "lblSinkType";
            this.lblSinkType.Size = new System.Drawing.Size(94, 28);
            this.lblSinkType.TabIndex = 40;
            this.lblSinkType.Text = "Sink Type:";
            // 
            // lblLoggingSettings
            // 
            this.lblLoggingSettings.AutoSize = true;
            this.lblLoggingSettings.BackColor = System.Drawing.Color.Transparent;
            this.lblLoggingSettings.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoggingSettings.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblLoggingSettings.Location = new System.Drawing.Point(8, 352);
            this.lblLoggingSettings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoggingSettings.Name = "lblLoggingSettings";
            this.lblLoggingSettings.Size = new System.Drawing.Size(204, 37);
            this.lblLoggingSettings.TabIndex = 39;
            this.lblLoggingSettings.Text = "Logging Settings";
            // 
            // chkAutoCalcVariables
            // 
            this.chkAutoCalcVariables.AutoSize = true;
            this.chkAutoCalcVariables.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoCalcVariables.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoCalcVariables.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkAutoCalcVariables.Location = new System.Drawing.Point(13, 206);
            this.chkAutoCalcVariables.Margin = new System.Windows.Forms.Padding(4);
            this.chkAutoCalcVariables.Name = "chkAutoCalcVariables";
            this.chkAutoCalcVariables.Size = new System.Drawing.Size(309, 32);
            this.chkAutoCalcVariables.TabIndex = 38;
            this.chkAutoCalcVariables.Text = "Calculate Variables Automatically";
            this.chkAutoCalcVariables.UseVisualStyleBackColor = false;
            // 
            // lblEndScriptHotKey
            // 
            this.lblEndScriptHotKey.AutoSize = true;
            this.lblEndScriptHotKey.BackColor = System.Drawing.Color.Transparent;
            this.lblEndScriptHotKey.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndScriptHotKey.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblEndScriptHotKey.Location = new System.Drawing.Point(8, 295);
            this.lblEndScriptHotKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndScriptHotKey.Name = "lblEndScriptHotKey";
            this.lblEndScriptHotKey.Size = new System.Drawing.Size(164, 28);
            this.lblEndScriptHotKey.TabIndex = 37;
            this.lblEndScriptHotKey.Text = "End Script Hotkey:";
            // 
            // cbxCancellationKey
            // 
            this.cbxCancellationKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCancellationKey.FormattingEnabled = true;
            this.cbxCancellationKey.Location = new System.Drawing.Point(189, 295);
            this.cbxCancellationKey.Margin = new System.Windows.Forms.Padding(4);
            this.cbxCancellationKey.Name = "cbxCancellationKey";
            this.cbxCancellationKey.Size = new System.Drawing.Size(188, 36);
            this.cbxCancellationKey.TabIndex = 36;
            // 
            // chkOverrideInstances
            // 
            this.chkOverrideInstances.AutoSize = true;
            this.chkOverrideInstances.BackColor = System.Drawing.Color.Transparent;
            this.chkOverrideInstances.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOverrideInstances.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkOverrideInstances.Location = new System.Drawing.Point(13, 178);
            this.chkOverrideInstances.Margin = new System.Windows.Forms.Padding(4);
            this.chkOverrideInstances.Name = "chkOverrideInstances";
            this.chkOverrideInstances.Size = new System.Drawing.Size(230, 32);
            this.chkOverrideInstances.TabIndex = 35;
            this.chkOverrideInstances.Text = "Override App Instances";
            this.chkOverrideInstances.UseVisualStyleBackColor = false;
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblDelay.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDelay.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblDelay.Location = new System.Drawing.Point(8, 250);
            this.lblDelay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(430, 28);
            this.lblDelay.TabIndex = 34;
            this.lblDelay.Text = "Default delay between executing commands (ms):";
            // 
            // chkTrackMetrics
            // 
            this.chkTrackMetrics.AutoSize = true;
            this.chkTrackMetrics.BackColor = System.Drawing.Color.Transparent;
            this.chkTrackMetrics.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTrackMetrics.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkTrackMetrics.Location = new System.Drawing.Point(13, 153);
            this.chkTrackMetrics.Margin = new System.Windows.Forms.Padding(4);
            this.chkTrackMetrics.Name = "chkTrackMetrics";
            this.chkTrackMetrics.Size = new System.Drawing.Size(229, 32);
            this.chkTrackMetrics.TabIndex = 25;
            this.chkTrackMetrics.Text = "Track Execution Metrics";
            this.chkTrackMetrics.UseVisualStyleBackColor = false;
            // 
            // lblAutomationEngine
            // 
            this.lblAutomationEngine.AutoSize = true;
            this.lblAutomationEngine.BackColor = System.Drawing.Color.Transparent;
            this.lblAutomationEngine.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutomationEngine.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblAutomationEngine.Location = new System.Drawing.Point(8, 7);
            this.lblAutomationEngine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutomationEngine.Name = "lblAutomationEngine";
            this.lblAutomationEngine.Size = new System.Drawing.Size(233, 37);
            this.lblAutomationEngine.TabIndex = 15;
            this.lblAutomationEngine.Text = "Automation Engine";
            // 
            // chkShowDebug
            // 
            this.chkShowDebug.AutoSize = true;
            this.chkShowDebug.BackColor = System.Drawing.Color.Transparent;
            this.chkShowDebug.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowDebug.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkShowDebug.Location = new System.Drawing.Point(13, 43);
            this.chkShowDebug.Margin = new System.Windows.Forms.Padding(4);
            this.chkShowDebug.Name = "chkShowDebug";
            this.chkShowDebug.Size = new System.Drawing.Size(216, 32);
            this.chkShowDebug.TabIndex = 12;
            this.chkShowDebug.Text = "Show Debug Window";
            this.chkShowDebug.UseVisualStyleBackColor = false;
            // 
            // chkAdvancedDebug
            // 
            this.chkAdvancedDebug.AutoSize = true;
            this.chkAdvancedDebug.BackColor = System.Drawing.Color.Transparent;
            this.chkAdvancedDebug.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAdvancedDebug.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkAdvancedDebug.Location = new System.Drawing.Point(13, 126);
            this.chkAdvancedDebug.Margin = new System.Windows.Forms.Padding(4);
            this.chkAdvancedDebug.Name = "chkAdvancedDebug";
            this.chkAdvancedDebug.Size = new System.Drawing.Size(424, 32);
            this.chkAdvancedDebug.TabIndex = 23;
            this.chkAdvancedDebug.Text = "Show Advanced Debug Logs During Execution";
            this.chkAdvancedDebug.UseVisualStyleBackColor = false;
            // 
            // chkAutoCloseWindow
            // 
            this.chkAutoCloseWindow.AutoSize = true;
            this.chkAutoCloseWindow.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoCloseWindow.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoCloseWindow.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkAutoCloseWindow.Location = new System.Drawing.Point(13, 70);
            this.chkAutoCloseWindow.Margin = new System.Windows.Forms.Padding(4);
            this.chkAutoCloseWindow.Name = "chkAutoCloseWindow";
            this.chkAutoCloseWindow.Size = new System.Drawing.Size(334, 32);
            this.chkAutoCloseWindow.TabIndex = 13;
            this.chkAutoCloseWindow.Text = "Automatically Close Debug Window";
            this.chkAutoCloseWindow.UseVisualStyleBackColor = false;
            // 
            // chkEnableLogging
            // 
            this.chkEnableLogging.AutoSize = true;
            this.chkEnableLogging.BackColor = System.Drawing.Color.Transparent;
            this.chkEnableLogging.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableLogging.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkEnableLogging.Location = new System.Drawing.Point(13, 98);
            this.chkEnableLogging.Margin = new System.Windows.Forms.Padding(4);
            this.chkEnableLogging.Name = "chkEnableLogging";
            this.chkEnableLogging.Size = new System.Drawing.Size(256, 32);
            this.chkEnableLogging.TabIndex = 14;
            this.chkEnableLogging.Text = "Enable Diagnostic Logging";
            this.chkEnableLogging.UseVisualStyleBackColor = false;
            // 
            // pnlSettings
            // 
            this.pnlSettings.BackColor = System.Drawing.Color.Transparent;
            this.pnlSettings.Controls.Add(this.lblMainLogo);
            this.pnlSettings.Controls.Add(this.lblManageSettings);
            this.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSettings.Location = new System.Drawing.Point(0, 0);
            this.pnlSettings.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(853, 86);
            this.pnlSettings.TabIndex = 26;
            // 
            // bgwMetrics
            // 
            this.bgwMetrics.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwMetrics_DoWork);
            this.bgwMetrics.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwMetrics_RunWorkerCompleted);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 955);
            this.Controls.Add(this.tlpSettings);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiBtnOpen)).EndInit();
            this.tlpSettings.ResumeLayout(false);
            this.uiSettingTabs.ResumeLayout(false);
            this.tabAppSettings.ResumeLayout(false);
            this.tabAppSettings.PerformLayout();
            this.tabDebugSettings.ResumeLayout(false);
            this.tabDebugSettings.PerformLayout();
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblManageSettings;
        private OpenBots.Core.UI.Controls.UIPictureButton uiBtnOpen;
        private System.Windows.Forms.Label lblMainLogo;
        private System.Windows.Forms.TableLayoutPanel tlpSettings;
        private System.Windows.Forms.Panel pnlSettings;
        private System.ComponentModel.BackgroundWorker bgwMetrics;
        private System.Windows.Forms.TabControl uiSettingTabs;
        private System.Windows.Forms.TabPage tabDebugSettings;
        private System.Windows.Forms.Button btnFileManager;
        private System.Windows.Forms.ComboBox cbxMinLogLevel;
        private System.Windows.Forms.Label lblMinLogLevel;
        private System.Windows.Forms.TextBox txtLogging1;
        private System.Windows.Forms.TextBox txtCommandDelay;
        private System.Windows.Forms.Label lblLogging1;
        private System.Windows.Forms.ComboBox cbxSinkType;
        private System.Windows.Forms.Label lblSinkType;
        private System.Windows.Forms.Label lblLoggingSettings;
        internal System.Windows.Forms.CheckBox chkAutoCalcVariables;
        private System.Windows.Forms.Label lblEndScriptHotKey;
        private System.Windows.Forms.ComboBox cbxCancellationKey;
        internal System.Windows.Forms.CheckBox chkOverrideInstances;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.CheckBox chkTrackMetrics;
        private System.Windows.Forms.Label lblAutomationEngine;
        private System.Windows.Forms.CheckBox chkShowDebug;
        private System.Windows.Forms.CheckBox chkAdvancedDebug;
        private System.Windows.Forms.CheckBox chkAutoCloseWindow;
        private System.Windows.Forms.CheckBox chkEnableLogging;
        private System.Windows.Forms.TabPage tabAppSettings;
        private System.Windows.Forms.CheckBox chkSlimActionBar;
        private System.Windows.Forms.CheckBox chkPreloadCommands;
        private System.Windows.Forms.Label lblStartupMode;
        private System.Windows.Forms.ComboBox cboStartUpMode;
        private System.Windows.Forms.Button btnSelectAttendedTaskFolder;
        private System.Windows.Forms.Label lblAttendedTasksFolder;
        private System.Windows.Forms.TextBox txtAttendedTaskFolder;
        private System.Windows.Forms.TextBox txtAppFolderPath;
        private System.Windows.Forms.Button btnLaunchAttendedMode;
        private System.Windows.Forms.CheckBox chkMinimizeToTray;
        private System.Windows.Forms.CheckBox chkSequenceDragDrop;
        private System.Windows.Forms.Button btnGenerateWikiDocs;
        private System.Windows.Forms.CheckBox chkInsertCommandsInline;
        private System.Windows.Forms.Button btnClearMetrics;
        private System.Windows.Forms.Label lblScriptExecutionMetrics;
        private System.Windows.Forms.Label lblGettingMetrics;
        private System.Windows.Forms.TreeView tvExecutionTimes;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label lblRootFolder;
        private System.Windows.Forms.Label lblApplicationSettings;
        private System.Windows.Forms.CheckBox chkAntiIdle;
        private System.Windows.Forms.Button btnUpdates;
    }
}