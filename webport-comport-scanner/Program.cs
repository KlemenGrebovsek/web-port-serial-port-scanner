using System;
using webport_comport_scanner.Parser;

namespace webport_comport_scanner
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgumentParser argumentParser = new ArgumentParser();
            argumentParser.Parse(args);
            Console.WriteLine("\nPress any key to close the program...");
            Console.ReadKey();
        }
    }
}
