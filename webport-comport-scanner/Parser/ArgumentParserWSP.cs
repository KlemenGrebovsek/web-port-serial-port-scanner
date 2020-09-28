using System;
using MatthiWare.CommandLine;
using webport_comport_scanner.Options;
using webport_comport_scanner.Scanners;
using MatthiWare.CommandLine.Extensions.FluentValidations;
using System.Linq;
using webport_comport_scanner.Printer;
using webport_comport_scanner.Arhitecture;
using webport_comport_scanner.Validators;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Parser
{
    /// <summary>
    /// Web and serial port args parser.
    /// </summary>
    public class ArgumentParserWSP : IArgumentParser
    {
        private CommandLineParser<ProgramOptions> argParser;

        public ArgumentParserWSP()
        {
            argParser = new CommandLineParser<ProgramOptions>(
                new CommandLineParserOptions
                {
                    AppName = "Serial and web port scanner.", 
                    EnableHelpOption = true,
                }
            );

            argParser.UseFluentValidations(configurator => 
                        configurator.AddValidator<ProgramOptions, PortOptionValidator>());

            argParser.AddCommand()

                .Name("webPort")
                .Required(false)
                .Description("This command scans web ports.")
                .OnExecuting((o) => 
                {
                    Console.WriteLine("Scanning web ports...");
                    new ResultPrinter().PrintTable(new WebPortScanner().Scan(o.MinPort, o.MaxPort, ParseStatus(o.Status)));
                    Console.WriteLine("\nDone!");
                });

            argParser.AddCommand()
                .Name("serialPort")
                .Required(false)
                .Description("This command scans serial ports.")
                .OnExecuting((o) => 
                {   
                    Console.WriteLine("Scanning serial ports...");
                    new ResultPrinter().PrintTable(new SerialPortScanner().Scan(o.MinPort, o.MaxPort, ParseStatus(o.Status)));
                    Console.WriteLine("\nDone!");
                });

            argParser.AddCommand()
                .Name("help")
                .Required(false)
                .Description("This command displays program options.")
                .OnExecuting((o) => argParser.Printer.PrintUsage());
        }

        /// <summary>
        /// Parses given arguments and starts exectuting commands.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        public void Parse(string[] args)
        {
            if(args.Length < 1)
            {
                Console.WriteLine("Error: No command or arguments given.");
                argParser.Printer.PrintUsage();
                return;
            }

            // check for invalid command/arguments.
            if (!argParser.Commands.Any(x => x.Name == args[0]) && args[0] != "--help")
            {
                Console.WriteLine("Error: Invalid command given.");
                argParser.Printer.PrintUsage();
                return;
            }

            argParser.Parse(args);
        }

        /// <summary>
        /// A string representing interpretation of port status.
        /// </summary>
        /// <exception cref="ArgumentException">If status value is invalid.</exception>
        /// <param name="status"></param>
        /// <returns>Enum type of port status.</returns>
        private PortStatus ParseStatus(string status){
            PortStatus portStatus;

            if (!Enum.TryParse(status.ToUpper(), out portStatus))
                throw new ArgumentException("Invalid status.");

            return portStatus;
        }
    }
}
