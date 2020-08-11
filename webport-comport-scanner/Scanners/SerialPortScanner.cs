using System;
using webport_comport_scanner.Options;
using System.IO.Ports;
using webport_comport_scanner.Models;
using System.Collections.Generic;

namespace webport_comport_scanner.Scanners
{
    public class SerialPortScanner : IPortScanner
    {
        public IEnumerable<IPrintableScanResult> Scan(ProgramOptions options)
        {
            string[] comPorts = SerialPort.GetPortNames();

            if (comPorts.Length < 1)
                return new List<SerialPortInfo>(0);

            ICollection<SerialPortInfo> comPortInfos = new List<SerialPortInfo>(comPorts.Length);

            SerialPort serialPort;

            for (int i = 0; i < comPorts.Length; i++)
            {
                serialPort = new SerialPort(comPorts[i]);

                try
                {
                    serialPort.Open();
                    comPortInfos.Add(new SerialPortInfo(comPorts[i], PortStatus.FREE));
                }
                catch (UnauthorizedAccessException)
                {
                    comPortInfos.Add(new SerialPortInfo(comPorts[i], PortStatus.IN_USE));
                }
                catch (Exception)
                {
                    comPortInfos.Add(new SerialPortInfo(comPorts[i], PortStatus.UNKNOWN));
                }
                finally
                {
                    if (serialPort.IsOpen)
                        serialPort.Close();
                }

            }

            return comPortInfos;
        }
    }
}
