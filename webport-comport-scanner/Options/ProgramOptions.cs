using System;
using System.Collections.Generic;
using MatthiWare.CommandLine.Core.Attributes;
using System.Text;

namespace webport_comport_scanner.Options
{
    public class ProgramOptions
    {
        [Required, Name("a","argument"), Description("Some desc"), DefaultValue(null)]
        public string SomeArgument { get; set; }
    }
}
