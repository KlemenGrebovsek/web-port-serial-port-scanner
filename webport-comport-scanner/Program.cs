using webport_comport_scanner.Arhitecture;
using webport_comport_scanner.Parser;

namespace webport_comport_scanner
{
    class Program
    {
        static void Main(string[] args)
        {
            IArgumentParser argumentParser = new ArgumentParserWSP();
            argumentParser.Parse(args);
        }
    }
}
