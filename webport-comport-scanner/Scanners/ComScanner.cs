using System;
using webport_comport_scanner.Options;
using System.IO.Ports;
using webport_comport_scanner.Models;
using System.Collections.Generic;

namespace webport_comport_scanner.Scanners
{
    public class ComScanner : IScanner
    {
        public IEnumerable<IPrintable> Scan(ProgramOptions options)
        {
            string[] comPorts = SerialPort.GetPortNames();

            if (comPorts.Length < 1) throw new Exception("No com ports found.");

            IList<ComPortInfo> portsInfo = new List<ComPortInfo>(comPorts.Length);

            SerialPort serialPort;

            for (int i = 0; i < comPorts.Length; i++)
            {
                serialPort = new SerialPort(comPorts[i]);

                try
                {
                    serialPort.Open();
                    portsInfo.Add(new ComPortInfo(comPorts[i], PortStatus.FREE));
                }
                catch (Exception)
                {
                    portsInfo.Add(new ComPortInfo(comPorts[i], PortStatus.IN_USE));
                }
                finally
                {
                    if (serialPort.IsOpen) serialPort.Close();
                }

            }

            return portsInfo;
        }
    }
}
