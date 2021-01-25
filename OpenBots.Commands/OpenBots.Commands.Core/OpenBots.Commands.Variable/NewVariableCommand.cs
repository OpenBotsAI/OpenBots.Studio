﻿using OpenBots.Core.Attributes.PropertyAttributes;
using OpenBots.Core.Command;
using OpenBots.Core.Enums;
using OpenBots.Core.Infrastructure;
using OpenBots.Core.Properties;
using OpenBots.Core.Script;
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
	[Description("This command adds a new variable or updates an existing variable.")]
	public class NewVariableCommand : ScriptCommand
	{

		[Required]
		[DisplayName("New Variable Name")]
		[Description("Indicate a unique reference name for later use.")]
		[SampleUsage("vSomeVariable")]
		[Remarks("")]
		public string v_VariableName { get; set; }

		[Required]
		[DisplayName("Input Value")]
		[Description("Enter the value for the variable.")]
		[SampleUsage("Hello || {vNum} || {vNum}+1")]
		[Remarks("You can use variables in input if you encase them within braces {vSomeValue}. You can also perform basic math operations.")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		public string v_Input { get; set; }

		[Required]
		[DisplayName("Additional Actions")]
		[PropertyUISelectionOption("Do Nothing If Variable Exists")]
		[PropertyUISelectionOption("Error If Variable Exists")]
		[PropertyUISelectionOption("Replace If Variable Exists")]
		[Description("Select an action to take if the variable already exists.")]
		[SampleUsage("")]
		[Remarks("")]
		public string v_IfExists { get; set; }

		public NewVariableCommand()
		{
			CommandName = "NewVariableCommand";
			SelectionName = "New Variable";
			CommandEnabled = true;
			CommandIcon = Resources.command_parse;

			v_IfExists = "Error If Variable Exists";
		}

		public override void RunCommand(object sender)
		{
			//get sending instance
			var engine = (IAutomationEngineInstance)sender;
			var variable = ("{" + v_VariableName + "}").ConvertUserVariableToObject(engine); 

			dynamic input = v_Input.ConvertUserVariableToString(engine);

			if (input == v_Input && input.StartsWith("{") && input.EndsWith("}"))
				if (v_Input.ConvertUserVariableToObject(engine) != null)
					input = v_Input.ConvertUserVariableToObject(engine);

			if (variable == null)
			{
				//variable does not exist so add to the list
				try
				{
					engine.AutomationEngineContext.Variables.Add(new ScriptVariable
					{
						VariableName = v_VariableName,
						VariableValue = (object)input
					});
				}
				catch (Exception ex)
				{
					throw new Exception("Encountered an error when adding variable '" + v_VariableName + "': " + ex.ToString());
				}
			}
			else
			{
				//variable exists so decide what to do
				switch (v_IfExists)
				{
					case "Replace If Variable Exists":
						((object)input).StoreInUserVariable(engine, "{" + v_VariableName + "}");
						break;
					case "Error If Variable Exists":
						throw new Exception("Attempted to create a variable that already exists! Use 'Set Variable' instead or change the Exception Setting in the 'Add Variable' Command.");
					default:
						break;
				}
			}
		}

		public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
		{
			base.Render(editor, commandControls);

			RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_VariableName", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_Input", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultDropdownGroupFor("v_IfExists", this, editor));

			return RenderedControls;
		}

		public override string GetDisplayValue()
		{
			return base.GetDisplayValue() + $" [Assign '{v_Input}' to New Variable '{v_VariableName}']";
		}
	}
}