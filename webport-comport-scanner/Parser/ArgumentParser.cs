using System;
using MatthiWare.CommandLine;
using webport_comport_scanner.Options;
using webport_comport_scanner.Scanners;
using MatthiWare.CommandLine.Extensions.FluentValidations;
using System.Linq;
using MatthiWare.CommandLine.Abstractions.Parsing;
using webport_comport_scanner.Printer;
using webport_comport_scanner.Arhitecture;

namespace webport_comport_scanner.Parser
{
    class ArgumentParser
    {
        private static CommandLineParser<ProgramOptions> argParser;

        public ArgumentParser()
        {
            argParser = new CommandLineParser<ProgramOptions>(
                new CommandLineParserOptions
                {
                    AppName = "Serial and web port scanner.", 
                    EnableHelpOption = true,
                }
            );

            argParser.UseFluentValidations(configurator => 
                        configurator.AddValidator<ProgramOptions, ProgramOptionValidator>());

            argParser.AddCommand()

                .Name("webPort")
                .Required(false)
                .Description("This command scans web ports. (Displays only used ones.)")
                .OnExecuting((o) => {

                    Console.WriteLine("Scanning web ports...");

                    IPortScanner webScanner = new WebPortScanner();
                    IResultPrinter printer = new ResultPrinter();

                    printer.PrintTable(webScanner.Scan(o));
                    Console.WriteLine("\n Done!");
                });

            argParser.AddCommand()
                .Name("serialPort")
                .Required(false)
                .Description("This command scans serial ports. (Displays all.)")
                .OnExecuting((o) => {

                    Console.WriteLine("Scanning serial ports...");
                    IPortScanner webScanner = new SerialPortScanner();
                    IResultPrinter printer = new ResultPrinter();

                    printer.PrintTable(webScanner.Scan(o));
                    Console.WriteLine("\n Done!");
                });

            argParser.AddCommand()
                .Name("help")
                .Required(false)
                .Description("This command displays program options.")
                .OnExecuting((o) => {
                    DisplayOptions();
                });

        }

        public void Parse(string[] args)
        {
            if(args.Length < 1)
            {
                Console.WriteLine("Error: No command or arguments given.");
                DisplayOptions();
                return;
            }

            IParserResult<ProgramOptions> programOptions = argParser.Parse(args);

            if (programOptions.HasErrors)
            {
                Console.WriteLine("Parse errors:");

                for (int i = 0; i < programOptions.Errors.Count; i++)
                    Console.WriteLine($"Error({i}) -> {programOptions.Errors.ElementAt(i).Message}");

                DisplayOptions();
            }
        }

        private void DisplayOptions()
        {
            Console.WriteLine();
            argParser.Parse(new string[1] { "--help" });
        }
    }
}
