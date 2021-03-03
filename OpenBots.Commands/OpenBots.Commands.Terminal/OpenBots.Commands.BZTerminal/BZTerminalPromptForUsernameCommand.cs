﻿using OpenBots.Commands.Terminal.Library;
using OpenBots.Core.Attributes.PropertyAttributes;
using OpenBots.Core.Command;
using OpenBots.Core.Infrastructure;
using OpenBots.Core.Properties;
using OpenBots.Core.Utilities.CommonUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;

namespace OpenBots.Commands.BZTerminal
{
    [Serializable]
	[Category("BlueZone Terminal Commands")]
	[Description("This command prompts the user to provide a username in the terminal.")]
	public class BZTerminalPromptForUsernameCommand : ScriptCommand
	{
		[Required]
		[DisplayName("BZ Terminal Instance Name")]
		[Description("Enter the unique instance that was specified in the **Create BZ Terminal Session** command.")]
		[SampleUsage("MyBZTerminalInstance")]
		[Remarks("Failure to enter the correct instance or failure to first call the **Create BZ Terminal Session** command will cause an error.")]
		public string v_InstanceName { get; set; }

		public BZTerminalPromptForUsernameCommand()
		{
			CommandName = "BZTerminalPromptForUsernameCommand";
			SelectionName = "BZ Prompt For Username";
			CommandEnabled = true;
			CommandIcon = Resources.command_system;
			v_InstanceName = "DefaultBZTerminal";
		}

		public override void RunCommand(object sender)
		{
			var engine = (IAutomationEngineInstance)sender;
			var terminalContext = (BZTerminalContext)v_InstanceName.GetAppInstance(engine);

			if (terminalContext.BZTerminalObj == null || !terminalContext.BZTerminalObj.Connected)
				throw new Exception($"Terminal Instance {v_InstanceName} is not connected.");

			terminalContext.BZTerminalObj.TypeUserName();
		}

		public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
		{
			base.Render(editor, commandControls);

			RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_InstanceName", this, editor));

			return RenderedControls;
		}

		public override string GetDisplayValue()
		{
			return base.GetDisplayValue() + $" [Instance Name '{v_InstanceName}']";
		}     
	}
}