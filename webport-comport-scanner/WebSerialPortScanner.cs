using webport_comport_scanner.Parser;

namespace webport_comport_scanner
{
    public class WebSerialPortScanner
    {
        public static void Main(string[] args)
        {
            new ArgumentParserWsp().ParseAsync(args);
        }
    }
}
