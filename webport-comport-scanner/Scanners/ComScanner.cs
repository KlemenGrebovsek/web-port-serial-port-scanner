using System;
using System.Threading.Tasks;
using webport_comport_scanner.Options;
using System.IO.Ports;
using webport_comport_scanner.Models;
using webport_comport_scanner.Printer;

namespace webport_comport_scanner.Scanners
{
    public class ComScanner : IScanner
    {
        public void Scan(ProgramOptions options)
        {    
            ResultPrinter printer = new ResultPrinter();
            printer.PrintR(Task<ComPortInfo[]>.Factory.StartNew(() => GetComPortsInfo()).Result, "PORT", "STATUS");
        }

        private ComPortInfo[] GetComPortsInfo()
        {
            string[] comPorts = SerialPort.GetPortNames();
            
            if(comPorts.Length < 1)
            {
                Console.WriteLine("No com ports found.");
                return new ComPortInfo[0];
            }

            ComPortInfo[] portsInfo = new ComPortInfo[comPorts.Length];
            Array.Sort(comPorts);

            SerialPort serialPort;

            for (int i = 0; i < comPorts.Length; i++)
            {
                serialPort = new SerialPort(comPorts[i]);
                portsInfo[i] = new ComPortInfo { Name = comPorts[i] };

                try
                {
                    serialPort.Open();
                    portsInfo[i].Status = PortStatus.FREE;
                }
                catch (Exception)
                {
                    portsInfo[i].Status = PortStatus.IN_USE;
                }
                finally
                {
                    serialPort.Close();
                }

            }

            return portsInfo;
        }
    }
}
