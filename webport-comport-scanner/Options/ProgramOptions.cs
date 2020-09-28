using MatthiWare.CommandLine.Core.Attributes;

namespace webport_comport_scanner.Options
{
    /// <summary>
    /// Contains options for web and serial port arguments.
    /// </summary>
    public class ProgramOptions
    {
        [Name("f", "from")]
        [Description("Scans ports that are equal or greater than this value (0 - 65535).")]
        [DefaultValue(0)]
        public int MinPort { get; set; }

        [Name("t", "to")]
        [Description("Scans ports that are equal or less than this value (0 - 65535).")]
        [DefaultValue(65535)]
        public int MaxPort { get; set; }

    }
}
