using System;
using System.Threading;
using System.Threading.Tasks;
using MatthiWare.CommandLine.Abstractions.Command;
using webport_comport_scanner.Model;
using webport_comport_scanner.Option;
using webport_comport_scanner.Printer;
using webport_comport_scanner.Scanner;
using webport_comport_scanner.Util;

namespace webport_comport_scanner.Command
{
    /// <summary>
    /// Searches for serial ports and checks their status.
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
            
            try
            {
                var portScanner = new SerialPortScanner();
                var settings = new ScanProperties(pOptions.MinPort, pOptions.MaxPort, 
                                                                    ProgramUtil.TransformOptionStatus(pOptions.Status));

                var scanResult = await portScanner.ScanAsync(settings, cToken);
                
                var printer = new PortStatusPrinter();
                printer.PrintTable(scanResult);
                
                Console.WriteLine("\nDone.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Command 'serialPort', ran into exception: {e.Message}");
            }
        }
    }
}