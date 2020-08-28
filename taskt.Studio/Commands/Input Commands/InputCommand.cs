﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using taskt.Core.Attributes.ClassAttributes;
using taskt.Core.Attributes.PropertyAttributes;
using taskt.Core.Command;
using taskt.Core.Common;
using taskt.Core.Enums;
using taskt.Core.Infrastructure;
using taskt.Core.Utilities.CommonUtilities;
using taskt.Engine;
using taskt.Properties;
using taskt.UI.CustomControls;
using taskt.UI.Forms;

namespace taskt.Commands
{
    [Serializable]
    [Group("Input Commands")]
    [Description("Sends keystrokes to a targeted window")]
    [UsesDescription("Use this command when you want to send keystroke inputs to a window.")]
    [ImplementationDescription("This command implements 'Windows.Forms.SendKeys' method to achieve automation.")]
    public class InputCommand : ScriptCommand
    {
        [XmlAttribute]
        [PropertyDescription("Please specify a heading name")]
        [InputSpecification("Define the header to be displayed on the input form.")]
        [SampleUsage("n/a")]
        [Remarks("")]
        public string v_InputHeader { get; set; }

        [XmlAttribute]
        [PropertyDescription("Please specify input directions")]
        [InputSpecification("Define the directions you want to give the user.")]
        [SampleUsage("n/a")]
        [Remarks("")]
        public string v_InputDirections { get; set; }

        [XmlElement]
        [PropertyDescription("User Input Parameters")]
        [InputSpecification("Define the required input parameters.")]
        [SampleUsage("n/a")]
        [Remarks("")]
        [PropertyUIHelper(UIAdditionalHelperType.ShowVariableHelper)]
        public DataTable v_UserInputConfig { get; set; }

        [XmlIgnore]
        [NonSerialized]
        private DataGridView UserInputGridViewHelper;

        [XmlIgnore]
        [NonSerialized]
        private CommandItemControl AddRowControl;

        public InputCommand()
        {
            CommandName = "InputCommand";
            SelectionName = "Prompt for Input";
            CommandEnabled = true;
            CustomRendering = true;

            v_UserInputConfig = new DataTable();
            v_UserInputConfig.TableName = DateTime.Now.ToString("UserInputParamTable" + DateTime.Now.ToString("MMddyy.hhmmss"));
            v_UserInputConfig.Columns.Add("Type");
            v_UserInputConfig.Columns.Add("Label");
            v_UserInputConfig.Columns.Add("Size");
            v_UserInputConfig.Columns.Add("DefaultValue");
            v_UserInputConfig.Columns.Add("UserInput");
            v_UserInputConfig.Columns.Add("ApplyToVariable");

            v_InputHeader = "Please Provide Input";
            v_InputDirections = "Directions: Please fill in the following fields";

        }

        public override void RunCommand(object sender)
        {


            var engine = (AutomationEngineInstance)sender;


            if (engine.TasktEngineUI == null)
            {
                engine.ReportProgress("UserInput Supported With UI Only");
                System.Windows.Forms.MessageBox.Show("UserInput Supported With UI Only", "UserInput Command", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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

                var targetVariable = rw["ApplyToVariable"] as string;

                if (string.IsNullOrEmpty(targetVariable))
                {
                    var newMessage = new ShowMessageCommand();
                    newMessage.v_Message = "User Input question '" + rw["Label"] + "' is missing variables to apply results to! Results for the item will not be tracked.  To fix this, assign a variable in the designer!";
                    newMessage.v_AutoCloseAfter = "10";
                    newMessage.RunCommand(sender);
                }
            }

            //invoke ui for data collection
            var result = ((frmScriptEngine)engine.TasktEngineUI).Invoke(new Action(() =>
            {
                //get input from user
              var userInputs = ((frmScriptEngine)engine.TasktEngineUI).ShowInput(clonedCommand);

                //check if user provided input
                if (userInputs != null)
                {
                    //loop through each input and assign
                    for (int i = 0; i < userInputs.Count; i++)
                    {                       
                        //get target variable
                        var targetVariable = v_UserInputConfig.Rows[i]["ApplyToVariable"] as string;

                        //store user data in variable
                        if (!string.IsNullOrEmpty(targetVariable))
                        {
                            ((object)userInputs[i]).StoreInUserVariable(engine, targetVariable);
                        }
                    }
                }
            }));
        }

        public override List<Control> Render(IfrmCommandEditor editor)
        {


            base.Render(editor);

            UserInputGridViewHelper = new DataGridView();
            UserInputGridViewHelper.KeyDown += UserInputDataGridView_KeyDown;
            UserInputGridViewHelper.DataBindings.Add("DataSource", this, "v_UserInputConfig", false, DataSourceUpdateMode.OnPropertyChanged);

            var typefield = new DataGridViewComboBoxColumn();
            typefield.Items.Add("TextBox");
            typefield.Items.Add("CheckBox");
            typefield.Items.Add("ComboBox");
            typefield.HeaderText = "Input Type";
            typefield.DataPropertyName = "Type";
            UserInputGridViewHelper.Columns.Add(typefield);

            var field = new DataGridViewTextBoxColumn();
            field.HeaderText = "Input Label";
            field.DataPropertyName = "Label";
            UserInputGridViewHelper.Columns.Add(field);


            field = new DataGridViewTextBoxColumn();
            field.HeaderText = "Input Size (X,Y)";
            field.DataPropertyName = "Size";
            UserInputGridViewHelper.Columns.Add(field);

            field = new DataGridViewTextBoxColumn();
            field.HeaderText = "Default Value";
            field.DataPropertyName = "DefaultValue";
            UserInputGridViewHelper.Columns.Add(field);

            field = new DataGridViewTextBoxColumn();
            field.HeaderText = "Assigned Variable";
            field.DataPropertyName = "ApplyToVariable";
            UserInputGridViewHelper.Columns.Add(field);

            UserInputGridViewHelper.ColumnHeadersHeight = 30;
            UserInputGridViewHelper.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            UserInputGridViewHelper.AllowUserToAddRows = false;
            UserInputGridViewHelper.AllowUserToDeleteRows = false;


            AddRowControl = new CommandItemControl();
            AddRowControl.Padding = new Padding(10, 0, 0, 0);
            AddRowControl.ForeColor = Color.AliceBlue;
            AddRowControl.Font = new Font("Segoe UI Semilight", 10);
            AddRowControl.CommandImage = Resources.command_input;
            AddRowControl.CommandDisplay = "Add Input Parameter";
            AddRowControl.Click += (sender, e) => AddInputParameter(sender, e, editor);

            RenderedControls.AddRange(CommandControls.CreateDefaultInputGroupFor("v_InputHeader", this, editor));
            RenderedControls.AddRange(CommandControls.CreateDefaultInputGroupFor("v_InputDirections", this, editor));
            RenderedControls.Add(CommandControls.CreateDefaultLabelFor("v_UserInputConfig", this));
            RenderedControls.Add(AddRowControl);
            RenderedControls.AddRange(CommandControls.CreateUIHelpersFor("v_UserInputConfig", this, new Control[] { UserInputGridViewHelper }, editor));
            RenderedControls.Add(UserInputGridViewHelper);



            return RenderedControls;

        }

        private void AddInputParameter(object sender, EventArgs e, IfrmCommandEditor editor)
        {
            var newRow = v_UserInputConfig.NewRow();
            newRow["Size"] = "500,100";
            v_UserInputConfig.Rows.Add(newRow);

        }

        private void UserInputDataGridView_KeyDown(object sender, KeyEventArgs e)
        {


            if (UserInputGridViewHelper.SelectedRows.Count > 0)
            {
                UserInputGridViewHelper.Rows.RemoveAt(UserInputGridViewHelper.SelectedCells[0].RowIndex);
            }

        }

        public override string GetDisplayValue()
        {
            return base.GetDisplayValue() + " [" + v_InputHeader + "]";
        }
    }
}