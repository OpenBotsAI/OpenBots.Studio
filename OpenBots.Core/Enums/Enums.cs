using System.ComponentModel;

namespace OpenBots.Core.Enums
{
    public enum WindowState
    {
        [Description("Minimizes a window, even if the thread that owns the window is not responding. This flag should only be used when minimizing windows from a different thread.")]
        SwForceMinimize = 11,
        [Description("Hides the window and activates another window.")]
        SwHide = 0,
        [Description("Maximizes the specified window.")]
        SwMaximize = 3,
        [Description("Minimizes the specified window and activates the next top-level window in the Z order.")]
        SwMinimize = 6,
        [Description("Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.")]
        SwRestore = 9,
        [Description("Activates the window and displays it in its current size and position.")]
        SwShow = 5,
        [Description("Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application.")]
        SwShowDefault = 10,
        [Description("Activates the window and displays it as a maximized window.")]
        SwShowMaximized = 3,
        [Description("Activates the window and displays it as a minimized window.")]
        SwShowMinimized = 2,
        [Description("Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.")]
        SwShowMinNoActive = 7,
        [Description("Displays the window in its current size and position. This value is similar to SW_SHOW, except that the window is not activated.")]
        SwShowNa = 8,
        [Description("Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except that the window is not activated.")]
        SwShowNoActivate = 4,
        [Description("Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.")]
        SwShowNormal = 1,
    }

    public enum FolderType
    {
        RootFolder,
        SettingsFolder,
        ScriptsFolder,
        LogFolder,
        TempFolder,
        AttendedTasksFolder,
        PublishedFolder,
        LocalAppDataPackagesFolder,
        ProgramFilesPackagesFolder,
    }

    public enum UIAdditionalHelperType
    {
        ShowVariableHelper,
        ShowElementHelper,
        ShowFileSelectionHelper,
        ShowFolderSelectionHelper,
        ShowImageCaptureHelper,
        ShowImageRecognitionTestHelper,
        ShowCodeBuilder,
        ShowMouseCaptureHelper,
        ShowElementRecorder,
        GenerateDLLParameters,
        ShowDLLExplorer,
        AddInputParameter,
        ShowHTMLBuilder,
        ShowIfBuilder,
        ShowLoopBuilder,
        ShowEncryptionHelper,
        CaptureWindowHelper
    }

    public enum ScriptFinishedResult
    {
        Successful, 
        Error, 
        Cancelled
    }

    public enum CreationMode
    {
        Add,
        Edit
    }

    public enum DialogType
    {
        YesNo,
        OkCancel,
        OkOnly,
        CancelOnly
    }

    public enum SinkType
    {
        File,
        HTTP
    }
    public enum MouseMessages
    {
        WmLButtonDown = 0x0201,
        WmLButtonUp = 0x0202,
        WmMouseMove = 0x0200,
        WmMouseWheel = 0x020A,       
        WmRButtonDown = 0x0204,
        WmRButtonUp = 0x0205,
        WmMButtonDown = 0x0207,
        WmMButtonUp = 0x0208,
    }

    public enum ProjectType
    {
        OpenBots,
        Python,
        TagUI,
        CSScript
    }

    public enum ScriptArgumentDirection
    {
        In,
        Out,
        InOut
    }
}
