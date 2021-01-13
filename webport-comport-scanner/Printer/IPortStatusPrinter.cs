﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using webport_comport_scanner.Model;

namespace webport_comport_scanner.Printer
{
    public interface IPortStatusPrinter
    {
        /// <summary>
        /// Prints collection of ports statuses as a table.
        /// </summary>
        /// <param name="portStatuses">A collection of type IPrintablePortStatus.</param>
        /// /// <param name="cToken">A cancellation token</param>
        Task PrintTableAsync(IEnumerable<IPrintablePortStatus> portStatuses, CancellationToken cToken);
    }
}