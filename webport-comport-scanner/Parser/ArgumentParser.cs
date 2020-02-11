using System;
using MatthiWare.CommandLine;
using webport_comport_scanner.Options;
using webport_comport_scanner.Scanners;
using MatthiWare.CommandLine.Extensions.FluentValidations;

namespace webport_comport_scanner.Parser
{
    class ArgumentParser
    {
        private static CommandLineParser<ProgramOptions> argParser;

        public ArgumentParser()
        {
            argParser = new CommandLineParser<ProgramOptions>(new CommandLineParserOptions{
                AppName = "Com and web port scanner."}
            );

            argParser.UseFluentValidations(configurator => 
                        configurator.AddValidator<ProgramOptions, ProgramOptionValidator>());

            argParser.AddCommand()
                .Name("scanWEB")
                .Required(false)
                .Description("This command scans web ports.")
                .OnExecuting((o) => {
                    WebScanner webScanner = new WebScanner();
                    webScanner.Scan(o);
                });

            argParser.AddCommand()
                .Name("scanCOM")
                .Required(false)
                .Description("This command scans com ports.")
                .OnExecuting((o) => {
                    ComScanner comScanner = new ComScanner();
                    comScanner.Scan(o);
                });

        }

        public void Parse(ref string[] args)
        {
            if(args.Length < 1 || argParser.Parse(args).HasErrors)
            {
                Console.WriteLine("Error: No command or arguemtns given.");
                DisplayOptions();
                return;
            }
        }

        private void DisplayOptions()
        {
            Console.WriteLine("\nCommands:");

            for (int i = 0; i < argParser.Commands.Count; i++)
                Console.WriteLine($"{argParser.Commands[i].Name} -> {argParser.Commands[i].Description}");

            Console.WriteLine("\nArgument options (web) :");

            for (int i = 0; i < argParser.Options.Count; i++)
                Console.WriteLine($"{argParser.Options[i].ShortName}  {argParser.Options[i].LongName} {argParser.Options[i].Description}");
        }

    }
}
