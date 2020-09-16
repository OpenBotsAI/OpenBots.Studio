﻿using System.Collections.Generic;

namespace OpenBots.UI.DTOs
{
    public class Method
    {
        public string MethodName { get; set; }
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}
