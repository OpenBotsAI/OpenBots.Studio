﻿//Copyright (c) 2019 Jason Bayldon
//Modifications - Copyright (c) 2020 OpenBots Inc.
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.
using OpenBots.Core.UI.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenBots.UI.Forms.Supplement_Forms
{
    public partial class frmVariableSelector : UIForm
    {
        private List<string> _variableList;
        private string _txtCommandWatermark = "Type Here to Search";

        public frmVariableSelector()
        {
            InitializeComponent();            
        }

        private void frmVariableSelector_Load(object sender, EventArgs e)
        {
            _variableList = new List<string>();
            _variableList.AddRange(lstVariables.Items.Cast<string>().ToList());
        }

        private void uiBtnOk_Click(object sender, EventArgs e)
        {
            if (lstVariables.SelectedItem == null)
            {
                MessageBox.Show("There are no item(s) selected! Select an item and Ok or select Cancel");
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void uiBtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void lstVariables_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void txtVariableSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtVariableSearch.Text;

            if (search == _txtCommandWatermark)
                return;

            if (string.IsNullOrEmpty(search))
            {
                lstVariables.Items.Clear();
                lstVariables.Items.AddRange(_variableList.ToArray());
            }

            var items = (from a in _variableList
                         where a.ToLower().Contains(search.ToLower())
                         select a).ToArray();

            lstVariables.Items.Clear();
            lstVariables.Items.AddRange(items);
        }

        private void txtVariableSearch_Enter(object sender, EventArgs e)
        {
            if (txtVariableSearch.Text == _txtCommandWatermark)
            {
                txtVariableSearch.Text = "";
                txtVariableSearch.ForeColor = Color.Black;
            }
        }

        private void txtVariableSearch_Leave(object sender, EventArgs e)
        {
            if (txtVariableSearch.Text == "")
            {
                txtVariableSearch.Text = _txtCommandWatermark;
                txtVariableSearch.ForeColor = Color.LightGray;
            }
        }
    }
}