using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MatthiWare.CommandLine.Abstractions.Command;
using webport_comport_scanner.Models;
using webport_comport_scanner.Options;
using webport_comport_scanner.Printers;
using webport_comport_scanner.Scanners;

namespace webport_comport_scanner.Commands
{
    /// <summary>
    /// Scans for web ports and their status.
    /// </summary>
    public class ScanWebPortCommand : Command<ProgramOptions, CommandOptions>
    {
        private readonly IPortStatusPrinter _printer;
        private readonly IPortScanner _scanner;
        
        public ScanWebPortCommand()
        {
            _printer = new PortStatusPrinter(Console.Out);
            _scanner = new WebPortScanner();
        }
        public override void OnConfigure(ICommandConfigurationBuilder<CommandOptions> builder)
        {
            base.OnConfigure(builder);

            builder
                .Name("webPort")
                .Required(false)
                .Description("This command scans web ports.");
        }

        public override async Task OnExecuteAsync(ProgramOptions pOptions, CommandOptions cOptions, CancellationToken cToken)
        {
            var scanProperties = pOptions.Adapt<ScanProperties>();
            var scanResult = await _scanner.ScanAsync(scanProperties, cToken);
            
            if (pOptions.Status != PortStatus.Any)
            {
                await _printer.PrintTableAsync(scanResult
                    .Where(x => x.PortStatus == pOptions.Status), cToken);
                    
                return;
            }

            await _printer.PrintTableAsync(scanResult, cToken);
        }
    }
}