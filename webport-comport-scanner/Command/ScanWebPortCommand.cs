using System;
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
            Console.WriteLine("Scanning for web ports...");
            
            if (!Enum.TryParse(pOptions.Status, out PortStatus portStatus))
            {
                Console.WriteLine("Invalid port status.");
                return;
            }
            
            try
            {
                var portScanner = new WebPortScanner();
                var settings = new ScanProperties(pOptions.MinPort, pOptions.MaxPort, portStatus);

                var scanResult = await portScanner.ScanAsync(settings, cToken);
                
                new PortStatusPrinter().PrintTable(scanResult);
                
                Console.WriteLine("\nDone.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Command 'webPort', ran into exception: {e.Message}");
            }
        }
    }
}