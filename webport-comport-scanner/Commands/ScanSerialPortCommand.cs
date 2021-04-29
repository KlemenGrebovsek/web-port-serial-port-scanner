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
    public class ScanSerialPortCommand : Command<ProgramOptions, CommandOptions>
    {
        private readonly IPortStatusPrinter _printer;
        private readonly IPortScanner _scanner;
        
        public ScanSerialPortCommand()
        {
            _printer = new PortStatusPrinter(Console.Out);
            _scanner = new SerialPortScanner();
        }
        
        public override void OnConfigure(ICommandConfigurationBuilder<CommandOptions> builder)
        {
            base.OnConfigure(builder);

            builder
                .Name("serialPort")
                .Required(false)
                .Description("This command scans serial ports.");
        }

        public override async Task OnExecuteAsync(ProgramOptions pOptions, CommandOptions cOptions, CancellationToken cToken)
        {
            var scanProperties = pOptions.Adapt<ScanProperties>();
            var scanResult = await _scanner.ScanAsync(scanProperties, cToken);
            
            if (pOptions.Status != PortStatus.Any)
            {
                await _printer.PrintTableAsync(scanResult
                    .Where(x => x.GetPortStatus() == pOptions.Status), cToken);
                    
                return;
            }

            await _printer.PrintTableAsync(scanResult, cToken);
        }
    }
}