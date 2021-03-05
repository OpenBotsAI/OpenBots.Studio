using OpenBots.Core.Attributes.PropertyAttributes;
using OpenBots.Core.Command;
using OpenBots.Core.Enums;
using OpenBots.Core.Infrastructure;
using OpenBots.Core.Properties;
using OpenBots.Core.User32;
using OpenBots.Core.Utilities.CommonUtilities;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using System.Threading;

namespace OpenBots.Commands.Window
{
	[Serializable]
	[Category("Window Commands")]
	[Description("This command sets a open window's state.")]
	public class SetWindowStateCommand : ScriptCommand
	{
		[Required]
		[DisplayName("Window Name")]
		[Description("Select the name of the window to set.")]
		[SampleUsage("Untitled - Notepad || Current Window || {vWindow}")]
		[Remarks("")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		[Editor("CaptureWindowHelper", typeof(UIAdditionalHelperType))]
		public string v_WindowName { get; set; }

		[Required]
		[DisplayName("Window State")]
		[PropertyUISelectionOption("Maximize")]
		[PropertyUISelectionOption("Minimize")]
		[PropertyUISelectionOption("Restore")]
		[Description("Select the appropriate window state.")]
		[SampleUsage("")]
		[Remarks("")]
		public string v_WindowState { get; set; }

		[Required]
		[DisplayName("Timeout (Seconds)")]
		[Description("Specify how many seconds to wait before throwing an exception.")]
		[SampleUsage("30 || {vSeconds}")]
		[Remarks("")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		public string v_Timeout { get; set; }

		public SetWindowStateCommand()
		{
			CommandName = "SetWindowStateCommand";
			SelectionName = "Set Window State";
			CommandEnabled = true;
			CommandIcon = Resources.command_window;


			v_WindowName = "Current Window";
			v_WindowState = "Maximize";
			v_Timeout = "30";
		}

		public override void RunCommand(object sender)
		{
			var engine = (IAutomationEngineInstance)sender;
			//convert window name
			string windowName = v_WindowName.ConvertUserVariableToString(engine);
			int timeout = Int32.Parse(v_Timeout);
			DateTime timeToEnd = DateTime.Now.AddSeconds(timeout);

			List<IntPtr> targetWindows = new List<IntPtr>();
			while (timeToEnd >= DateTime.Now)
			{
				try
				{
					if (engine.IsCancellationPending)
						break;
					targetWindows = User32Functions.FindTargetWindows(windowName);
					if (targetWindows.Count == 0)
					{
						throw new Exception($"Window '{windowName}' Not Yet Found... ");
					}
					break;
				}
				catch (Exception)
				{
					engine.ReportProgress($"Window '{windowName}' Not Yet Found... " + (timeToEnd - DateTime.Now).Minutes + "m, " + (timeToEnd - DateTime.Now).Seconds + "s remain");
					Thread.Sleep(500);
				}
			}
			targetWindows = User32Functions.FindTargetWindows(windowName);

			//loop each window and set the window state
			foreach (var targetedWindow in targetWindows)
			{
				WindowState WINDOW_STATE = WindowState.SwShowNormal;
				switch (v_WindowState)
				{
					case "Maximize":
						WINDOW_STATE = WindowState.SwMaximize;
						break;

					case "Minimize":
						WINDOW_STATE = WindowState.SwMinimize;
						break;

					case "Restore":
						WINDOW_STATE = WindowState.SwRestore;
						break;

					default:
						break;
				}

				User32Functions.SetWindowState(targetedWindow, WINDOW_STATE);
			}     
		}

		public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
		{
			base.Render(editor, commandControls);

			RenderedControls.AddRange(commandControls.CreateDefaultWindowControlGroupFor("v_WindowName", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultDropdownGroupFor("v_WindowState", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultWindowControlGroupFor("v_Timeout", this, editor));

			return RenderedControls;
		}

		public override string GetDisplayValue()
		{
			return base.GetDisplayValue() + $" [Window '{v_WindowName}' - Window State '{v_WindowState}']";
		}
	}
}