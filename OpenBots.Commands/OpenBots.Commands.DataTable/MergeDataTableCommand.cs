﻿using System.ComponentModel;
using OpenBots.Core.Attributes.PropertyAttributes;
using OpenBots.Core.Command;
using OpenBots.Core.Enums;
using OpenBots.Core.Infrastructure;
using OpenBots.Core.Utilities.CommonUtilities;
using OpenBots.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Data = System.Data;

namespace OpenBots.Commands.DataTable
{
    [Serializable]
    [Category("DataTable Commands")]
    [Description("This command merges a source DataTable into a destination DataTable.")]

    public class MergeDataTableCommand : ScriptCommand
    {

        [DisplayName("Source DataTable")]
        [Description("Enter an existing DataTable to merge into another one.")]
        [SampleUsage("{vSrcDataTable}")]
        [Remarks("")]
        [PropertyUIHelper(UIAdditionalHelperType.ShowVariableHelper)]
        public string v_SourceDataTable { get; set; }

        [DisplayName("Destination DataTable")]
        [Description("Enter an existing DataTable to apply the merge operation to.")]
        [SampleUsage("{vDestDataTable}")]
        [Remarks("")]
        [PropertyUIHelper(UIAdditionalHelperType.ShowVariableHelper)]
        public string v_DestinationDataTable { get; set; }

        [DisplayName("Missing Schema Action")]
        [PropertyUISelectionOption("Add")]
        [PropertyUISelectionOption("AddWithKey")]
        [PropertyUISelectionOption("Error")]
        [PropertyUISelectionOption("Ignore")]
        [Description("Select any Missing Schema Action.")]
        [SampleUsage("")]
        [Remarks("Specifies the action to take when adding data to the DataSet and the required DataTable or DataColumn is missing.")]
        public string v_MissingSchemaAction { get; set; }

        public MergeDataTableCommand()
        {
            CommandName = "MergeDataTableCommand";
            SelectionName = "Merge DataTable";
            CommandEnabled = true;
            CustomRendering = true;
            v_MissingSchemaAction = "Add";
        }

        public override void RunCommand(object sender)
        {
            /* ------------Before Merge Operation, following conditions must be checked---------------

            1. None of the (Source, Destination) DataTable Variables is null            -->     (Null Check)
            2. Data Type of both (Source, Destination) Variables must be DataTable      -->     (Data Type Check)
            3. Source and Destination DataTable Varibales must not be the same          -->     (Same Variable Check)

             */
            var engine = (AutomationEngineInstance)sender;

            // Get Variable Objects
            var v_SourceDTVariable = v_SourceDataTable.ConvertUserVariableToObject(engine);
            var v_DestinationDTVariable = v_DestinationDataTable.ConvertUserVariableToObject(engine);

            // (Null Check)
            if (v_SourceDTVariable is null)
                throw new Exception("Source DataTable Variable '" + v_SourceDataTable + "' is not initialized.");

            if (v_DestinationDTVariable is null)
                throw new Exception("Destination DataTable Variable '" + v_DestinationDataTable + "' is not initialized.");

            // (Data Type Check)
            if (!(v_SourceDTVariable is Data.DataTable))
                throw new Exception("Type of Source DataTable Variable '" + v_SourceDataTable + "' is not DataTable.");

            if (!(v_DestinationDTVariable is Data.DataTable))
                throw new Exception("Type of Destination DataTable Variable '" + v_DestinationDataTable + "' is not DataTable.");

            // Same Variable Check
            if (v_SourceDataTable != v_DestinationDataTable)
            {
                var sourceDT = (Data.DataTable)v_SourceDTVariable;
                var destinationDT = (Data.DataTable)v_DestinationDTVariable;

                switch (v_MissingSchemaAction)
                {
                    case "Add":
                        destinationDT.Merge(sourceDT, false, MissingSchemaAction.Add);
                        break;
                    case "AddWithKey":
                        destinationDT.Merge(sourceDT, false, MissingSchemaAction.AddWithKey);
                        break;
                    case "Error":
                        destinationDT.Merge(sourceDT, false, MissingSchemaAction.Error);
                        break;
                    case "Ignore":
                        destinationDT.Merge(sourceDT, false, MissingSchemaAction.Ignore);
                        break;
                    default:
                        throw new NotImplementedException("Missing Schema Action '" + v_MissingSchemaAction + "' not implemented");
                }

                // Update Destination Variable Value
                destinationDT.StoreInUserVariable(engine, v_DestinationDataTable);               
            }

        }

        public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
        {
            base.Render(editor, commandControls);

            RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_SourceDataTable", this, editor));
            RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_DestinationDataTable", this, editor));
            RenderedControls.AddRange(commandControls.CreateDefaultDropdownGroupFor("v_MissingSchemaAction", this, editor));

            return RenderedControls;
        }

        public override string GetDisplayValue()
        {
            return base.GetDisplayValue() + $" [Merge Source '{v_SourceDataTable}' Into Destination '{v_DestinationDataTable}']";
        }       
    }
}