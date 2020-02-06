using System;
using System.Collections.Generic;
using System.Text;

namespace webport_comport_scanner.Scanners
{
    interface IScanner
    {
        public void Scan();

        public void PrintR();
    }
}
