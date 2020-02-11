using MatthiWare.CommandLine;
using System;
using webport_comport_scanner.Parser;

namespace webport_comport_scanner
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgumentParser argumentParser = new ArgumentParser();
            args = new string[] { "scanWEB","-f", "50", "-t", "150" };
            argumentParser.Parse(ref args);
            Console.WriteLine("\nPress any key to close the program...");
            Console.ReadKey();
        }
    }
}
