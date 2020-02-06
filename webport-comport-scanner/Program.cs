using MatthiWare.CommandLine;
using System;
using webport_comport_scanner.Options;
using webport_comport_scanner.Parser;

namespace webport_comport_scanner
{
    class Program
    {
        private static void Close()
        {
            Console.WriteLine("\nPress any key to close the program...");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Error : No arguments specified.");
                Close();
                return;
            }

            ArgumentParser argumentParser = new ArgumentParser();
            argumentParser.Parse(ref args);
            Close();
        }
    }
}
