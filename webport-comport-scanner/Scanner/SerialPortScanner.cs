using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using webport_comport_scanner.Model;

namespace webport_comport_scanner.Scanner
{
    /// <summary>
    /// Provides functionality of scanning serial ports.
    /// </summary>
    public class SerialPortScanner : IPortScanner
    {
        /// <summary>
        /// Scans for ports and their status.
        /// </summary>
        /// <param name="minPort">Scan from this port (including).</param>
        /// <param name="maxPort">Scan to this port (including).</param>
        /// <param name="cToken">CancellationToken object.</param>
        /// <returns>A collection of port status.</returns>
        public async Task<IEnumerable<PortStatusData>> ScanAsync(int minPort, int maxPort, CancellationToken cToken)
        {
            var serialPorts = SerialPort.GetPortNames()
                .Where(x => int.Parse(x.Substring(3)) >= minPort && int.Parse(x.Substring(3)) <= maxPort).ToList();

            if (serialPorts.Count < 1)
                throw new Exception("No serial port found.");
            
            var taskList = new List<Task<PortStatusData>>((maxPort - minPort) + 1);

            for (var i = minPort; i < maxPort + 1; i++)
            {
                cToken.ThrowIfCancellationRequested();
                taskList.Add(Task.FromResult(GetPortStatus(serialPorts[i])));
            }

            return await Task.Run(() => Task.WhenAll(taskList), cToken);
        }

        /// <summary>
        /// Scans status of serial port.
        /// </summary>
        /// <returns>Serial port status.</returns>
        private static PortStatusData GetPortStatus(string portName)
        {
            PortStatusData serialPortStatus;

            try
            {
                using var serialPort  = new SerialPort(portName);
                serialPort.Open();
                serialPortStatus = new PortStatusData(portName, PortStatus.Free);
            }
            catch (UnauthorizedAccessException)
            {
                serialPortStatus = new PortStatusData(portName, PortStatus.InUse);
            }
            catch (Exception)
            {
                serialPortStatus = new PortStatusData(portName, PortStatus.Unknown);
            }

            return serialPortStatus;
        }
    }
}

