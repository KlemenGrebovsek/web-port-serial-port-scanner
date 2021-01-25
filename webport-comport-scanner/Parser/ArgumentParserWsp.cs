using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MatthiWare.CommandLine;
using webport_comport_scanner.Option;
using MatthiWare.CommandLine.Extensions.FluentValidations;
using webport_comport_scanner.Validator;

namespace webport_comport_scanner.Parser
{
    /// <summary>
    /// Web and serial port args parser.
    /// </summary>
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

        /// <summary>
        /// Parses given arguments and starts executing command.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        /// <returns>Collection of errors if any.</returns>
        public async Task<IEnumerable<string>> ParseAsync(string[] args)
        {
            var parserResult = await _argParser.ParseAsync(args);

            if (!parserResult.HasErrors) 
                return parserResult.Errors.Select(x => x.Message);

            return Enumerable.Empty<string>();
        }
    }
}
