﻿using Microsoft.Office.Interop.Word;
using System.ComponentModel;
using OpenBots.Core.Attributes.PropertyAttributes;
using OpenBots.Core.Command;
using OpenBots.Core.Enums;
using OpenBots.Core.Infrastructure;
using OpenBots.Core.Utilities.CommonUtilities;
using OpenBots.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;

namespace OpenBots.Commands.Word
{
    [Serializable]
    [Category("Word Commands")]
    [Description("This command saves a Word Document to a specific file.")]
    public class WordSaveDocumentAsCommand : ScriptCommand
    {
        [DisplayName("Word Instance Name")]
        [Description("Enter the unique instance that was specified in the **Create Application** command.")]
        [SampleUsage("MyWordInstance")]
        [Remarks("Failure to enter the correct instance or failure to first call the **Create Application** command will cause an error.")]
        public string v_InstanceName { get; set; }

        [DisplayName("Document Location")]
        [Description("Enter or Select the path of the folder to save the Document in.")]
        [SampleUsage(@"C:\temp || {vFolderPath} || {ProjectPath}")]
        [Remarks("")]
        [PropertyUIHelper(UIAdditionalHelperType.ShowVariableHelper)]
        [PropertyUIHelper(UIAdditionalHelperType.ShowFolderSelectionHelper)]
        public string v_FolderPath { get; set; }

        [DisplayName("Document File Name")]
        [Description("Enter or Select the name of the Document file.")]
        [SampleUsage("myFile.docx || {vFilename}")]
        [Remarks("")]
        [PropertyUIHelper(UIAdditionalHelperType.ShowVariableHelper)]
        public string v_FileName { get; set; }

        public WordSaveDocumentAsCommand()
        {
            CommandName = "WordSaveDocumentAsCommand";
            SelectionName = "Save Document As";
            CommandEnabled = true;
            CustomRendering = true;
            v_InstanceName = "DefaultWord";
        }

        public override void RunCommand(object sender)
        {
            var engine = (AutomationEngineInstance)sender;
            var vFileName = v_FileName.ConvertUserVariableToString(engine);
            var vFolderPath = v_FolderPath.ConvertUserVariableToString(engine);

            //get word app object
            var wordObject = v_InstanceName.GetAppInstance(engine);

            //convert object
            Application wordInstance = (Application)wordObject;
            string filePath = Path.Combine(vFolderPath, vFileName);

            //overwrite and save
            wordInstance.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            wordInstance.ActiveDocument.SaveAs(filePath);
            wordInstance.DisplayAlerts = WdAlertLevel.wdAlertsAll;
        }

        public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
        {
            base.Render(editor, commandControls);

            RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_InstanceName", this, editor));
            RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_FolderPath", this, editor));
            RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_FileName", this, editor));

            return RenderedControls;
        }

        public override string GetDisplayValue()
        {
            return base.GetDisplayValue() + $" [Save to '{v_FolderPath}\\{v_FileName}' - Instance Name '{v_InstanceName}']";
        }
    }
}