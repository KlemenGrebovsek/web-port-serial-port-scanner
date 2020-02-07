using System;
using MatthiWare.CommandLine;
using webport_comport_scanner.Options;
using webport_comport_scanner.Scanners;

namespace webport_comport_scanner.Parser
{
    class ArgumentParser
    {
        private static CommandLineParser<ProgramOptions> argParser;

        public ArgumentParser()
        {
            argParser = new CommandLineParser<ProgramOptions>(
                new CommandLineParserOptions { AppName = "Com and web port scanner.", EnableHelpOption = true });

            AddCommandToParser("scanWEB", "Scans web ports.", false, new WebScanner());
            AddCommandToParser("scanCOM", "Scans com ports.", false, new ComScanner());
        }

        public void Parse(ref string[] args)
        {
            var parseResult = argParser.Parse(args);

            if (parseResult.HasErrors)
            {
                Console.WriteLine("Error : Failed to parse arguments");
                return;
            }

            ProgramOptions programOptions = parseResult.Result;

            if (ValidatePorts(ref programOptions, out string error))
                Console.WriteLine(String.Format("Arguments validation error : {0}.", error));

        }

        public void DisplayOptions()
        {
            var argOptions = argParser.Options;
            Console.WriteLine("\nArgument options:");

            for (int i = 0; i < argOptions.Count; i++)
                Console.WriteLine($"{argOptions[i].ShortName}  {argOptions[i].LongName}  {argOptions[i].Description} ");
        }

        private void AddCommandToParser(string name, string description, bool required, IScanner scanner)
        {
            argParser.AddCommand<ProgramOptions>()
                .Name(name)
                .Required(required)
                .Description(description)
                .OnExecuting((p) => { 
                    scanner.Scan(p);
                });
        }

        private bool ValidatePorts(ref ProgramOptions programOptions, out string error)
        {
            if (programOptions.MaxPort > 65535 || programOptions.MinPort < 0)
            {
                error = "Port range must be between 0 and 65535";
                return true;
            }

            error = "";
            return false;
        } 
    }
}
