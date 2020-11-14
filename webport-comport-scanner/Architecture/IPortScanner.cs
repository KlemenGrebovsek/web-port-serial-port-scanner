using System.Collections.Generic;
using webport_comport_scanner.Model;

namespace webport_comport_scanner.Architecture
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
        IEnumerable<IPrintablePortStatus> Scan(int minPort, int maxPort, PortStatus status);
    }
}
