using System;
using webport_comport_scanner.Options;
using System.IO.Ports;
using webport_comport_scanner.Models;
using System.Collections.Generic;
using System.Linq;

namespace webport_comport_scanner.Scanners
{
    /// <summary>
    /// Scans for serial ports and their status.
    /// </summary>
    public class SerialPortScanner : IPortScanner
    {
        public IEnumerable<IPrintableScanResult> Scan(int minPort, int maxPort)
        {
            string[] seriaPortNames = SerialPort.GetPortNames();

            if (seriaPortNames.Length < 1)
                return Enumerable.Empty<SerialPortStatus>();

            List<SerialPortStatus> serialPortStatusCollection = new List<SerialPortStatus>(seriaPortNames.Length);

            SerialPort serialPort;

            for (int i = 0; i < seriaPortNames.Length; i++)
            {
                serialPort = new SerialPort(seriaPortNames[i]);

                try
                {
                    serialPort.Open();
                    serialPortStatusCollection.Add(new SerialPortStatus(seriaPortNames[i], PortStatus.FREE));
                }
                catch (UnauthorizedAccessException)
                {
                    serialPortStatusCollection.Add(new SerialPortStatus(seriaPortNames[i], PortStatus.IN_USE));
                }
                catch (Exception)
                {
                    serialPortStatusCollection.Add(new SerialPortStatus(seriaPortNames[i], PortStatus.UNKNOWN));
                }
                finally
                {
                    if (serialPort.IsOpen)
                        serialPort.Close();
                }

            }

            return serialPortStatusCollection;
        }
    }
}
