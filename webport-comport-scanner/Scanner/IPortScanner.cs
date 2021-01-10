using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using webport_comport_scanner.Model;

namespace webport_comport_scanner.Scanner
{
    public interface IPortScanner
    {
        /// <summary>
        /// ScanAsync for ports and their status.
        /// </summary>
        /// <param name="sSettings">Object containing all scan settings.</param>
        /// <param name="cToken">CancellationToken object.</param>
        /// <returns>A collection of type printable port status.</returns>
        Task<IEnumerable<IPrintablePortStatus>> ScanAsync(IScanProperties sSettings, CancellationToken cToken);
    }
}
