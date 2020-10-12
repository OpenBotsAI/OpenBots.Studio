﻿using Serilog.Core;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OpenBots.Core.Enums;
using OpenBots.Core.IO;
using OpenBots.Core.Settings;
using OpenBots.Core.Utilities.CommonUtilities;
using OpenBots.Core.UI.Forms;

namespace OpenBots.UI.Forms
{
    public partial class frmAttendedMode : UIForm
    {
        #region Variables
        private ApplicationSettings _appSettings;
        private int _flashCount = 0;
        private bool _dragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        private string _projectPath;

        #endregion

        #region Form Events
        public frmAttendedMode(string projectPath)
        {
            _projectPath = projectPath;
            InitializeComponent();
        }

        private void frmAttendedMode_Load(object sender, EventArgs e)
        {
            //get app settings
            _appSettings = new ApplicationSettings().GetOrCreateApplicationSettings();

            //setup file system watcher
            attendedScriptWatcher.Path = _projectPath

            //move form to default location
            MoveToDefaultFormLocation();

            //load scripts to be used for attended automation
            LoadAttendedScripts();
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
            //build script path and execute
            var scriptFilePath = Path.Combine(_projectPath, cboSelectedScript.Text);
            var projectName = new DirectoryInfo(_projectPath).Name;
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
                case SinkType.SignalR:
                    string[] groupNames = _appSettings.EngineSettings.LoggingValue3.Split(',').Select(x => x.Trim()).ToArray();
                    string[] userIDs = _appSettings.EngineSettings.LoggingValue4.Split(',').Select(x => x.Trim()).ToArray();

                    engineLogger = new Logging().CreateSignalRLogger(projectName, _appSettings.EngineSettings.LoggingValue1, _appSettings.EngineSettings.LoggingValue2, 
                        groupNames, userIDs, _appSettings.EngineSettings.MinLogLevel);
                    break;
            }
            
            frmScriptEngine newEngine = new frmScriptEngine(scriptFilePath, _projectPath, null, engineLogger);
            newEngine.Show();
        }

        private void attendedScriptWatcher_Created(object sender, FileSystemEventArgs e)
        {
            LoadAttendedScripts();
        }

        private void LoadAttendedScripts()
        {
            //clear script list
            cboSelectedScript.Items.Clear();
        
            //get script files
            var files = Directory.GetFiles(_projectPath);

            //loop each file and add to potential
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Extension == ".json")
                    cboSelectedScript.Items.Add(fileInfo.Name);
            }

            cboSelectedScript.Text = cboSelectedScript.Items[0].ToString();
        }
        #endregion

        #region Flashing Animation
        private void frmAttendedMode_Shown(object sender, EventArgs e)
        {
            tmrBackColorFlash.Enabled = true;
        }

        private void tmrBackColorFlash_Tick(object sender, EventArgs e)
        {
            if (BackColor == Color.FromArgb(59, 59, 59))
            {
                BackColor = Color.LightYellow;
                uiBtnClose.DisplayTextBrush = Color.Black;
                uiBtnRun.DisplayTextBrush = Color.Black;
            }
            else
            {
                BackColor = Color.FromArgb(59, 59, 59);
                uiBtnClose.DisplayTextBrush = Color.White;
                uiBtnRun.DisplayTextBrush = Color.White;
            }

            _flashCount++;

            if (_flashCount == 6)
            {
                tmrBackColorFlash.Enabled = false;
            }
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
    }
}
