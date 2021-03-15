﻿using Newtonsoft.Json;
using OpenBots.NetCore.Core.Attributes.PropertyAttributes;
using OpenBots.NetCore.Core.Command;
using OpenBots.NetCore.Core.Enums;
using OpenBots.NetCore.Core.Infrastructure;
using OpenBots.NetCore.Core.Properties;
using OpenBots.NetCore.Core.Utilities.CommonUtilities;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Forms;

namespace OpenBots.NetCore.Commands.Engine
{
	[Serializable]
	[Category("Engine Commands")]
	[Description("This command measures time elapsed during the execution of the process.")]
	public class StopwatchCommand : ScriptCommand
	{
		[Required]
		[DisplayName("Stopwatch Instance Name")]
		[Description("Enter a unique name that will represent the application instance.")]
		[SampleUsage("MyStopwatchInstance")]
		[Remarks("This unique name allows you to refer to the instance by name in future commands, " +
				 "ensuring that the commands you specify run against the correct application.")]
		[CompatibleTypes(new Type[] { typeof(Stopwatch) })]
		public string v_InstanceName { get; set; }

		[Required]
		[DisplayName("Stopwatch Action")]
		[PropertyUISelectionOption("Start Stopwatch")]
		[PropertyUISelectionOption("Stop Stopwatch")]
		[PropertyUISelectionOption("Restart Stopwatch")]
		[PropertyUISelectionOption("Reset Stopwatch")]
		[PropertyUISelectionOption("Measure Stopwatch")]
		[Description("Select the appropriate stopwatch action.")]
		[SampleUsage("")]
		[Remarks("")]
		public string v_StopwatchAction { get; set; }

		[DisplayName("String Format (Optional)")]
		[Description("Specify a TimeSpan string format if required.")]
		[SampleUsage("g || dd\\.hh\\:mm || {vFormat}")]
		[Remarks("This input is optional.")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		[CompatibleTypes(null, true)]
		public string v_ToStringFormat { get; set; }

		[Required]
		[Editable(false)]
		[DisplayName("Output Elapsed Time Variable")]
		[Description("Create a new variable or select a variable from the list.")]
		[SampleUsage("{vUserVariable}")]
		[Remarks("Variables not pre-defined in the Variable Manager will be automatically generated at runtime.")]
		[CompatibleTypes(new Type[] { typeof(string) })]
		public string v_OutputUserVariableName { get; set; }

		[JsonIgnore]
		[Browsable(false)]
		private List<Control> _measureControls;

		public StopwatchCommand()
		{
			CommandName = "StopwatchCommand";
			SelectionName = "Stopwatch";
			CommandEnabled = true;
			CommandIcon = Resources.command_stopwatch;

			v_InstanceName = "DefaultStopwatch";
			v_StopwatchAction = "Start Stopwatch";
		}

		public override void RunCommand(object sender)
		{
			var engine = (IAutomationEngineInstance)sender;
			var format = v_ToStringFormat.ConvertUserVariableToString(engine);
			
			Stopwatch stopwatch;
			switch (v_StopwatchAction)
			{
				case "Start Stopwatch":
					//start a new stopwatch
					stopwatch = new Stopwatch();
					stopwatch.AddAppInstance(engine, v_InstanceName);
					stopwatch.Start();
					break;
				case "Stop Stopwatch":
					//stop existing stopwatch
					stopwatch = (Stopwatch)engine.AutomationEngineContext.AppInstances[v_InstanceName];
					stopwatch.Stop();
					break;
				case "Restart Stopwatch":
					//restart which sets to 0 and automatically starts
					stopwatch = (Stopwatch)engine.AutomationEngineContext.AppInstances[v_InstanceName];
					stopwatch.Restart();
					break;
				case "Reset Stopwatch":
					//reset which sets to 0
					stopwatch = (Stopwatch)engine.AutomationEngineContext.AppInstances[v_InstanceName];
					stopwatch.Reset();
					break;
				case "Measure Stopwatch":
					//check elapsed which gives measure
					stopwatch = (Stopwatch)engine.AutomationEngineContext.AppInstances[v_InstanceName];
					string elapsedTime;
					if (string.IsNullOrEmpty(format))
						elapsedTime = stopwatch.Elapsed.ToString();
					else
						elapsedTime = stopwatch.Elapsed.ToString(format);

					elapsedTime.StoreInUserVariable(engine, v_OutputUserVariableName, nameof(v_OutputUserVariableName), this);
					break;
				default:
					throw new NotImplementedException("Stopwatch Action '" + v_StopwatchAction + "' not implemented");
			}
		}

		public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
		{
			base.Render(editor, commandControls);

			RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_InstanceName", this, editor));
			RenderedControls.AddRange(commandControls.CreateDefaultDropdownGroupFor("v_StopwatchAction", this, editor));
			((ComboBox)RenderedControls[3]).SelectedIndexChanged += StopWatchComboBox_SelectedValueChanged;

			_measureControls = new List<Control>();
			_measureControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_ToStringFormat", this, editor));
			_measureControls.AddRange(commandControls.CreateDefaultOutputGroupFor("v_OutputUserVariableName", this, editor));

			foreach (var ctrl in _measureControls)
				ctrl.Visible = false;

			RenderedControls.AddRange(_measureControls);
		  
			return RenderedControls;
		}     

		public override string GetDisplayValue()
		{
			if (v_StopwatchAction == "Measure Stopwatch")
				return base.GetDisplayValue() + $" [{v_StopwatchAction} - Store Elapsed Time in '{v_OutputUserVariableName}' - Instance Name '{v_InstanceName}']";
			else
				return base.GetDisplayValue() + $" [{v_StopwatchAction} - Instance Name '{v_InstanceName}']";
		}

		private void StopWatchComboBox_SelectedValueChanged(object sender, EventArgs e)
		{
			if (((ComboBox)RenderedControls[3]).Text == "Measure Stopwatch")
			{
				foreach (var ctrl in _measureControls)
					ctrl.Visible = true;
			}
			else
			{
				foreach (var ctrl in _measureControls)
				{
					ctrl.Visible = false;
					if (ctrl is TextBox)
						((TextBox)ctrl).Clear();
				}
			}
		}
	}
}