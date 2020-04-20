using System;
using webport_comport_scanner.Models;
using System.Linq;

namespace webport_comport_scanner.Printer
{
    public class ResultPrinter
    {
        public void PrintR(IPrintable[] data, string title, string value)
        {
            int maxChar = data.Max(x => x.GetPrintMaxLenght());

            maxChar = (title.Length > maxChar) ? title.Length : maxChar;
            maxChar = (value.Length > maxChar) ? value.Length : maxChar;
            maxChar = (maxChar < 10) ? 10 : maxChar;

            string specLine = $"+{GetLine((maxChar * 2) + 2)}+";

            Console.WriteLine($"\n {FillStringToLenght(title, maxChar)} {FillStringToLenght(value, maxChar)} ");

            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(specLine);
                Console.WriteLine($"|{FillStringToLenght(data[i].GetName(), maxChar)}|{FillStringToLenght(data[i].GetValue(), maxChar)}|");
            }

            Console.WriteLine(specLine);
        }


        private string FillStringToLenght(string value, int lenght)
        {
            return String.Format($"{{0,{lenght * -1}}}", String.Format("{0," + ((lenght + value.Length) / 2).ToString() + "}", value));
        }

        private string GetLine(int length)
        {
            if (length < 1)
                return "";

            return new string('-', length - 1);
        }
    }
}
