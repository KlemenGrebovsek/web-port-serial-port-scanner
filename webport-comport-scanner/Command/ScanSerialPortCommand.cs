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
    /// Scans for serial ports and their status.
    /// </summary>
    public class ScanSerialPortCommand : Command<ProgramOptions, CommandOptions>
    {
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
            if (!Enum.TryParse(pOptions.Status, out PortStatus portStatus))
            {
                Console.WriteLine("Invalid port status.");
                return;
            }
            
            Console.WriteLine("Scanning for serial ports...");
            
            try
            {

                var portScanner = new SerialPortScanner();
                var printer = new PortStatusPrinter(Console.Out);
                
                var scanResult =  await Task.WhenAll(portScanner.Scan(pOptions.MinPort, pOptions.MaxPort, cToken));
                
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
                    Console.WriteLine($"Command 'serialPort', ran into exception: {e.Message}");
            }
        }
    }
}