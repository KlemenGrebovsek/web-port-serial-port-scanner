using System.Collections.Generic;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Scanners
{
    public interface IPortScanner
    {
        /// <summary>
        /// Scans ports and returns thier status.
        /// </summary>
        /// <param name="minPort">Representing minimum port of scanning.</param>
        /// <param name="maxPort">Representing maximum port of scanning.</param>
        /// <returns>Collection of scan status.</returns>
        public IEnumerable<IPrintableScanResult> Scan(int minPort, int maxPort);
    }
}
