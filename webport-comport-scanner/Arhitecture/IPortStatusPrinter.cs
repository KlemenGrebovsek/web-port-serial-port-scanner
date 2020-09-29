using System.Collections.Generic;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Arhitecture
{
    public interface IPortStatusPrinter
    {
        /// <summary>
        /// Prints collection of port status as a table.
        /// </summary>
        /// <param name="portStatusData">A collection of type printable port status.</param>
        void PrintTable(IEnumerable<IPrintablePortStatus> portStatusData);
    }
}
