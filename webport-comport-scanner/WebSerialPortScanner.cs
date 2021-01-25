using System;
using System.Threading.Tasks;
using webport_comport_scanner.Parser;

namespace webport_comport_scanner
{
    public class WebSerialPortScanner
    {
        public static async Task Main(string[] args)
        {
            var parser = new ArgumentParserWsp();
            
            var parseErrors = await parser.ParseAsync(args);

            // print if any error
            foreach (var error in parseErrors)
                Console.WriteLine(error);
        }
    }
}
