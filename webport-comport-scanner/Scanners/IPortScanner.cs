using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Scanners
{
    public interface IPortScanner
    {
        Task<IEnumerable<PortStatusData>> ScanAsync(ScanProperties properties, CancellationToken cToken);
    }
}
