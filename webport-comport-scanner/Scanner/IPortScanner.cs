using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using webport_comport_scanner.Model;

namespace webport_comport_scanner.Scanner
{
    public interface IPortScanner
    {
        /// <summary>
        /// Scans for ports and their status.
        /// </summary>
        /// <param name="minPort">Scan from this port (including).</param>
        /// <param name="maxPort">Scan to this port (including).</param>
        /// <param name="cToken">CancellationToken object.</param>
        /// <returns>A collection of port status.</returns>
        Task<IEnumerable<IPrintablePortStatus>> ScanAsync(int minPort, int maxPort, CancellationToken cToken);
    }
}
