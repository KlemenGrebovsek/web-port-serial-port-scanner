using System.Threading.Tasks;
using webport_comport_scanner.ArgumentParsers;

namespace webport_comport_scanner
{
    public static class WebSerialPortScanner
    {
        public static async Task Main(string[] args)
        {
            await new ArgumentParserWsp()
                .ParseAsync(args);
        }
    }
}
