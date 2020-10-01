﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenBots.Core.Script;
using OpenBots.Core.UI.Forms;

namespace OpenBots.UI.Forms.Supplement_Forms
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class frmHTMLDisplayForm : UIForm
    {
        public DialogResult Result { get; set; }
        public string TemplateHTML { get; set; }

        public frmHTMLDisplayForm()
        {
            InitializeComponent();
        }

        private void frmHTMLDisplayForm_Load(object sender, EventArgs e)
        {
            webBrowserHTML.ScriptErrorsSuppressed = true;
            webBrowserHTML.ObjectForScripting = this;
            webBrowserHTML.DocumentText = TemplateHTML;
            TopMost = true;
        }

        public void OK()
        {
            //Todo: figure out why return DialogResult not working for some reason
            Result = DialogResult.OK;
            Close();
        }
        public void Cancel()
        {
            //Todo: figure out why return DialogResult not working for some reason
            Result = DialogResult.Cancel;
            Close();
        }

        public List<ScriptVariable> GetVariablesFromHTML(string tagSearch)
        {
            var varList = new List<ScriptVariable>();

            HtmlElementCollection collection = webBrowserHTML.Document.GetElementsByTagName(tagSearch);
            for (int i = 0; i < collection.Count; i++)
            {
                var variableName = collection[i].GetAttribute("v_OutputUserVariableName");

                if (!string.IsNullOrEmpty(variableName))
                {
                    var parentElement = collection[i];

                    if (tagSearch == "select")
                    {
                        foreach (HtmlElement item in parentElement.Children)
                        {
                            if (item.GetAttribute("selected") == "True")
                            {
                                varList.Add(new ScriptVariable() { VariableName = variableName, VariableValue = item.InnerText });
                            }
                        }
                    }
                    else
                    {
                        if (parentElement.GetAttribute("type") == "checkbox")
                        {
                            var inputValue = collection[i].GetAttribute("checked");
                            varList.Add(new ScriptVariable() { VariableName = variableName, VariableValue = inputValue });
                        }
                        else
                        {
                            var inputValue = collection[i].GetAttribute("value");
                            varList.Add(new ScriptVariable() { VariableName = variableName, VariableValue = inputValue });
                        }
                    }
                }
            }
            return varList;
        }
    }
}
