using System.Collections.Generic;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Arhitecture
{
    public interface IResultPrinter
    {
        void PrintR(IEnumerable<IPrintable> data, string title, string value);
    }
}
