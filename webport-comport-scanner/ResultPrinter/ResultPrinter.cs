using System;
using webport_comport_scanner.Models;
using System.Linq;
using webport_comport_scanner.Arhitecture;
using System.Collections.Generic;

namespace webport_comport_scanner.Printer
{
    public class ResultPrinter : IResultPrinter
    {
        public void PrintR(IEnumerable<IPrintable> data, string title, string value)
        {
            int printColSize = data.Count();

            if (printColSize == 0) return;

            int maxChar = data.Max(x => x.GetPrintMaxLenght());

            maxChar = (title.Length > maxChar) ? title.Length : maxChar;
            maxChar = (value.Length > maxChar) ? value.Length : maxChar;
            maxChar = (maxChar < 10) ? 10 : maxChar;

            string tableLine = $"+{new string('-', (maxChar * 2) + 1 )}+";

            Console.WriteLine($"\n {FillStringToLenght(title, maxChar)} {FillStringToLenght(value, maxChar)} ");

            for (int i = 0; i < printColSize; i++)
            {
                Console.WriteLine(tableLine);
                Console.WriteLine($"|{FillStringToLenght(data.ElementAt(i).GetName(), maxChar)}|{FillStringToLenght(data.ElementAt(i).GetValue(), maxChar)}|");
            }

            Console.WriteLine(tableLine);
        }

        private string FillStringToLenght(string value, int lenght)
        {
            return String.Format($"{{0,{lenght * -1}}}", String.Format("{0," + ((lenght + value.Length) / 2).ToString() + "}", value));
        }

    }
}
