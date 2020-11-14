using System.Collections.Generic;

namespace webport_comport_scanner.Architecture
{
    public interface IPortStatusPrinter
    {
        /// <summary>
        /// Prints collection of port status as a table.
        /// </summary>
        /// <param name="portStatuses">A collection of type printable port status.</param>
        void PrintTable(IEnumerable<IPrintablePortStatus> portStatuses);
    }
}
