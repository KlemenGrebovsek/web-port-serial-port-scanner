using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Printers
{
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
        
        public async Task PrintTableAsync(IEnumerable<IPrintablePortStatus> portStatuses, CancellationToken cToken)
        {
            if (!portStatuses.Any())
                return;
            
            await using (_writer)
            {
                // Define table line.
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
        }
        
        private static string FillStringToLen(string value, int length)
        {
            return string.Format($"{{0,{length * -1}}}", string.Format($"{{0,{(length + value.Length) / 2}}}", value));
        }
    }
}
