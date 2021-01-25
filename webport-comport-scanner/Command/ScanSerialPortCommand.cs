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
            Console.WriteLine("Scanning for serial ports...");
            
            var portScanner = new SerialPortScanner();
            var printer = new PortStatusPrinter(Console.Out);
            
            try
            {
                var scanResult = await portScanner.ScanAsync(pOptions.MinPort, pOptions.MaxPort, cToken);
                
                if (pOptions.Status != PortStatus.Any)
                {
                    var stringStatus = pOptions.Status.ToString();
                    
                    await printer.PrintTableAsync(scanResult
                                 .Where(x => x.GetStatusString() == stringStatus), cToken);
                }
                else
                {
                    await printer.PrintTableAsync(scanResult, cToken);
                }
            }
            catch (TaskCanceledException) { }
            catch (Exception e)
            {
                Console.WriteLine($"Command 'serialPort', ran into exception: {e.Message}");
            }
        }
    }
}