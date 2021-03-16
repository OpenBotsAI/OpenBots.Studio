using OpenBots.Core.Enums;
using OpenBots.Core.IO;
using OpenBots.Core.Project;
using OpenBots.Core.Settings;
using OpenBots.Core.UI.Forms;
using OpenBots.Core.Utilities.CommonUtilities;
using Serilog.Core;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OpenBots.UI.Forms
{
    public partial class frmAttendedMode : UIForm
    {
        #region Variables
        private ApplicationSettings _appSettings;
        private bool _dragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        private string _publishedProjectsPath;
        #endregion

        #region Form Events
        public frmAttendedMode()
        {
            InitializeComponent();
        }

        private void frmAttendedMode_Load(object sender, EventArgs e)
        {
            //get app settings
            _appSettings = new ApplicationSettings().GetOrCreateApplicationSettings();

            //setup file system watcher
            _publishedProjectsPath = Folders.GetFolder(FolderType.PublishedFolder);
            if (!Directory.Exists(_publishedProjectsPath))
                Directory.CreateDirectory(_publishedProjectsPath);

            attendedScriptWatcher.Path = _publishedProjectsPath;
            attendedScriptWatcher.Filter = "*.*";

            //move form to default location
            MoveToDefaultFormLocation();

            //load scripts to be used for attended automation
            LoadPublishedProjects();
        }

        private void frmAttendedMode_Shown(object sender, EventArgs e)
        {
            Program.SplashForm.Close();
        }

        private void frmAttendedMode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //move to default location
            MoveToDefaultFormLocation();
        }

        private void uiBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MoveToDefaultFormLocation()
        {
            //move to top middle of screen
            Screen myScreen = Screen.FromControl(this);
            Rectangle area = myScreen.WorkingArea;

            Top = 0;
            Left = (area.Width - Width) / 2;
        }

        private void uiBtnRun_Click(object sender, EventArgs e)
        {
            if (cboSelectedProject.Text == $"No published projects in '{_publishedProjectsPath}'")
                return;

            string projectPackagePath = Path.Combine(Folders.GetFolder(FolderType.PublishedFolder), cboSelectedProject.Text);

            if (!File.Exists(projectPackagePath))
            {
                MessageBox.Show($"Unable to find '{projectPackagePath}' in Published directory", "Error");
                return;
            }
                
            string newProjectPath = Path.Combine(Folders.GetFolder(FolderType.TempFolder), Path.GetFileNameWithoutExtension(cboSelectedProject.Text));
            
            if (Directory.Exists(newProjectPath))
                Directory.Delete(newProjectPath, true);

            try
            {               
                Directory.CreateDirectory(newProjectPath);
                File.Copy(projectPackagePath, Path.Combine(Folders.GetFolder(FolderType.TempFolder), cboSelectedProject.Text), true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }

            string configPath;
            string projectPath;

            try
            {                
                configPath = Project.ExtractGalleryProject(newProjectPath);
                projectPath = Directory.GetParent(configPath).ToString();
            }
            catch (Exception ex)
            {
                Directory.Delete(newProjectPath, true);
                MessageBox.Show(ex.Message, "Error");
                return;
            }

            var projectName = new DirectoryInfo(projectPath).Name;
            //initialize Logger
            Logger engineLogger = null;
            switch (_appSettings.EngineSettings.LoggingSinkType)
            {
                case SinkType.File:
                    if (string.IsNullOrEmpty(_appSettings.EngineSettings.LoggingValue1.Trim()))
                        _appSettings.EngineSettings.LoggingValue1 = Path.Combine(Folders.GetFolder(FolderType.LogFolder), "OpenBots Engine Logs.txt");

                    engineLogger = new Logging().CreateFileLogger(_appSettings.EngineSettings.LoggingValue1, Serilog.RollingInterval.Day,
                        _appSettings.EngineSettings.MinLogLevel);
                    break;
                case SinkType.HTTP:
                    engineLogger = new Logging().CreateHTTPLogger(projectName, _appSettings.EngineSettings.LoggingValue1, _appSettings.EngineSettings.MinLogLevel);
                    break;               
            }
            
            frmScriptEngine newEngine = new frmScriptEngine(configPath, engineLogger);
            newEngine.ShowDialog();

            if (Directory.Exists(newProjectPath))
                Directory.Delete(newProjectPath, true);

            newEngine.Dispose();
        }

        private void attendedScriptWatcher_Created(object sender, FileSystemEventArgs e)
        {
            LoadPublishedProjects();
        }

        private void attendedScriptWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            LoadPublishedProjects();
        }

        private void attendedScriptWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            LoadPublishedProjects();
        }

        private void attendedScriptWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            LoadPublishedProjects();
        }

        private void LoadPublishedProjects()
        {
            //clear project list
            cboSelectedProject.Items.Clear();

            //get project files
            var projectFiles = Directory.GetFiles(_publishedProjectsPath);

            //loop each file and add to potential
            foreach (var file in projectFiles)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Extension == ".nupkg")
                    cboSelectedProject.Items.Add(fileInfo.Name);
            }

            if (cboSelectedProject.Items.Count == 0)
                cboSelectedProject.Items.Add($"No published projects in '{_publishedProjectsPath}'");

            cboSelectedProject.Text = cboSelectedProject.Items[0].ToString();
        }
        #endregion

        #region Form Dragging
        private void frmAttendedMode_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
                Location = Point.Add(_dragFormPoint, new Size(dif));
            }
        }

        private void frmAttendedMode_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _dragCursorPoint = Cursor.Position;
            _dragFormPoint = Location;
        }

        private void frmAttendedMode_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        #endregion

        private void uiBtnSettings_Click(object sender, EventArgs e)
        {
            //show settings dialog
            frmSettings newSettings = new frmSettings(null);
            newSettings.ShowDialog();

            //reload app settings
            _appSettings = new ApplicationSettings().GetOrCreateApplicationSettings();

            newSettings.Dispose();
        }

        private void cboSelectedProject_MouseHover(object sender, EventArgs e)
        {
            var cboSelectedProject = (ComboBox)sender;
            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;
            toolTip.AutoPopDelay = 15000;
            toolTip.SetToolTip(cboSelectedProject, cboSelectedProject.Text);
        }
    }
}
