﻿using OpenBots.NetCore.Core.Attributes.PropertyAttributes;
using OpenBots.NetCore.Core.Command;
using OpenBots.NetCore.Core.Enums;
using OpenBots.NetCore.Core.Infrastructure;
using OpenBots.NetCore.Core.Properties;
using OpenBots.NetCore.Core.Utilities.CommonUtilities;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Windows.Forms;

namespace OpenBots.NetCore.Commands.Engine
{
	[Serializable]
	[Category("Engine Commands")]
	[Description("This command pauses the script for a set amount of time specified in milliseconds.")]
	public class PauseScriptCommand : ScriptCommand
	{
		[Required]
		[DisplayName("Pause Time (Milliseconds)")]
		[Description("Select or provide a specific amount of time in milliseconds.")]
		[SampleUsage("1000 || {vTime}")]
		[Remarks("")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		[CompatibleTypes(null, true)]
		public string v_PauseLength { get; set; }

		public PauseScriptCommand()
		{
			CommandName = "PauseScriptCommand";
			SelectionName = "Pause Script";
			CommandEnabled = true;
			CommandIcon = Resources.command_pause;

			v_PauseLength = "1000";
		}

		public override void RunCommand(object sender)
		{
			var engine = (IAutomationEngineInstance)sender;
			var userPauseLength = v_PauseLength.ConvertUserVariableToString(engine);
			var pauseLength = int.Parse(userPauseLength);
			Thread.Sleep(pauseLength);
		}

		public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
		{
			base.Render(editor, commandControls);

			RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_PauseLength", this, editor));

			return RenderedControls;
		}

		public override string GetDisplayValue()
		{
			return base.GetDisplayValue() + $" [Pause for '{v_PauseLength} ms']";
		}
	}
}