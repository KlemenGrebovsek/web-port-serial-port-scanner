using System.Collections.Generic;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Arhitecture
{
    public interface IResultPrinter
    {
        /// <summary>
        /// Prints scanning results as a table.
        /// </summary>
        /// <param name="scanResults">Collection of scanning results.</param>
        void PrintTable(IEnumerable<IPrintableScanResult> scanResults);
    }
}
