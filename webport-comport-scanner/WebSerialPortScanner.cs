using webport_comport_scanner.Architecture;
using webport_comport_scanner.Parser;

namespace webport_comport_scanner
{
    public class WebSerialPortScanner
    {
        public static void Main(string[] args)
        {
            IArgumentParser argumentParser = new ArgumentParserWsp();
            argumentParser.Parse(args);
        }
    }
}
