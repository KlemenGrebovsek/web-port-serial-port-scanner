using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using webport_comport_scanner.Architecture;

namespace webport_comport_scanner.Printer
{
    /// <summary>
    /// Provides functionality to print port scan results to console.
    /// </summary>
    public class PortStatusPrinter : IPortStatusPrinter
    {
        // Represents first column header text.
        private const string PortHeader = "PORT NAME";

        // Represents second column header text.
        private const string StatusHeader = "STATUS";

        // Represents column width (number of chars).
        private const int ColWidth = 13;
        
        private readonly BufferedStream _bufferedStream;

        public PortStatusPrinter()
        {
            Console.OutputEncoding = Encoding.Unicode;
            _bufferedStream = new BufferedStream(Console.OpenStandardOutput(), 8192);
        }
        
        /// <summary>
        /// Prints a collection of port status in console as table. 
        /// </summary>
        /// <param name="portStatuses">A collection of type port status.</param>
        public void PrintTable(IEnumerable<IPrintablePortStatus> portStatuses)
        {
            if (portStatuses == null)
                return;
            
            // Define table line which will be printed after each row.
            var tableLine = Encoding.Unicode.GetBytes($"\n+{new string('-', (ColWidth * 2) + 1 )}+");

            _bufferedStream.Write(Encoding.Unicode.GetBytes($"\n {FillStringToLen(PortHeader, ColWidth)}" +
                                                            $" {FillStringToLen(StatusHeader, ColWidth)} "));

            // Generate rows of table
            foreach (var result in portStatuses)
            {
                _bufferedStream.Write(tableLine);
                _bufferedStream.Write(Encoding.Unicode.GetBytes($"\n|{FillStringToLen(result.GetName(), ColWidth)}|" +
                                                                $"{FillStringToLen(result.GetStatusString(), ColWidth)}|"));
            }
            
            _bufferedStream.Write(tableLine);
            _bufferedStream.Flush();
            _bufferedStream.Close();
        }
        
        /// <summary>
        /// Fill string with empty chars to length.
        /// </summary>
        /// <param name="value">Value to print.</param>
        /// <param name="length">Column length.</param>
        /// <returns>A string of length of column filled with empty sequence.</returns>
        private static string FillStringToLen(string value, int length)
        {
            return string.Format($"{{0,{length * -1}}}", string.Format("{0," +
                ((length + value.Length) / 2) + "}", value));
        }
    }
}
