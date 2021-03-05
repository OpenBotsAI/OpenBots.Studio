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
	[Description("This command activates an open window and brings it to the front.")]
	public class ActivateWindowCommand : ScriptCommand
	{
		[Required]
		[DisplayName("Window Name")]
		[Description("Select the name of the window to activate and bring forward.")]
		[SampleUsage("Untitled - Notepad || {vWindow}")]
		[Remarks("")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		[Editor("CaptureWindowHelper", typeof(UIAdditionalHelperType))]
		public string v_WindowName { get; set; }

		[Required]
		[DisplayName("Timeout (Seconds)")]
		[Description("Specify how many seconds to wait before throwing an exception.")]
		[SampleUsage("30 || {vSeconds}")]
		[Remarks("")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		public string v_Timeout { get; set; }

		public ActivateWindowCommand()
		{
			CommandName = "ActivateWindowCommand";
			SelectionName = "Activate Window";
			CommandEnabled = true;
			CommandIcon = Resources.command_window;
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
					User32Functions.ActivateWindow(windowName);
					if (!User32Functions.GetActiveWindowTitle().Equals(windowName))
					{
						throw new Exception($"Window '{windowName}' Not Yet Found... ");
					}
					break;
				}
				catch (Exception)
                {
					engine.ReportProgress($"Window '{windowName}' Not Yet Found... "+ (timeToEnd - DateTime.Now).Minutes + "m, " + (timeToEnd - DateTime.Now).Seconds + "s remain");
					Thread.Sleep(500);
				}
            }
			if (!User32Functions.GetActiveWindowTitle().Equals(windowName))
			{
				throw new Exception($"Window '{windowName}' Not Found");
			}
		}

		public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
		{
			base.Render(editor, commandControls);

			RenderedControls.AddRange(commandControls.CreateDefaultWindowControlGroupFor("v_WindowName", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultWindowControlGroupFor("v_Timeout", this, editor));

			return RenderedControls;
		}     

		public override string GetDisplayValue()
		{
			return base.GetDisplayValue() + $" [Window '{v_WindowName}']";
		}
	}
}
