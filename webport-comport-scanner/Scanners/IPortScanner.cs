using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Scanners
{
    public interface IPortScanner
    {
        /// <summary>
        /// Scans for ports and their status.
        /// </summary>
        /// <param name="properties">Scan properties.</param>
        /// <param name="cToken">CancellationToken object.</param>
        /// <returns>A collection of port status.</returns>
        Task<IEnumerable<PortStatusData>> ScanAsync(ScanProperties properties, CancellationToken cToken);
    }
}
