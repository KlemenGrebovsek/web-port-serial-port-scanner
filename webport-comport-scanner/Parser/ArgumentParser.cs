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
                    AppName = "Com and web port scanner.", 
                }
            );

            argParser.UseFluentValidations(configurator => 
                        configurator.AddValidator<ProgramOptions, ProgramOptionValidator>());

            argParser.AddCommand()
                .Name("scanWEB")
                .Required(false)
                .Description("This command scans web ports.")
                .OnExecuting((o) => {
                    Console.WriteLine("Scanning web ports...");

                    IScanner webScanner = new WebScanner();
                    IResultPrinter printer = new ResultPrinter();

                    try  
                    { 
                        printer.PrintR(webScanner.Scan(o), "PORT", "STATUS");
                    } 
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                    
                });

            argParser.AddCommand()
                .Name("scanCOM")
                .Required(false)
                .Description("This command scans com ports.")
                .OnExecuting((o) => {
                    IScanner webScanner = new ComScanner();
                    IResultPrinter printer = new ResultPrinter();

                    try
                    {
                        printer.PrintR(webScanner.Scan(o), "PORT", "STATUS");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
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
            Console.WriteLine("\nCommands:");

            for (int i = 0; i < argParser.Commands.Count; i++)
                Console.WriteLine($"{argParser.Commands[i].Name} -> {argParser.Commands[i].Description}");

            Console.WriteLine("\nArgument options (web) :");

            for (int i = 0; i < argParser.Options.Count; i++)
                Console.WriteLine($"{argParser.Options[i].ShortName}  {argParser.Options[i].LongName}" +
                    $" {argParser.Options[i].Description}");
        }

    }
}
