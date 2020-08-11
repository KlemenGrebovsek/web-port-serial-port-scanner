using System;
using webport_comport_scanner.Models;
using System.Linq;
using webport_comport_scanner.Arhitecture;
using System.Collections.Generic;

namespace webport_comport_scanner.Printer
{
    public class ResultPrinter : IResultPrinter
    {
        const string portColumn = "PORT";
        const string statusColumn = "STATUS";

        public void PrintTable(IEnumerable<IPrintablePortResult> data)
        {
            int printColSize = data.Count();

            if (printColSize == 0)
            {
                Console.WriteLine("No scan results.");
                return;
            }

            int maxPrintLenColumn = data.Max(x => x.GetMaxPrintLenght());

            maxPrintLenColumn = (portColumn.Length > maxPrintLenColumn) ? portColumn.Length : maxPrintLenColumn;
            maxPrintLenColumn = (statusColumn.Length > maxPrintLenColumn) ? statusColumn.Length : maxPrintLenColumn;
            maxPrintLenColumn = (maxPrintLenColumn < 10) ? 10 : maxPrintLenColumn;

            string tableLine = $"+{new string('-', (maxPrintLenColumn * 2) + 1 )}+";

            Console.WriteLine($"\n {FillStringToLenght(portColumn, maxPrintLenColumn)}" +
                $" {FillStringToLenght(statusColumn, maxPrintLenColumn)} ");

            for (int i = 0; i < printColSize; i++)
            {
                Console.WriteLine(tableLine);
                Console.WriteLine($"|{FillStringToLenght(data.ElementAt(i).GetName(), maxPrintLenColumn)}|{FillStringToLenght(data.ElementAt(i).GetStatus(), maxPrintLenColumn)}|");
            }

            Console.WriteLine(tableLine);
        }

        private string FillStringToLenght(string value, int lenght)
        {
            return String.Format($"{{0,{lenght * -1}}}", String.Format("{0," + ((lenght + value.Length) / 2).ToString() + "}", value));
        }
    }
}
