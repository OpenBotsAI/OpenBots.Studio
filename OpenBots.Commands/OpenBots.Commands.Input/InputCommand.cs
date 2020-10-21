﻿using Newtonsoft.Json;
using OpenBots.Commands.Input.Forms;
using OpenBots.Core.Attributes.PropertyAttributes;
using OpenBots.Core.Command;
using OpenBots.Core.Common;
using OpenBots.Core.Enums;
using OpenBots.Core.Infrastructure;
using OpenBots.Core.Properties;
using OpenBots.Core.UI.Controls;
using OpenBots.Core.Utilities.CommonUtilities;
using OpenBots.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace OpenBots.Commands.Input
{
    [Serializable]
    [Category("Input Commands")]
    [Description("This command provides the user with a form to input and store a collection of data.")]
    public class InputCommand : ScriptCommand
    {

        [Required]
		[DisplayName("Header Name")]
        [Description("Define the header to be displayed on the input form.")]
        [SampleUsage("Please Provide Input || {vHeader}")]
        [Remarks("")]
        [Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
        public string v_InputHeader { get; set; }

        [Required]
		[DisplayName("Input Directions")]
        [Description("Define the directions to give to the user.")]
        [SampleUsage("Directions: Please fill in the following fields || {vDirections}")]
        [Remarks("")]
        [Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
        public string v_InputDirections { get; set; }

        [Required]
		[DisplayName("Input Parameters")]
        [Description("Define the required input parameters.")]
        [SampleUsage("[TextBox | Name | 500,30 | John | {vName}]")]
        [Remarks("")]
        [Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
        public DataTable v_UserInputConfig { get; set; }

        [JsonIgnore]
		[Browsable(false)]
        private DataGridView _userInputGridViewHelper;

        [JsonIgnore]
		[Browsable(false)]
        private CommandItemControl _addRowControl;

        public InputCommand()
        {
            CommandName = "InputCommand";
            SelectionName = "Prompt for Input";
            CommandEnabled = true;
            

            v_UserInputConfig = new DataTable();
            v_UserInputConfig.TableName = DateTime.Now.ToString("UserInputParamTable" + DateTime.Now.ToString("MMddyy.hhmmss"));
            v_UserInputConfig.Columns.Add("Type");
            v_UserInputConfig.Columns.Add("Label");
            v_UserInputConfig.Columns.Add("Size");
            v_UserInputConfig.Columns.Add("DefaultValue");
            v_UserInputConfig.Columns.Add("StoreInVariable");

            v_InputHeader = "Please Provide Input";
            v_InputDirections = "Directions: Please fill in the following fields";
        }

        public override void RunCommand(object sender)
        {
            var engine = (AutomationEngineInstance)sender;

            if (engine.ScriptEngineUI == null)
            {
                engine.ReportProgress("UserInput Supported With UI Only");
                MessageBox.Show("UserInput Supported With UI Only", "UserInput Command", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //create clone of original
            dynamic clonedCommand = Common.Clone(this);

            //translate variable
            clonedCommand.v_InputHeader = ((string)clonedCommand.v_InputHeader).ConvertUserVariableToString(engine);
            clonedCommand.v_InputDirections = ((string)clonedCommand.v_InputDirections).ConvertUserVariableToString(engine);

            //translate variables for each label
            foreach (DataRow rw in clonedCommand.v_UserInputConfig.Rows)
            {
                rw["DefaultValue"] = rw["DefaultValue"].ToString().ConvertUserVariableToString(engine);
                var targetVariable = rw["StoreInVariable"] as string;

                if (string.IsNullOrEmpty(targetVariable))
                {
                    var message = "User Input question '" + rw["Label"] + "' is missing variables to apply results to! " +
                                           "Results for the item will not be tracked. To fix this, assign a variable in the designer!";

                    MessageBox.Show("UserInput Supported With UI Only", "UserInput Command", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            //invoke ui for data collection
            var result = ((Form)engine.ScriptEngineUI).Invoke(new Action(() =>
            {
                //get input from user
              var userInputs = ShowInput(clonedCommand);

                //check if user provided input
                if (userInputs != null)
                {
                    //loop through each input and assign
                    for (int i = 0; i < userInputs.Count; i++)
                    {                       
                        //get target variable
                        var targetVariable = v_UserInputConfig.Rows[i]["StoreInVariable"] as string;

                        //store user data in variable
                        if (!string.IsNullOrEmpty(targetVariable))
                            ((object)userInputs[i]).StoreInUserVariable(engine, targetVariable);
                    }
                }
            }));
        }

        public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
        {
            base.Render(editor, commandControls);

            _userInputGridViewHelper = new DataGridView();
            _userInputGridViewHelper.KeyDown += UserInputDataGridView_KeyDown;
            _userInputGridViewHelper.DataBindings.Add("DataSource", this, "v_UserInputConfig", false, DataSourceUpdateMode.OnPropertyChanged);

            var typefield = new DataGridViewComboBoxColumn();
            typefield.Items.Add("TextBox");
            typefield.Items.Add("CheckBox");
            typefield.Items.Add("ComboBox");
            typefield.HeaderText = "Input Type";
            typefield.DataPropertyName = "Type";
            _userInputGridViewHelper.Columns.Add(typefield);

            var field = new DataGridViewTextBoxColumn();
            field.HeaderText = "Input Label";
            field.DataPropertyName = "Label";
            _userInputGridViewHelper.Columns.Add(field);

            field = new DataGridViewTextBoxColumn();
            field.HeaderText = "Input Size (X,Y)";
            field.DataPropertyName = "Size";
            _userInputGridViewHelper.Columns.Add(field);

            field = new DataGridViewTextBoxColumn();
            field.HeaderText = "Default Value";
            field.DataPropertyName = "DefaultValue";
            _userInputGridViewHelper.Columns.Add(field);

            field = new DataGridViewTextBoxColumn();
            field.HeaderText = "Assigned Variable";
            field.DataPropertyName = "StoreInVariable";
            _userInputGridViewHelper.Columns.Add(field);

            _userInputGridViewHelper.ColumnHeadersHeight = 30;
            _userInputGridViewHelper.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _userInputGridViewHelper.AllowUserToAddRows = false;
            _userInputGridViewHelper.AllowUserToDeleteRows = true;

            _addRowControl = new CommandItemControl();
            _addRowControl.Padding = new Padding(10, 0, 0, 0);
            _addRowControl.ForeColor = Color.AliceBlue;
            _addRowControl.Font = new Font("Segoe UI Semilight", 10);
            _addRowControl.CommandImage = Resources.command_input;
            _addRowControl.CommandDisplay = "Add Input Parameter";
            _addRowControl.Click += (sender, e) => AddInputParameter(sender, e, editor);

            RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_InputHeader", this, editor));
            RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_InputDirections", this, editor));

            RenderedControls.Add(commandControls.CreateDefaultLabelFor("v_UserInputConfig", this));
            RenderedControls.Add(_addRowControl);
            RenderedControls.AddRange(commandControls.CreateUIHelpersFor("v_UserInputConfig", this, new Control[] { _userInputGridViewHelper }, editor));
            RenderedControls.Add(_userInputGridViewHelper);

            return RenderedControls;
        }

        public override string GetDisplayValue()
        {
            return base.GetDisplayValue() + $" [Header '{v_InputHeader}']";
        }

        private void AddInputParameter(object sender, EventArgs e, IfrmCommandEditor editor)
        {
            var newRow = v_UserInputConfig.NewRow();
            newRow["Size"] = "500,30";
            v_UserInputConfig.Rows.Add(newRow);
        }

        private void UserInputDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (_userInputGridViewHelper.SelectedRows.Count > 0)
                _userInputGridViewHelper.Rows.RemoveAt(_userInputGridViewHelper.SelectedCells[0].RowIndex);
        }

        private List<string> ShowInput(InputCommand inputs)
        {

            var inputForm = new frmUserInput
            {
                InputCommand = inputs
            };
            var dialogResult = inputForm.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                var responses = new List<string>();
                foreach (var ctrl in inputForm.InputControls)
                {
                    if (ctrl is CheckBox)
                    {
                        var checkboxCtrl = (CheckBox)ctrl;
                        responses.Add(checkboxCtrl.Checked.ToString());
                    }
                    else
                        responses.Add(ctrl.Text);
                }
                return responses;
            }
            else
                return null;
        }
    }
}