using System;
using System.Threading.Tasks;
using webport_comport_scanner.Options;
using System.IO.Ports;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Scanners
{
    public class ComScanner : IScanner
    {
        public ComScanner()
        {
            Console.WriteLine("Colletings COM port info.");
        }
        public void Scan(ProgramOptions options)
        {
            Task<ComPortInfo[]> info = Task<ComPortInfo[]>.Factory.StartNew(() => GetComPortsInfo());
            PrintR(info.Result);
        }

        private ComPortInfo[] GetComPortsInfo()
        {
            string[] comPorts = SerialPort.GetPortNames();
            ComPortInfo[] portsInfo = new ComPortInfo[comPorts.Length];

            if(portsInfo.Length < 1)
            {
                Console.WriteLine("No com ports found.");
                return new ComPortInfo[] { };
            }

            Array.Sort(comPorts);
            SerialPort serialPort;

            for (int i = 0; i < comPorts.Length; i++)
            {
                serialPort = new SerialPort(comPorts[i]);
                portsInfo[i] = new ComPortInfo { Name = comPorts[i] };
                try
                {
                    serialPort.Open();
                    portsInfo[i].Status = "Available";
                }
                catch (Exception)
                {
                    portsInfo[i].Status = "In use";
                }
                finally
                {
                    serialPort.Close();
                }
            }

            return portsInfo;
        }
        
        private void PrintR(ComPortInfo[] info)
        {
            if (info.Length < 1)
                return;

            Console.WriteLine("\nScan results:");

            for (int i = 0; i < info.Length; i++)
            {
                Console.WriteLine($"PORT: {info[i].Name}  STATUS: {info[i].Status}");
            }
        }
    }
}
