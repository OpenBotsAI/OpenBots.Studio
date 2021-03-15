﻿using OpenBots.NetCore.Core.Command;
using OpenBots.NetCore.Core.Infrastructure;
using OpenBots.NetCore.Core.Properties;
using OpenBots.NetCore.Core.Script;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenBots.NetCore.Commands.ErrorHandling
{
    [Serializable]
    [Category("Error Handling Commands")]
    [Description("This command defines a block of commands which are always executed after a try/catch block.")]
    public class FinallyCommand : ScriptCommand
    {
        public FinallyCommand()
        {
            CommandName = "FinallyCommand";
            SelectionName = "Finally";
            CommandEnabled = true;
            CommandIcon = Resources.command_try;

        }

        public override void RunCommand(object sender, ScriptAction parentCommand)
        {
            //no execution required, used as a marker by the Automation Engine
        }

        public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
        {
            base.Render(editor, commandControls);

            return RenderedControls;
        }

        public override string GetDisplayValue()
        {
            return base.GetDisplayValue();
        }
    }
}