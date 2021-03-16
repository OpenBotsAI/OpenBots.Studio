using OpenBots.Core.Attributes.PropertyAttributes;
using OpenBots.Core.Command;
using OpenBots.Core.Enums;
using OpenBots.Core.Infrastructure;
using OpenBots.Core.Properties;
using OpenBots.Core.Utilities.CommonUtilities;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;

namespace OpenBots.Commands.Variable
{
	[Serializable]
	[Category("Variable Commands")]
	[Description("This command modifies a variable.")]
	public class SetVariableCommand : ScriptCommand
	{
		[Required]
		[DisplayName("Input Value")]
		[Description("Enter the input value for the variable.")]
		[SampleUsage("Hello || {vNum} || {vNum}+1")]
		[Remarks("You can use variables in input if you encase them within braces {vValue}. You can also perform basic math operations.")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		[CompatibleTypes(new Type[] { typeof(object) }, true)]
		public string v_Input { get; set; }

		[Required]
		[Editable(false)]
		[DisplayName("Output Data Variable")]
		[Description("Create a new variable or select a variable from the list.")]
		[SampleUsage("{vUserVariable}")]
		[Remarks("New variables/arguments may be instantiated by utilizing the Ctrl+K/Ctrl+J shortcuts.")]
		[CompatibleTypes(new Type[] { typeof(object) }, true)]
		public string v_OutputUserVariableName { get; set; }

		public SetVariableCommand()
		{
			CommandName = "SetVariableCommand";
			SelectionName = "Set Variable";
			CommandEnabled = true;
			CommandIcon = Resources.command_parse;
		}

		public override void RunCommand(object sender)
		{
			var engine = (IAutomationEngineInstance)sender;

			dynamic input = v_Input.ConvertUserVariableToString(engine);

			if (string.IsNullOrEmpty(input))
				input = null;

			if (input == v_Input && input.StartsWith("{") && input.EndsWith("}"))
            {
				if (v_Input.ConvertUserVariableToObject(engine, typeof(object)) != null)
					input = v_Input.ConvertUserVariableToObject(engine, typeof(object));
				else
					input = null;
			}
				
			if (input != null)
            {
				Type inputType = input.GetType();
				Type outputType = v_OutputUserVariableName.GetVarArgType(engine);

				if ((inputType == typeof(string) || inputType.IsPrimitive) && (outputType == typeof(string) || outputType.IsPrimitive))
				{
					var converter = TypeDescriptor.GetConverter(outputType);
					input = converter.ConvertFrom(input);
					inputType = input.GetType();
				}

				if (inputType != outputType)
					throw new InvalidCastException("Input and Output types do not match");
			}
			
			((object)input).StoreInUserVariable(engine, v_OutputUserVariableName, nameof(v_OutputUserVariableName), this);
		}

		public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
		{
			base.Render(editor, commandControls);

			RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_Input", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultOutputGroupFor("v_OutputUserVariableName", this, editor));

			return RenderedControls;
		}

		public override string GetDisplayValue()
		{
			return base.GetDisplayValue() + $" [Set '{v_Input}' to Variable '{v_OutputUserVariableName}']";
		}
	}
}