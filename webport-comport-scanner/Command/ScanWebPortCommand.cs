using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MatthiWare.CommandLine.Abstractions.Command;
using webport_comport_scanner.Model;
using webport_comport_scanner.Option;
using webport_comport_scanner.Printer;
using webport_comport_scanner.Scanner;

namespace webport_comport_scanner.Command
{
    /// <summary>
    /// Scans for web ports and their status.
    /// </summary>
    public class ScanWebPortCommand : Command<ProgramOptions, CommandOptions>
    {
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
            if (!Enum.TryParse(pOptions.Status, out PortStatus portStatus))
            {
                Console.WriteLine("Invalid port status.");
                return;
            }
            
            Console.WriteLine("Scanning for web ports...");
            
            try
            {
                var portScanner = new WebPortScanner();
                var scanResult = await Task.WhenAll(portScanner.Scan(pOptions.MinPort, pOptions.MaxPort, cToken));
                var printer = new PortStatusPrinter(Console.Out);

                if (portStatus != PortStatus.Any)
                {
                    await printer.PrintTableAsync(scanResult
                        .Where(x => x.GetStatusString() == pOptions.Status), cToken);
                }
                else
                {
                    await printer.PrintTableAsync(scanResult, cToken);
                }
            }
            catch (Exception e)
            {
                if (!cToken.IsCancellationRequested)
                    Console.WriteLine($"Command 'webPort', ran into exception: {e.Message}");
            }
        }
    }
}