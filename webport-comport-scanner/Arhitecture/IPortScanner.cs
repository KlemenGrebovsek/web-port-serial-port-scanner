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
        /// <returns>A collection of type IPrintablePortStatus.</returns>
        IEnumerable<IPrintablePortStatus> Scan(int minPort, int maxPort);
    }
}
