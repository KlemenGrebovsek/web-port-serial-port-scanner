using System.Threading.Tasks;
using webport_comport_scanner.Parser;

namespace webport_comport_scanner
{
    public class WebSerialPortScanner
    {
        public static async Task Main(string[] args)
        {
            var parser = new ArgumentParserWsp();
            await Task.Run(() => parser.ParseAsync(args));
        }
    }
}
