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
                .Description("This command scans web ports.")
                .OnExecuting((o) => {

                    Console.WriteLine("Scanning web ports...");

                    IPortScanner webPortScanner = new WebPortScanner();
                    IResultPrinter printer = new ResultPrinter();

                    printer.PrintTable(webPortScanner.Scan(o));
                    Console.WriteLine("\nDone!");
                });

            argParser.AddCommand()
                .Name("serialPort")
                .Required(false)
                .Description("This command scans serial ports.")
                .OnExecuting((o) => {

                    Console.WriteLine("Scanning serial ports...");
                    IPortScanner serialPortScanner = new SerialPortScanner();
                    IResultPrinter printer = new ResultPrinter();

                    printer.PrintTable(serialPortScanner.Scan(o));
                    Console.WriteLine("\nDone!");
                });

            argParser.AddCommand()
                .Name("help")
                .Required(false)
                .Description("This command displays program options.")
                .OnExecuting((o) => {
                    argParser.Printer.PrintUsage();
                });

        }

        public void Parse(string[] args)
        {
            if(args.Length < 1)
            {
                Console.WriteLine("Error: No command or arguments given.");
                argParser.Printer.PrintUsage();
                return;
            }

            if (!argParser.Commands.Any(x => x.Name == args[0]) && args[0] != "--help")
            {
                Console.WriteLine("Error: Invalid command given.");
                argParser.Printer.PrintUsage();
                return;
            }

            IParserResult<ProgramOptions> parserResult = argParser.Parse(args);

            if (parserResult.HasErrors)
            {
                Console.WriteLine("Parse errors:");

                for (int i = 0; i < parserResult.Errors.Count; i++)
                    Console.WriteLine($"Error({i}) -> {parserResult.Errors.ElementAt(i).Message}");

                argParser.Printer.PrintUsage();
            }
        }
    }
}
