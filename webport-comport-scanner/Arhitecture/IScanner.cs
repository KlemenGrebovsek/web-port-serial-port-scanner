using System.Collections.Generic;
using webport_comport_scanner.Models;
using webport_comport_scanner.Options;

namespace webport_comport_scanner.Scanners
{
    interface IScanner
    {
        public IEnumerable<IPrintable> Scan(ProgramOptions options);
    }
}
