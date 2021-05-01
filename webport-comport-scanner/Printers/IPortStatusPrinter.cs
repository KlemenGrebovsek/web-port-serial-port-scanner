using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Printers
{
    public interface IPortStatusPrinter
    {
        Task PrintTableAsync(IEnumerable<IPrintablePortStatus> portStatuses, CancellationToken cToken);
    }
}
