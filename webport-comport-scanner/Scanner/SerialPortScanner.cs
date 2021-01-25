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
        /// Scans for serial ports and their status.
        /// </summary>
        /// <exception cref="ArgumentException">If min and max port are logically wrong.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If min or max value is outside the port range. </exception>
        /// <exception cref="Exception">If scan of ports can't be started or any other reason.</exception>
        /// <param name="minPort">Scan from this port (including).</param>
        /// <param name="maxPort">Scan to this port (including).</param>
        /// <param name="cToken">CancellationToken object.</param>
        /// <returns>A collection of type serial port statuses.</returns>
        public async Task<IEnumerable<IPrintablePortStatus>> ScanAsync(int minPort, int maxPort, CancellationToken cToken)
        {
            if (maxPort < minPort)
                throw new ArgumentException("Max port value is less than min port value.");
            
            if (minPort < 0)
                throw new ArgumentOutOfRangeException(nameof(minPort));
            
            if (maxPort > 65535)
                throw new ArgumentOutOfRangeException(nameof(maxPort));

            
            var serialPorts = SerialPort.GetPortNames()
                .Where(x => int.Parse(x.Substring(3)) >= minPort && int.Parse(x.Substring(3)) <= maxPort).ToList();

            if (serialPorts.Count < 1)
                throw new Exception("No serial port found.");
            
            var taskList = new List<Task<IPrintablePortStatus>>((maxPort - minPort) + 1);

            for (var i = minPort; i < maxPort + 1; i++)
            {
                cToken.ThrowIfCancellationRequested();
                taskList.Add(Task.FromResult(GetPortStatus(serialPorts[i])));
            }

            return await Task.Run(() => Task.WhenAll(taskList), cToken);
        }

        /// <summary>
        /// Check status of serial ports.
        /// </summary>
        /// <returns>Serial port status.</returns>
        private static IPrintablePortStatus GetPortStatus(string portName)
        {
            SerialPortStatus serialPortStatus;

            try
            {
                using var serialPort  = new SerialPort(portName);
                serialPort.Open();
                serialPortStatus = new SerialPortStatus(portName, PortStatus.Free);
            }
            catch (UnauthorizedAccessException)
            {
                serialPortStatus = new SerialPortStatus(portName, PortStatus.InUse);
            }
            catch (Exception)
            {
                serialPortStatus = new SerialPortStatus(portName, PortStatus.Unknown);
            }

            return serialPortStatus;
        }
    }
}

