﻿using Newtonsoft.Json;
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
using System.IO;
using CSScriptLibrary;
using Diagnostics = System.Diagnostics;
using System.Data;
using OBFile = System.IO.File;
namespace OpenBots.Commands.Process
{
	[Serializable]
	[Category("Programs/Process Commands")]
	[Description("This command runs a C# script and waits for it to exit before proceeding.")]

	public class RunCSharpScriptCommand : ScriptCommand
	{

		[Required]
		[DisplayName("Script Path")]
		[Description("Enter a fully qualified path to the script, including the script extension.")]
		[SampleUsage(@"C:\temp\myscript.ps1 || {vScriptPath} || {ProjectPath}\myscript.ps1")]
		[Remarks("This command differs from *Start Process* because this command blocks execution until the script has completed. " +
				 "If you do not want to stop while the script executes, consider using *Start Process* instead.")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		[Editor("ShowFileSelectionHelper", typeof(UIAdditionalHelperType))]
		[CompatibleTypes(null, true)]
		public string v_ScriptPath { get; set; }

		[Required]
		[DisplayName("Argument Style")]
		[PropertyUISelectionOption("In-Studio Variables")]
		[PropertyUISelectionOption("Command Line")]
		[Description("Whether to pass a string[] (command line) or object[] (variables) to your script.")]
		[SampleUsage("")]
		[Remarks("")]
		public string v_ArgumentType { get; set; }

		[DisplayName("Argument Values (Optional)")]
		[Description("Enter the values to pass into the script.")]
		[SampleUsage("hello || {vValue}")]
		[Remarks("This input is passed to your script as a object[].")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		[CompatibleTypes(new Type[] { typeof(object) }, true)]
		public DataTable v_VariableArgumentsDataTable { get; set; }

		[DisplayName("Command Line Arguments (Optional)")]
		[Description("Enter any arguments as a single string.")]
		[SampleUsage("-message Hello -t 2 || {vArguments}")]
		[Remarks("This input is passed to your script as a string[].")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		[CompatibleTypes(null, true)]
		public string v_ScriptArgs { get; set; }

		[Required]
		[DisplayName("Script Has Output")]
		[PropertyUISelectionOption("Yes")]
		[PropertyUISelectionOption("No")]
		[Description("Whether the Main function of the script returns a value.")]
		[SampleUsage("")]
		[Remarks("")]
		public string v_HasOutput { get; set; }

		[Required]
		[Editable(false)]
		[DisplayName("Output Script Result Variable")]
		[Description("Create a new variable or select a variable from the list.")]
		[SampleUsage("vUserVariable")]
		[Remarks("Variables not pre-defined in the Variable Manager will be automatically generated at runtime.")]
		[CompatibleTypes(new Type[] { typeof(object) })]
		public string v_OutputUserVariableName { get; set; }

		[JsonIgnore]
		[Browsable(false)]
		private List<Control> _variableInputControls;

		[JsonIgnore]
		[Browsable(false)]
		private List<Control> _commandLineInputControls;

		[JsonIgnore]
		[Browsable(false)]
		private List<Control> _outputControls;

		public RunCSharpScriptCommand()
		{
			CommandName = "RunCSharpScriptCommand";
			SelectionName = "Run C# Script";
			CommandEnabled = true;
			CommandIcon = Resources.command_script;

			//initialize data table
			v_VariableArgumentsDataTable = new DataTable
			{
				TableName = "VariableArgumentsDataTable" + DateTime.Now.ToString("MMddyy.hhmmss")
			};

			v_ArgumentType = "In-Studio Variables";
			v_HasOutput = "No";

			v_VariableArgumentsDataTable.Columns.Add("Argument Values");
		}

		public override void RunCommand(object sender)
		{
			var engine = (IAutomationEngineInstance)sender;

			string scriptPath = v_ScriptPath.ConvertUserVariableToString(engine);

			string code = OBFile.ReadAllText(scriptPath);
			dynamic script = CSScript.LoadCode(code).CreateObject("*");

			if (v_ArgumentType == "In-Studio Variables")
			{
				object[] args = new object[v_VariableArgumentsDataTable.Rows.Count];
				int i = 0;
				foreach (DataRow varColumn in v_VariableArgumentsDataTable.Rows)
                {
					string var = varColumn.Field<string>("Argument Values").Trim();
					if (var.Contains("{"))
						args[i] = var.ConvertUserVariableToString(engine);
						if (var.ConvertUserVariableToObject(engine, typeof(object)) != null)
							args[i] = var.ConvertUserVariableToObject(engine, typeof(object));
					else
						args[i] = var;
					i++;
				}

				if (v_HasOutput == "No")
					script.Main(args);
				else
				{
					object result = script.Main(args);
					
					result.StoreInUserVariable(engine, v_OutputUserVariableName, nameof(v_OutputUserVariableName), this);
				}
			}
			else if (v_ArgumentType == "Command Line")
			{
				string scriptArgs = v_ScriptArgs.ConvertUserVariableToString(engine);
				string[] argStrings = scriptArgs.Trim().Split(' ');
				if (v_HasOutput == "No")
					script.Main(argStrings);
				else
				{
					object result = script.Main(argStrings);
					result.StoreInUserVariable(engine, v_OutputUserVariableName, nameof(v_OutputUserVariableName), this);
				}
			}
		}

		public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
		{
			base.Render(editor, commandControls);

			RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_ScriptPath", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultDropdownGroupFor("v_ArgumentType", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultDropdownGroupFor("v_HasOutput", this, editor));
			((ComboBox)RenderedControls[7]).SelectedIndexChanged += HasOutputComboBox_SelectedIndexChanged;
			((ComboBox)RenderedControls[5]).SelectedIndexChanged += ArgumentTypeComboBox_SelectedIndexChanged;

			_variableInputControls = commandControls.CreateDefaultDataGridViewGroupFor("v_VariableArgumentsDataTable", this, editor);

			_commandLineInputControls = commandControls.CreateDefaultInputGroupFor("v_ScriptArgs", this, editor);

			_outputControls = commandControls.CreateDefaultOutputGroupFor("v_OutputUserVariableName", this, editor);

			foreach (var ctrl in _outputControls)
				ctrl.Visible = false;

			foreach (var ctrl in _commandLineInputControls)
				ctrl.Visible = false;

			RenderedControls.AddRange(_variableInputControls);
			RenderedControls.AddRange(_commandLineInputControls);
			RenderedControls.AddRange(_outputControls);


			return RenderedControls;
		}

		public override string GetDisplayValue()
		{
			return base.GetDisplayValue() + $" [C# Script Path '{v_ScriptPath}']";
		}

		private void ArgumentTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (((ComboBox)RenderedControls[5]).Text == "In-Studio Variables")
			{
				foreach (var ctrl in _variableInputControls)
					ctrl.Visible = true;

				foreach (var ctrl in _commandLineInputControls) {
					ctrl.Visible = false;
					if (ctrl is TextBox)
						((TextBox)ctrl).Clear();
				}
			}
			else
			{
				foreach (var ctrl in _variableInputControls)
				{
					ctrl.Visible = false;
					if (ctrl is DataGridView)
						((DataGridView)ctrl).ClearSelection();
				}
				foreach (var ctrl in _commandLineInputControls)
				{
					ctrl.Visible = true;
				}
			}
		}

		private void HasOutputComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (((ComboBox)RenderedControls[7]).Text == "Yes")
			{
				foreach (var ctrl in _outputControls)
					ctrl.Visible = true;
			}
			else
			{
				foreach (var ctrl in _outputControls)
				{
					ctrl.Visible = false;
					if (ctrl is TextBox)
						((TextBox)ctrl).Clear();
				}
			}
		}
	}
}
