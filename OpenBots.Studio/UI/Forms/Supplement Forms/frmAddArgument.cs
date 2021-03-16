using OpenBots.Core.Enums;
using OpenBots.Core.Script;
using OpenBots.Core.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OpenBots.UI.Forms.Supplement_Forms
{
    public partial class frmAddArgument : UIForm
    {
        public List<ScriptVariable> ScriptVariables { get; set; }
        public List<ScriptArgument> ScriptArguments { get; set; }
        private bool _isEditMode;
        private string _editingArgumentName;
        private TypeContext _typeContext;
        private Type _preEditType;

        public frmAddArgument(TypeContext typeContext)
        {
            InitializeComponent();
            _typeContext = typeContext;
            cbxDefaultDirection.SelectedIndex = 0;

            cbxDefaultType.DataSource = new BindingSource(_typeContext.DefaultTypes, null);
            cbxDefaultType.DisplayMember = "Key";
            cbxDefaultType.ValueMember = "Value";

            cbxDefaultType.SelectedValue = typeof(string);
            cbxDefaultType.Tag = typeof(string);
        }

        public frmAddArgument(string argumentName, ScriptArgumentDirection argumentDirection, string argumentValue, Type argumentType,
            TypeContext typeContext)
        {
            InitializeComponent();
            _typeContext = typeContext;
            cbxDefaultType.DataSource = new BindingSource(_typeContext.DefaultTypes, null);
            cbxDefaultType.DisplayMember = "Key";
            cbxDefaultType.ValueMember = "Value";

            Text = "edit argument";
            lblHeader.Text = "edit argument";
            txtArgumentName.Text = argumentName;
            cbxDefaultDirection.Text = argumentDirection.ToString();
            txtDefaultValue.Text = argumentValue;
            cbxDefaultType.SelectedValue = argumentType;
            cbxDefaultType.Tag = argumentType;

            _isEditMode = true;
            _editingArgumentName = argumentName;
        }

        private void frmAddArgument_Load(object sender, EventArgs e)
        {

        }

        private void uiBtnOk_Click(object sender, EventArgs e)
        {
            txtArgumentName.Text = txtArgumentName.Text.Trim();
            if (txtArgumentName.Text == string.Empty)
            {
                lblArgumentNameError.Text = "Argument Name not provided";
                return;
            }

            string newArgumentName = txtArgumentName.Text;
            var existingVariable = ScriptVariables.Where(var => var.VariableName == newArgumentName).FirstOrDefault();
            var existingArgument = ScriptArguments.Where(var => var.ArgumentName == newArgumentName).FirstOrDefault();
            if (existingArgument != null || existingVariable != null)
            {
                if (!_isEditMode || existingArgument.ArgumentName != _editingArgumentName)
                {
                    lblArgumentNameError.Text = "An Argument or Variable with this name already exists";
                    return;
                }
            }

            if (txtArgumentName.Text.StartsWith("{") || txtArgumentName.Text.EndsWith("}"))
            {
                lblArgumentNameError.Text = "Argument markers '{' and '}' should not be included";
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void uiBtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cbxDefaultDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDefaultDirection.Text == "Out" || cbxDefaultDirection.Text == "InOut")
            {
                txtDefaultValue.Text = "";
                txtDefaultValue.Enabled = false;
            }
            else
                txtDefaultValue.Enabled = true;               
        }

        private void cbxDefaultDirection_Click(object sender, EventArgs e)
        {
            ComboBox clickedDropdownBox = (ComboBox)sender;
            clickedDropdownBox.DroppedDown = true;
        }

        private void cbxDefaultType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (((Type)cbxDefaultType.SelectedValue).Name == "MoreOptions")
            {
                frmTypes typeForm = new frmTypes(_typeContext.GroupedTypes);
                typeForm.ShowDialog();

                if (typeForm.DialogResult == DialogResult.OK)
                {
                    if (!_typeContext.DefaultTypes.ContainsKey(typeForm.SelectedType.FullName))
                    {
                        _typeContext.DefaultTypes.Add(typeForm.SelectedType.FullName, typeForm.SelectedType);
                        cbxDefaultType.DataSource = new BindingSource(_typeContext.DefaultTypes, null);
                    }

                    cbxDefaultType.SelectedValue = typeForm.SelectedType;
                    cbxDefaultType.Tag = typeForm.SelectedType;
                }
                else
                {
                    cbxDefaultType.SelectedValue = _preEditType;
                    cbxDefaultType.Tag = _preEditType;
                }
            }
            else
                cbxDefaultType.Tag = cbxDefaultType.SelectedValue;

            _preEditType = (Type)cbxDefaultType.SelectedValue;

            if (_preEditType == typeof(string) || _preEditType.IsPrimitive)
                txtDefaultValue.ReadOnly = false;
            else
            {
                txtDefaultValue.ReadOnly = true;
                txtDefaultValue.Text = "";
            }
        }
    }
}
