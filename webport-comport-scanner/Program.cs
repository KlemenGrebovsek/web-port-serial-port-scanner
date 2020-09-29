using webport_comport_scanner.Arhitecture;
using webport_comport_scanner.Parser;

namespace webport_comport_scanner
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[] {"webPort", "-s", "free"};
            IArgumentParser argumentParser = new ArgumentParserWSP();
            argumentParser.Parse(args);
        }
    }
}
