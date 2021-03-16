using OpenBots.Core.Enums;
using OpenBots.Core.Script;
using OpenBots.Core.Utilities.CommonUtilities;
using OpenBots.UI.Forms.Supplement_Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenBots.UI.Forms.ScriptBuilder_Forms
{
    public partial class frmScriptBuilder : Form
    {
        #region Variable/Argument Tab Events
        private void dgvVariablesArguments_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //creates a list of all existing variable/argument names to check against, prior to creating a new one
                DataGridView dgv = (DataGridView)sender;

                _preEditVarArgName = dgv.Rows[e.RowIndex].Cells[0].Value?.ToString();

                _existingVarArgSearchList = new List<string>();
                _existingVarArgSearchList.AddRange(_scriptArguments.Select(arg => arg.ArgumentName).ToList());
                _existingVarArgSearchList.AddRange(_scriptVariables.Select(var => var.VariableName).ToList());
                _existingVarArgSearchList.AddRange(CommonMethods.GenerateSystemVariables().Select(var => var.VariableName).ToList());
            }
            catch (Exception ex)
            {
                //datagridview event failure
                Console.WriteLine(ex);
            }
        }

        private void dgvVariablesArguments_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;

                //variable/argument name column
                if (e.ColumnIndex == 0)
                {
                    var cellValue = dgv.Rows[e.RowIndex].Cells[0].Value;

                    //deletes an empty row if it's created without assigning values
                    if ((cellValue == null && _preEditVarArgName != null) ||
                        (cellValue != null && string.IsNullOrEmpty(cellValue.ToString().Trim())))
                    {
                        dgv.Rows.RemoveAt(e.RowIndex);
                        return;
                    }
                    //removes an empty uncommitted row
                    else if (dgv.Rows[e.RowIndex].Cells[0].Value == null)
                        return;

                    //trims any space characters before reassigning the value to the cell
                    string variableName = dgv.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    dgv.Rows[e.RowIndex].Cells[0].Value = variableName;

                    //prevents user from creating a new variable/argument with an already used name
                    if (_existingVarArgSearchList.Contains(variableName) && variableName != _preEditVarArgName)
                    {
                        Notify($"A variable or argument with the name '{variableName}' already exists", Color.Red);
                        dgv.Rows.RemoveAt(e.RowIndex);
                        return;
                    }
                    //if the variable/argument name is valid, set value cell's readonly as false
                    else
                    {
                        foreach (DataGridViewCell cell in dgv.Rows[e.RowIndex].Cells)
                            cell.ReadOnly = false;                          

                        dgv.Rows[e.RowIndex].Cells[0].Value = variableName.Trim(); 
                    }
                }
                //variable/argument type column
                else if (e.ColumnIndex == 1)
                {
                    Type selectedType = (Type)dgv.Rows[e.RowIndex].Cells[1].Value;
                    if (selectedType.IsPrimitive || selectedType == typeof(string))
                        dgv.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                    else
                    {
                        dgv.Rows[e.RowIndex].Cells[2].Value = null;
                        dgv.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    }                       
                }

                //marks the script as unsaved with changes
                if (!uiScriptTabControl.SelectedTab.Text.Contains(" *"))
                    uiScriptTabControl.SelectedTab.Text += " *";
            }
            catch (Exception ex)
            {
                //datagridview event failure
                Console.WriteLine(ex);
            }
        }

        private void dgvVariablesArguments_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;

                if (dgv.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    //when a new row is added, all cells except for the variable/argument name will be readonly until the name is validated
                    foreach (DataGridViewCell cell in dgv.Rows[e.RowIndex].Cells)
                    {
                        if (cell.ColumnIndex != 0)
                            cell.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //datagridview event failure
                Console.WriteLine(ex);
            }
        }

        private void dgvVariablesArguments_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                //prevents the ProjectPath row from being deleted
                if (e.Row.Cells[0].Value?.ToString() == "ProjectPath")
                    e.Cancel = true;
                else
                {
                    //marks the script as unsaved with changes
                    if (!uiScriptTabControl.SelectedTab.Text.Contains(" *"))
                        uiScriptTabControl.SelectedTab.Text += " *";
                }
            }
            catch (Exception ex)
            {
                //datagridview event failure
                Console.WriteLine(ex);
            }
        }

        private void dgvVariablesArguments_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
               
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    //sets the entire ProjectPath row as readonly
                    if (row.Cells[0].Value?.ToString() == "ProjectPath")
                        row.ReadOnly = true;

                    //adds new type to default list when a script containing non-defaults is loaded
                    if (!_typeContext.DefaultTypes.ContainsKey(((Type)row.Cells[1].Value)?.ToString()))
                        _typeContext.DefaultTypes.Add(((Type)row.Cells[1].Value).ToString(), (Type)row.Cells[1].Value);

                    //sets Value cell to readonly if the Direction is Out
                    if (row.Cells.Count == 4 && row.Cells["Direction"].Value != null && 
                        ((ScriptArgumentDirection)row.Cells["Direction"].Value == ScriptArgumentDirection.Out || 
                         (ScriptArgumentDirection)row.Cells["Direction"].Value == ScriptArgumentDirection.InOut))
                        row.Cells["ArgumentValue"].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                //datagridview event failure
                Console.WriteLine(ex);
            }
        }

        private void dgvVariablesArguments_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;

                if (dgv.IsCurrentCellDirty)
                {
                    //grab pre-edit value from here
                    if (dgv.CurrentCell.Value is Type)
                        _preEditVarArgType = (Type)dgv.CurrentCell.Value;
                    
                    //this fires the cell value changed handler below
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }

            }
            catch (Exception ex)
            {
                //datagridview event failure
                Console.WriteLine(ex);
            }
        }

        private void dgvVariablesArguments_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                if (e.RowIndex != -1)
                {
                    var selectedCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    if (selectedCell.Value == null)
                        return;

                    else if (e.RowIndex != -1 && e.ColumnIndex == 3)
                    {
                        //sets value cell to read only if the argument direction is set to Out
                        if ((ScriptArgumentDirection)selectedCell.Value == ScriptArgumentDirection.Out || 
                            (ScriptArgumentDirection)selectedCell.Value == ScriptArgumentDirection.InOut)
                        {
                            dgv.Rows[e.RowIndex].Cells["ArgumentValue"].Value = null;
                            dgv.Rows[e.RowIndex].Cells["ArgumentValue"].ReadOnly = true;
                        }

                        else if ((ScriptArgumentDirection)selectedCell.Value == ScriptArgumentDirection.In)
                            dgv.Rows[e.RowIndex].Cells["ArgumentValue"].ReadOnly = false;
                    }

                    else if (selectedCell.Value is Type && ((Type)selectedCell.Value).Name == "MoreOptions")
                    {
                        //triggers the type form to open if 'More Options...' is selected
                        frmTypes typeForm = new frmTypes(_typeContext.GroupedTypes);
                        typeForm.ShowDialog();

                        //adds type to defaults if new, then commits selection to the cell
                        if (typeForm.DialogResult == DialogResult.OK)
                        {
                            if (!_typeContext.DefaultTypes.ContainsKey(typeForm.SelectedType.FullName))
                            {
                                _typeContext.DefaultTypes.Add(typeForm.SelectedType.FullName, typeForm.SelectedType);
                                VariableType.DataSource = new BindingSource(_typeContext.DefaultTypes, null);
                                ArgumentType.DataSource = new BindingSource(_typeContext.DefaultTypes, null);
                            }

                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = typeForm.SelectedType;
                            ((DataGridViewComboBoxCell)dgv.Rows[e.RowIndex].Cells[1]).Value = typeForm.SelectedType;
                        }
                        //returns the cell to its original value
                        else
                        {
                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = _preEditVarArgType;
                            ((DataGridViewComboBoxCell)dgv.Rows[e.RowIndex].Cells[1]).Value = _preEditVarArgType;
                        }

                        //necessary hack to force the set value to update
                        SendKeys.Send("{TAB}");
                        SendKeys.Send("+{TAB}");
                    }
                }
                
            }
            catch (Exception ex)
            {
                //datagridview event failure
                Console.WriteLine(ex);
            }
        }

        private void dgvVariablesArguments_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;

                if (dgv.Columns.Count == 4)
                {
                    //sets Direction to In by default when a new row is added. Prevents cell from ever being null
                    e.Row.Cells["Direction"].Value = ScriptArgumentDirection.In;
                }

                e.Row.Cells[1].Value = typeof(string);
                
            }
            catch (Exception ex)
            {
                //datagridview event failure
                Console.WriteLine(ex);
            }
        }

        private void dgvVariables_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;

                var dgvBindingList = (BindingList<ScriptVariable>)dgv.DataSource;
                var nullScriptVariable = dgvBindingList.Where(x => string.IsNullOrEmpty(x.VariableName)).FirstOrDefault();

                if (nullScriptVariable != null && dgvBindingList.Count > 1)
                    dgvBindingList.Remove(nullScriptVariable);

            }
            catch (Exception ex)
            {
                //datagridview event failure
                Console.WriteLine(ex);
            }
        }

        private void dgvArguments_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;

                var dgvBindingList = (BindingList<ScriptArgument>)dgv.DataSource;
                var nullScriptVariable = dgvBindingList.Where(x => string.IsNullOrEmpty(x.ArgumentName)).FirstOrDefault();

                if (nullScriptVariable != null && dgvBindingList.Count > 1)
                    dgvBindingList.Remove(nullScriptVariable);

            }
            catch (Exception ex)
            {
                //datagridview event failure
                Console.WriteLine(ex);
            }
        }

        private void ResetVariableArgumentBindings()
        {
            dgvVariables.DataSource = new BindingList<ScriptVariable>(_scriptVariables);
            dgvArguments.DataSource = new BindingList<ScriptArgument>(_scriptArguments);

            var defaultTypesBinding = new BindingSource(_typeContext.DefaultTypes, null);
            VariableType.DataSource = defaultTypesBinding;
            ArgumentType.DataSource = defaultTypesBinding;
        }

        private void dgvVariablesArguments_KeyDown(object sender, KeyEventArgs e)
        {
            //various advanced keystroke shortcuts for saving, creating new var/arg/elem, shortcut menu, etc
            if (e.Control)
            {
                if (e.Shift)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.S:
                            SaveAllFiles();
                            break;
                    }
                }
                else
                {
                    switch (e.KeyCode)
                    {
                        case Keys.S:
                            ClearSelectedListViewItems();
                            SaveToOpenBotsFile(false);
                            break;
                        case Keys.J:
                            OpenArgumentManager();
                            break;
                        case Keys.K:
                            OpenVariableManager();
                            break;
                        case Keys.L:
                            OpenElementManager();
                            break;
                        case Keys.M:
                            shortcutMenuToolStripMenuItem_Click(null, null);
                            break;
                        case Keys.O:
                            aboutOpenBotsToolStripMenuItem_Click(null, null);
                            break;
                    }
                }
            }
        }
        #endregion        
    }
}
