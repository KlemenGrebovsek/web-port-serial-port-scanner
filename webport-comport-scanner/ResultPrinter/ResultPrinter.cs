using System;
using webport_comport_scanner.Models;
using System.Linq;
using webport_comport_scanner.Arhitecture;
using System.Collections.Generic;

namespace webport_comport_scanner.Printer
{
    public class ResultPrinter : IResultPrinter
    {
        const string portColumnHeader = "PORT";
        const string statusColumnHeader = "STATUS";

        public void PrintTable(IEnumerable<IPrintableScanResult> scanResults)
        {
            int printColSize = scanResults.Count();

            if (printColSize == 0)
            {
                Console.WriteLine("No port found.");
                return;
            }

            int maxPrintLenColumn = scanResults.Max(x => x.GetMaxPrintLenght());

            maxPrintLenColumn = (portColumnHeader.Length > maxPrintLenColumn) ? portColumnHeader.Length : maxPrintLenColumn;
            maxPrintLenColumn = (statusColumnHeader.Length > maxPrintLenColumn) ? statusColumnHeader.Length : maxPrintLenColumn;
            maxPrintLenColumn = (maxPrintLenColumn < 10) ? 10 : maxPrintLenColumn;

            string tableLine = $"+{new string('-', (maxPrintLenColumn * 2) + 1 )}+";

            Console.WriteLine($"\n {FillStringToLenght(portColumnHeader, maxPrintLenColumn)}" +
                $" {FillStringToLenght(statusColumnHeader, maxPrintLenColumn)} ");

            for (int i = 0; i < printColSize; i++)
            {
                Console.WriteLine(tableLine);

                Console.WriteLine($"|{FillStringToLenght(scanResults.ElementAt(i).GetName(), maxPrintLenColumn)}|" +
                    $"{FillStringToLenght(scanResults.ElementAt(i).GetStatus(), maxPrintLenColumn)}|");
            }

            Console.WriteLine(tableLine);
        }

        private string FillStringToLenght(string value, int lenght)
        {
            return String.Format($"{{0,{lenght * -1}}}", String.Format("{0," +
                ((lenght + value.Length) / 2).ToString() + "}", value));
        }
    }
}
