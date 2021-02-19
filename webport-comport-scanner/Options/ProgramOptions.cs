using MatthiWare.CommandLine.Core.Attributes;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Options
{
    /// <summary>
    /// Represents options for web and serial port arguments.
    /// </summary>
    public class ProgramOptions
    {
        [Name("f", "from")]
        [Description("Scans for ports that are equal or greater than this value. (0 - 65535)")]
        [DefaultValue(0)]
        public int MinPort { get; set; }

        [Name("t", "to")]
        [Description("Scans for ports that are equal or less than this value. (0 - 65535)")]
        [DefaultValue(65535)]
        public int MaxPort { get; set; }

        [Name("s", "status")]
        [Description("Filters ports by this status. (Any, Free, InUse, Unknown)")]
        [DefaultValue(PortStatus.Any)]
        public PortStatus Status { get; set; }
    }
}
