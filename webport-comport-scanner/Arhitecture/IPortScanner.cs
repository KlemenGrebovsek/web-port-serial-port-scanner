using System.Collections.Generic;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Scanners
{
    public interface IPortScanner
    {
        /// <summary>
        /// Scans for ports and their status.
        /// </summary>
        /// <param name="minPort">Minimum port (including).</param>
        /// <param name="maxPort">Maximum port (including).</param>
        /// <param name="status">Filter ports by this status.</param>
        /// <returns>A collection of type printable port status.</returns>
        IEnumerable<IPrintablePortStatus> ScanStatus(int minPort, int maxPort, PortStatus status);
    }
}
