using System;
using System.Collections.Generic;
using MatthiWare.CommandLine.Core.Attributes;
using System.Text;

namespace webport_comport_scanner.Options
{
    public class ProgramOptions
    {
        [Required, Name("-f", "From port (0 - 65535)"), Description("Program scans ports that are equal or greater than this value."), DefaultValue(0)]
        public int MinPort { get; set; }

        [Required, Name("-t", "To port (0 - 65535)"), Description("Program scans ports that are equal or less than this value."), DefaultValue(65535)]
        public int MaxPort { get; set; }
    }
}
