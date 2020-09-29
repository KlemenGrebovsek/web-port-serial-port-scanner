using System;
using webport_comport_scanner.Models;
using System.Linq;
using webport_comport_scanner.Arhitecture;
using System.Collections.Generic;
using System.Text;

namespace webport_comport_scanner.Printer
{
    /// <summary>
    /// Provides functionality to print port scan results to console.
    /// </summary>
    public class PortStatusPrinter : IPortStatusPrinter
    {
        const string _portHeader = "PORT";
        const string _statusHeader = "STATUS";
        const int _minColWidth = 10;

        /// <summary>
        /// Prints a collection of port status on console as table. 
        /// </summary>
        /// <param name="portStatusCollection">A collection of type port status.</param>
        public void PrintTable(IEnumerable<IPrintablePortStatus> portStatusCollection)
        {
            if (portStatusCollection == null || !portStatusCollection.Any())
                return;

            StringBuilder tableOutput = new StringBuilder(); 

            // Idea behind this variable is to get equal width of each column in table.
            int columnWidth = GetColumnWidth(portStatusCollection.Max(x => x.GetMaxPrintLenght()));

            // Define table line which will be used after each row.
            string tableLine = $"\n+{new string('-', (columnWidth * 2) + 1 )}+";

            tableOutput.Append($"\n {FillStringToLenght(_portHeader, columnWidth)}" +
                $" {FillStringToLenght(_statusHeader, columnWidth)} ");

            // Generate rows of table
            foreach (IPrintablePortStatus result in portStatusCollection)
            {
                tableOutput.Append(tableLine);
                tableOutput.Append($"\n|{FillStringToLenght(result.GetName(), columnWidth)}|" +
                    $"{FillStringToLenght(result.GetStatus(), columnWidth)}|");
            }

            tableOutput.Append(tableLine);

            Console.WriteLine(tableOutput.ToString());
        }

        /// <summary>
        /// Calculates best column width for this data set.
        /// </summary>
        /// <param name="maxValueWidth">Length of longest value in table.</param>
        /// <returns>An integer, recommended column width.</returns>
        private int GetColumnWidth(int maxValueWidth)
        {
            int recommendedWidth;

            // Get max width of table column headers.
            int maxColHeaderWidth = (_portHeader.Length > _statusHeader.Length) ? _portHeader.Length : _statusHeader.Length;

            // Width of column should not be less than min width.
            if (maxValueWidth < _minColWidth && maxColHeaderWidth < _minColWidth)
            {
                recommendedWidth = _minColWidth;
            }
            else
            {
                recommendedWidth = (maxColHeaderWidth > maxValueWidth) ? maxColHeaderWidth: maxValueWidth;
            }

            return recommendedWidth;
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
