using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Printers
{
    public interface IPortStatusPrinter
    {
        /// <summary>
        /// Prints collection of port statuses as a table.
        /// </summary>
        /// <param name="portStatuses">A collection of port status.</param>
        /// /// <param name="cToken">A cancellation token</param>
        Task PrintTableAsync(IEnumerable<IPrintablePortStatus> portStatuses, CancellationToken cToken);
    }
}
