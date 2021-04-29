using System.Reflection;
using System.Threading.Tasks;
using MatthiWare.CommandLine;
using MatthiWare.CommandLine.Extensions.FluentValidations;
using webport_comport_scanner.Options;
using webport_comport_scanner.Validators;

namespace webport_comport_scanner.ArgumentParsers
{
    public class ArgumentParserWsp : IArgumentParser
    {
        private readonly CommandLineParser<ProgramOptions> _argParser;

        public ArgumentParserWsp()
        {
            var parserOptions = new CommandLineParserOptions
            {
                AppName = "Serial and web port scanner.",
                EnableHelpOption = true,
                AutoPrintUsageAndErrors = true
            };
            
            _argParser = new CommandLineParser<ProgramOptions>(parserOptions);
            
            _argParser.UseFluentValidations(configurator => configurator
                      .AddValidator<ProgramOptions, ProgramOptionsValidator>());
            
            _argParser.AddCommand()
                .Name("help")
                .Required(false)
                .Description("This command displays program options.")
                .OnExecuting((o) => _argParser.Printer.PrintUsage());
            
            _argParser.DiscoverCommands(Assembly.GetExecutingAssembly());
        }
        
        public async Task ParseAsync(string[] args)
        {
            await _argParser.ParseAsync(args);
        }
    }
}
