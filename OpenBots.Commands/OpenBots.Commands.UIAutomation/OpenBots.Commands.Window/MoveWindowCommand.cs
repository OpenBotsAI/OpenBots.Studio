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
	[Description("This command moves an open window to a specified location on screen.")]
	public class MoveWindowCommand : ScriptCommand
	{
		[Required]
		[DisplayName("Window Name")]
		[Description("Select the name of the window to move.")]
		[SampleUsage("Untitled - Notepad || Current Window || {vWindow}")]
		[Remarks("")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		[Editor("CaptureWindowHelper", typeof(UIAdditionalHelperType))]
		public string v_WindowName { get; set; }

		[Required]
		[DisplayName("X Position")]
		[Description("Input the new horizontal coordinate of the window. Starts from 0 on the left and increases going right.")]
		[SampleUsage("0 || {vXPosition}")]
		[Remarks("This number is the pixel location on screen. Maximum value should be the maximum value allowed by your resolution. For 1920x1080, the valid range would be 0-1920.")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		[Editor("ShowMouseCaptureHelper", typeof(UIAdditionalHelperType))]
		public string v_XMousePosition { get; set; }

		[Required]
		[DisplayName("Y Position")]
		[Description("Input the new vertical coordinate of the window. Starts from 0 at the top and increases going down.")]
		[SampleUsage("0 || {vYPosition}")]
		[Remarks("This number is the pixel location on screen. Maximum value should be the maximum value allowed by your resolution. For 1920x1080, the valid range would be 0-1080.")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		[Editor("ShowMouseCaptureHelper", typeof(UIAdditionalHelperType))]
		public string v_YMousePosition { get; set; }

		[Required]
		[DisplayName("Timeout (Seconds)")]
		[Description("Specify how many seconds to wait before throwing an exception.")]
		[SampleUsage("30 || {vSeconds}")]
		[Remarks("")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		public string v_Timeout { get; set; }

		public MoveWindowCommand()
		{
			CommandName = "MoveWindowCommand";
			SelectionName = "Move Window";
			CommandEnabled = true;
			CommandIcon = Resources.command_window;


			v_WindowName = "Current Window";
			v_XMousePosition = "0";
			v_YMousePosition = "0";
			v_Timeout = "30";
		}

		public override void RunCommand(object sender)
		{
			var engine = (IAutomationEngineInstance)sender;
			string windowName = v_WindowName.ConvertUserVariableToString(engine);
			int timeout = Int32.Parse(v_Timeout);
			DateTime timeToEnd = DateTime.Now.AddSeconds(timeout);
			while (timeToEnd >= DateTime.Now)
            {
				try
				{
					if (engine.IsCancellationPending)
						break;
					List<IntPtr> targetWindows = User32Functions.FindTargetWindows(windowName);
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

			var variableXPosition = v_XMousePosition.ConvertUserVariableToString(engine);
			var variableYPosition = v_YMousePosition.ConvertUserVariableToString(engine);

			User32Functions.MoveWindow(windowName, variableXPosition, variableYPosition);
		}

		public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
		{
			base.Render(editor, commandControls);

			RenderedControls.AddRange(commandControls.CreateDefaultWindowControlGroupFor("v_WindowName", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_XMousePosition", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_YMousePosition", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultWindowControlGroupFor("v_Timeout", this, editor));

			return RenderedControls;
		}

		public override string GetDisplayValue()
		{
			return base.GetDisplayValue() + $" [Window '{v_WindowName}' - Target Coordinates '({v_XMousePosition},{v_YMousePosition})']";
		}
	}
}