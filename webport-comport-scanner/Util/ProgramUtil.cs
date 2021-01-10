using System;
using webport_comport_scanner.Model;

namespace webport_comport_scanner.Util
{
    /// <summary>
    /// Provides simple helper utils for this program.
    /// </summary>
    public static class ProgramUtil
    {
        public static PortStatus TransformOptionStatus(string status)
        {
            if (!Enum.TryParse(status, out PortStatus portStatus) || portStatus == PortStatus.Unknown)
                throw new ArgumentException("Invalid port status.");
            
            return portStatus;
        }
    }
}