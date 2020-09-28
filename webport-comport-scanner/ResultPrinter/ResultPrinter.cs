using System;
using webport_comport_scanner.Models;
using System.Linq;
using webport_comport_scanner.Arhitecture;
using System.Collections.Generic;

namespace webport_comport_scanner.Printer
{
    /// <summary>
    /// Provides functionality to print port scan results to console.
    /// </summary>
    public class ResultPrinter : IPortStatusPrinter
    {
        const string portColumnHeader = "PORT";
        const string portStatusColumnHeader = "STATUS";

        /// <summary>
        /// Prints a collection of IPrintableScanResult on console as table. 
        /// </summary>
        /// <param name="scanResults">A collection of type IPrintableScanResult.</param>
        public void PrintTable(IEnumerable<IPrintablePortStatus> scanResults)
        {
            if (scanResults == null || !scanResults.Any())
                return;

            int maxPrintLenColumn = scanResults.Max(x => x.GetMaxPrintLenght());

            maxPrintLenColumn = (portColumnHeader.Length > maxPrintLenColumn) ? portColumnHeader.Length : maxPrintLenColumn;
            maxPrintLenColumn = (portStatusColumnHeader.Length > maxPrintLenColumn) ? portStatusColumnHeader.Length : maxPrintLenColumn;
            maxPrintLenColumn = (maxPrintLenColumn < 10) ? 10 : maxPrintLenColumn;

            string tableLine = $"+{new string('-', (maxPrintLenColumn * 2) + 1 )}+";

            Console.WriteLine($"\n {FillStringToLenght(portColumnHeader, maxPrintLenColumn)}" +
                $" {FillStringToLenght(portStatusColumnHeader, maxPrintLenColumn)} ");

            foreach (IPrintablePortStatus result in scanResults)
            {
                Console.WriteLine(tableLine);

                Console.WriteLine($"|{FillStringToLenght(result.GetName(), maxPrintLenColumn)}|" +
                    $"{FillStringToLenght(result.GetStatus(), maxPrintLenColumn)}|");
            }

            Console.WriteLine(tableLine);
        }

        /// <summary>
        /// Fill string with empty chars to length.
        /// </summary>
        /// <param name="value">Value to print.</param>
        /// <param name="lenght">Column length.</param>
        /// <returns>A string of length of column filled with empty sequence.</returns>
        private string FillStringToLenght(string value, int lenght)
        {
            return String.Format($"{{0,{lenght * -1}}}", String.Format("{0," +
                ((lenght + value.Length) / 2).ToString() + "}", value));
        }
    }
}
