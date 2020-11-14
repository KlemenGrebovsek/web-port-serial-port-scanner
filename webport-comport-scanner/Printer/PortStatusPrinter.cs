using System;
using System.Linq;
using System.Collections.Generic;
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

        // Represents min column width (number of chars).
        private const int MinColWidth = 11;


        /// <summary>
        /// Prints a collection of port status on console as table. 
        /// </summary>
        /// <param name="portStatuses">A collection of type port status.</param>
        public void PrintTable(IEnumerable<IPrintablePortStatus> portStatuses)
        {
            if (portStatuses == null)
                return;

            var statuses = portStatuses as IPrintablePortStatus[] ?? portStatuses.ToArray();
            var tableOutput = new StringBuilder(); 

            // Idea behind this variable is to get equal width of each column in table.
            
            var columnWidth = GetColumnWidth(statuses.Max(x => x.GetMaxPrintLen()));

            // Define table line which will be printed after each row.
            var tableLine = $"\n+{new string('-', (columnWidth * 2) + 1 )}+";

            tableOutput.Append($"\n {FillStringToLen(PortHeader, columnWidth)}" +
                $" {FillStringToLen(StatusHeader, columnWidth)} ");

            // Generate rows of table
            foreach (var result in statuses)
            {
                tableOutput.Append(tableLine);
                tableOutput.Append($"\n|{FillStringToLen(result.GetName(), columnWidth)}|" +
                    $"{FillStringToLen(result.GetStatus(), columnWidth)}|");
            }

            tableOutput.Append(tableLine);

            Console.WriteLine(tableOutput.ToString());
        }

        /// <summary>
        /// Calculates recommended column width for this data set.
        /// </summary>
        /// <param name="maxValWidth">Length of longest value in table.</param>
        /// <returns>An integer, recommended column width.</returns>
        private static int GetColumnWidth(int maxValWidth)
        {
            int recWidth;

            // Get max width of table column headers.
            var maxHeaderWidth = PortHeader.Length > StatusHeader.Length ? PortHeader.Length : StatusHeader.Length;

            // Width of column shouldn't be less than min width.
            if (maxValWidth < MinColWidth && maxHeaderWidth < MinColWidth)
                recWidth = MinColWidth;
            else
                recWidth = (maxHeaderWidth > maxValWidth) ? maxHeaderWidth: maxValWidth;
            
            return recWidth;
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
                ((length + value.Length) / 2).ToString() + "}", value));
        }
    }
}
