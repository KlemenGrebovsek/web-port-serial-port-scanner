using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using webport_comport_scanner.Model;

namespace webport_comport_scanner.Printer
{
    /// <summary>
    /// Provides functionality to print port scan result to console.
    /// </summary>
    public class PortStatusPrinter : IPortStatusPrinter
    {
        // Represents first column header text.
        private const string PortHeader = "PORT NAME";

        // Represents second column header text.
        private const string StatusHeader = "STATUS";

        // Represents column width (number of chars).
        private const int ColWidth = 13;

        private readonly TextWriter _writer;

        public PortStatusPrinter(TextWriter textWriter)
        {
            _writer = textWriter;
        }
        
        /// <summary>
        /// Prints a collection of port status in console as table. 
        /// </summary>
        /// <param name="portStatuses">A collection of type port status.</param>
        /// <param name="cToken">A cancellation token</param>
        public async Task PrintTableAsync(IEnumerable<IPrintablePortStatus> portStatuses, CancellationToken cToken)
        {
            if (!portStatuses.Any())
            {
                await _writer.WriteAsync("No port to print.");
                await _writer.FlushAsync();
                return;
            }
            
            // Define table line which will be printed after each row.
            var tableLine = $"\n+{new string('-', (ColWidth * 2) + 1 )}+";
            
            // Table header print
            await _writer.WriteAsync($"\n {FillStringToLen(PortHeader, ColWidth)}"); 
            await _writer.WriteAsync($" {FillStringToLen(StatusHeader, ColWidth)} ");
            
            // Generate rows of table
            foreach (var result in portStatuses)
            {
                cToken.ThrowIfCancellationRequested();
                
                await _writer.WriteAsync(tableLine);
                await _writer.WriteAsync($"\n|{FillStringToLen(result.GetName(), ColWidth)}|");
                await _writer.WriteAsync($"{FillStringToLen(result.GetStatusString(), ColWidth)}|");
            }
            
            await _writer.WriteAsync(tableLine + "\n");
            await _writer.FlushAsync();
        }
        
        /// <summary>
        /// Fills string with empty chars to length.
        /// </summary>
        /// <param name="value">Value to fill.</param>
        /// <param name="length">Expected string length.</param>
        /// <returns>A string of length of column filled with empty sequence.</returns>
        private static string FillStringToLen(string value, int length)
        {
            return string.Format($"{{0,{length * -1}}}", string.Format($"{{0,{(length + value.Length) / 2}}}", value));
        }
    }
}
