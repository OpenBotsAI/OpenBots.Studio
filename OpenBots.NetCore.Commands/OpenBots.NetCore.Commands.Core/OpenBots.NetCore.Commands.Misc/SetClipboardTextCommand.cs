﻿using OpenBots.NetCore.Core.Attributes.PropertyAttributes;
using OpenBots.NetCore.Core.Command;
using OpenBots.NetCore.Core.Enums;
using OpenBots.NetCore.Core.Infrastructure;
using OpenBots.NetCore.Core.Properties;
using OpenBots.NetCore.Core.User32;
using OpenBots.NetCore.Core.Utilities.CommonUtilities;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;

namespace OpenBots.NetCore.Commands.Misc
{
	[Serializable]
	[Category("Misc Commands")]
	[Description("This command sets text to the user's clipboard.")]
	public class SetClipboardTextCommand : ScriptCommand
	{

		[Required]
		[DisplayName("Text")]
		[Description("Select or provide the text to set on the clipboard.")]
		[SampleUsage("Hello || {vTextToSet}")]
		[Remarks("")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		[CompatibleTypes(null, true)]
		public string v_TextToSet { get; set; }

		public SetClipboardTextCommand()
		{
			CommandName = "SetClipboardTextCommand";
			SelectionName = "Set Clipboard Text";
			CommandEnabled = true;
			CommandIcon = Resources.command_files;

		}

		public override void RunCommand(object sender)
		{
			var engine = (IAutomationEngineInstance)sender;
			var input = v_TextToSet.ConvertUserVariableToString(engine);

			User32Functions.SetClipboardText(input);
		}

		public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
		{
			base.Render(editor, commandControls);

			RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_TextToSet", this, editor));

			return RenderedControls;
		}

		public override string GetDisplayValue()
		{
			return base.GetDisplayValue() + $" [Text '{v_TextToSet}']";
		}
	}
}
