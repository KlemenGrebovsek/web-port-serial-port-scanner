using System.Collections.Generic;
using webport_comport_scanner.Models;
using webport_comport_scanner.Options;

namespace webport_comport_scanner.Scanners
{
    interface IPortScanner
    {
        /// <summary>
        /// Scans comport, webport...
        /// </summary>
        /// <param name="options">Program options generated from args parser.</param>
        /// <returns>Collection of scan status.</returns>
        public IEnumerable<IPrintablePortResult> Scan(ProgramOptions options);
    }
}
