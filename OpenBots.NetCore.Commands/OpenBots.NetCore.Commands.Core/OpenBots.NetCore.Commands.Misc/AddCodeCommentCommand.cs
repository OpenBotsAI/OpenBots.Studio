﻿using OpenBots.NetCore.Core.Command;
using OpenBots.NetCore.Core.Infrastructure;
using OpenBots.NetCore.Core.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenBots.NetCore.Commands.Misc
{
    [Serializable]
    [Category("Misc Commands")]
    [Description("This command adds an in-line comment to the script.")]
    public class AddCodeCommentCommand : ScriptCommand
    {
        public AddCodeCommentCommand()
        {
            CommandName = "AddCodeCommentCommand";
            SelectionName = "Add Code Comment";
            CommandEnabled = true;
            CommandIcon = Resources.command_comment;
        }

        public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
        {
            base.Render(editor, commandControls);

            return RenderedControls;
        }

        public override string GetDisplayValue()
        {
            return $"// Comment ['{v_Comment}']";
        }
    }
}