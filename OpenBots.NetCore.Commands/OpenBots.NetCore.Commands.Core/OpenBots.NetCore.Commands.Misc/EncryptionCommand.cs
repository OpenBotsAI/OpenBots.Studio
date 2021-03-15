﻿using OpenBots.NetCore.Core.Attributes.PropertyAttributes;
using OpenBots.NetCore.Core.Command;
using OpenBots.NetCore.Core.Enums;
using OpenBots.NetCore.Core.Infrastructure;
using OpenBots.NetCore.Core.Properties;
using OpenBots.NetCore.Core.Utilities.CommandUtilities;
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
	[Description("This command encrypts or decrypts text.")]
	public class EncryptionCommand : ScriptCommand
	{

		[Required]
		[DisplayName("Encryption Action")]
		[PropertyUISelectionOption("Encrypt")]
		[PropertyUISelectionOption("Decrypt")]
		[Description("Select the appropriate action to take.")]
		[SampleUsage("")]
		[Remarks("")]
		public string v_EncryptionType { get; set; }

		[Required]
		[DisplayName("Text")]
		[Description("Select or provide the text to encrypt/decrypt.")]
		[SampleUsage("Hello || {vText}")]
		[Remarks("")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		[CompatibleTypes(null, true)]
		public string v_InputValue { get; set; }

		[Required]
		[DisplayName("Pass Phrase")]
		[Description("Select or provide a pass phrase for encryption/decryption.")]
		[SampleUsage("OPENBOTS || {vPassPhrase}")]
		[Remarks("If decrypting, provide the pass phrase used to encypt the original text.")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		[CompatibleTypes(null, true)]
		public string v_PassPhrase { get; set; }

		[Required]
		[Editable(false)]
		[DisplayName("Output Result Variable")]
		[Description("Create a new variable or select a variable from the list.")]
		[SampleUsage("{vUserVariable}")]
		[Remarks("Variables not pre-defined in the Variable Manager will be automatically generated at runtime.")]
		[CompatibleTypes(new Type[] { typeof(string) })]
		public string v_OutputUserVariableName { get; set; }

		public EncryptionCommand()
		{
			CommandName = "EncryptionCommand";
			SelectionName = "Encryption Command";
			CommandEnabled = true;
			CommandIcon = Resources.command_password;

			v_EncryptionType = "Encrypt";
		}

		public override void RunCommand(object sender)
		{
			var engine = (IAutomationEngineInstance)sender;

			var variableInput = v_InputValue.ConvertUserVariableToString(engine);
			var passphrase = v_PassPhrase.ConvertUserVariableToString(engine);

			string resultData = "";
			if (v_EncryptionType == "Encrypt")
				resultData = EncryptionServices.EncryptString(variableInput, passphrase);
			else if (v_EncryptionType == "Decrypt")
				resultData = EncryptionServices.DecryptString(variableInput, passphrase);

			resultData.StoreInUserVariable(engine, v_OutputUserVariableName, nameof(v_OutputUserVariableName), this);
		}

		public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
		{
			base.Render(editor, commandControls);

			RenderedControls.AddRange(commandControls.CreateDefaultDropdownGroupFor("v_EncryptionType", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_InputValue", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultPasswordInputGroupFor("v_PassPhrase", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultOutputGroupFor("v_OutputUserVariableName", this, editor));

			return RenderedControls;
		}

		public override string GetDisplayValue()
		{
			return base.GetDisplayValue() + $" [{v_EncryptionType} '{v_InputValue}' - Store Result in '{v_OutputUserVariableName}']";
		}
	}
}