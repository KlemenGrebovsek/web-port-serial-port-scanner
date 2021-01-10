using System.Collections.Generic;
using webport_comport_scanner.Model;

namespace webport_comport_scanner.Printer
{
    public interface IPortStatusPrinter
    {
        /// <summary>
        /// Prints collection of ports statuses as a table.
        /// </summary>
        /// <param name="portStatuses">A collection of type IPrintablePortStatus.</param>
        void PrintTable(IEnumerable<IPrintablePortStatus> portStatuses);
    }
}
