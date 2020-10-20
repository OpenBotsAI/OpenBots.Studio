﻿using Microsoft.Office.Interop.Outlook;
using System.ComponentModel;
using OpenBots.Core.Attributes.PropertyAttributes;
using OpenBots.Core.Command;
using OpenBots.Core.Enums;
using OpenBots.Core.Infrastructure;
using OpenBots.Core.Utilities.CommonUtilities;
using OpenBots.Engine;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OpenBots.Commands.Outlook
{
    [Serializable]
    [Category("Outlook Commands")]
    [Description("This command deletes a selected email in Outlook.")]

    public class DeleteOutlookEmailCommand : ScriptCommand
    {

        [DisplayName("MailItem")]
        [Description("Enter the MailItem to delete.")]
        [SampleUsage("{vMailItem}")]
        [Remarks("")]
        [PropertyUIHelper(UIAdditionalHelperType.ShowVariableHelper)]
        public string v_MailItem { get; set; }

        [DisplayName("Delete Read Emails Only")]
        [PropertyUISelectionOption("Yes")]
        [PropertyUISelectionOption("No")]
        [Description("Specify whether to delete read email messages only.")]
        [SampleUsage("")]
        [Remarks("")]
        public string v_DeleteReadOnly { get; set; }

        public DeleteOutlookEmailCommand()
        {
            CommandName = "DeleteOutlookEmailCommand";
            SelectionName = "Delete Outlook Email";
            CommandEnabled = true;
            CustomRendering = true;
            v_DeleteReadOnly = "Yes";
        }

        public override void RunCommand(object sender)
        {
            var engine = (AutomationEngineInstance)sender;
            MailItem vMailItem = (MailItem)v_MailItem.ConvertUserVariableToObject(engine);

            if (v_DeleteReadOnly == "Yes")
            {
                if (vMailItem.UnRead == false)
                    vMailItem.Delete();
            }
            else
                vMailItem.Delete();
        }

        public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
        {
            base.Render(editor, commandControls);

            RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_MailItem", this, editor));
            RenderedControls.AddRange(commandControls.CreateDefaultDropdownGroupFor("v_DeleteReadOnly", this, editor));

            return RenderedControls;
        }

        public override string GetDisplayValue()
        {
            return base.GetDisplayValue() + $" [MailItem '{v_MailItem}']";
        }
    }
}