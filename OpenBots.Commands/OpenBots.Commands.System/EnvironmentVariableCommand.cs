﻿using Newtonsoft.Json;
using OpenBots.Core.Attributes.PropertyAttributes;
using OpenBots.Core.Command;
using OpenBots.Core.Infrastructure;
using OpenBots.Core.Properties;
using OpenBots.Core.Utilities.CommonUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenBots.Commands.System
{
    [Serializable]
	[Category("System Commands")]
	[Description("This command exclusively selects an environment variable.")]
	public class EnvironmentVariableCommand : ScriptCommand
	{
		[Required]
		[DisplayName("Environment Variable")]
		[Description("Select an evironment variable from one of the options.")]
		[SampleUsage("")]
		[Remarks("")]
		public string v_EnvVariableName { get; set; }

		[Required]
		[Editable(false)]
		[DisplayName("Output Environment Variable")]
		[Description("Create a new variable or select a variable from the list.")]
		[SampleUsage("{vUserVariable}")]
		[Remarks("Variables not pre-defined in the Variable Manager will be automatically generated at runtime.")]
		public string v_OutputUserVariableName { get; set; }

		[JsonIgnore]
		[Browsable(false)]
		private ComboBox _variableNameComboBox;

		[JsonIgnore]
		[Browsable(false)]
		private Label _variableValue;

		[JsonIgnore]
		[Browsable(false)]
		private string[] _excludedVariables = {
				"_NO_DEBUG_HEAP",
				"ENABLE_XAML_DIAGNOSTICS_SOURCE_INFO",
				"ForceIdentityAuthenticationType",
				"FPS_BROWSER_APP_PROFILE_STRING",
				"FPS_BROWSER_USER_PROFILE_STRING",
				"MSBuildLoadMicrosoftTargetsReadOnly",
				"PkgDefApplicationConfigFile",
				"ServiceHubLogSessionKey",
				"SESSIONNAME",
				"ThreadedWaitDialogDpiContext",
				"VisualStudioDir",
				"VisualStudioEdition",
				"VisualStudioVersion",
				"VSAPPIDDIR",
				"VSAPPIDNAME",
				"VSLANG",
				"VSSKUEDITION",
				"SignInWithHomeTenantOnly",
				"CLIENTNAME"
			};

		public EnvironmentVariableCommand()
		{
			CommandName = "EnvironmentVariableCommand";
			SelectionName = "Environment Variable";
			CommandEnabled = true;
			CommandIcon = Resources.command_system;

		}

		public override void RunCommand(object sender)
		{
			var engine = (IAutomationEngineInstance)sender;
			var environmentVariable = v_EnvVariableName.ConvertUserVariableToString(engine);
			
			var envVariables = Environment.GetEnvironmentVariables();
			var envDict = envVariables.Keys.Cast<object>().ToDictionary(k => k.ToString(), v => envVariables[v]);
			var filteredEnvDict = envDict.Where(kvp => !_excludedVariables.Contains(kvp.Key)).ToDictionary(k => k.Key, v => v.Value);

			var envValue = (string)filteredEnvDict[environmentVariable];

			envValue.StoreInUserVariable(engine, v_OutputUserVariableName);
		}

		public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
		{
			base.Render(editor, commandControls);

			var ActionNameComboBoxLabel = commandControls.CreateDefaultLabelFor("v_EnvVariableName", this);
			_variableNameComboBox = (ComboBox)commandControls.CreateDropdownFor("v_EnvVariableName", this);

			var envVariables = Environment.GetEnvironmentVariables();
			var envDict = envVariables.Keys.Cast<object>().ToDictionary(k => k.ToString(), v => envVariables[v]);
			var filteredEnvDict = envDict.Where(kvp => !_excludedVariables.Contains(kvp.Key)).ToDictionary(k => k.Key, v => v.Value);

			foreach (var env in filteredEnvDict)
			{
				var envVariableKey = env.Key.ToString();
				var envVariableValue = env.Value.ToString();
				_variableNameComboBox.Items.Add(envVariableKey);
			}

			_variableNameComboBox.SelectedValueChanged += VariableNameComboBox_SelectedValueChanged;
			RenderedControls.Add(ActionNameComboBoxLabel);
			RenderedControls.Add(_variableNameComboBox);

			_variableValue = new Label();
			_variableValue.Font = new Font("Segoe UI Semilight", 10, FontStyle.Bold);
			_variableValue.ForeColor = Color.White;
			RenderedControls.Add(_variableValue);

			RenderedControls.AddRange(commandControls.CreateDefaultOutputGroupFor("v_OutputUserVariableName", this, editor));          

			return RenderedControls;
		}

		private void VariableNameComboBox_SelectedValueChanged(object sender, EventArgs e)
		{
			var selectedValue = _variableNameComboBox.SelectedItem;

			if (selectedValue == null)
				return;

			var variable = Environment.GetEnvironmentVariables();
			var value = variable[selectedValue];

			_variableValue.Text = value.ToString();
		}

		public override string GetDisplayValue()
		{
			return base.GetDisplayValue() + $" [Store Environment Variable '{v_EnvVariableName}' in '{v_OutputUserVariableName}']";
		}
	}

}
