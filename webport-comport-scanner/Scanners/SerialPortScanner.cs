using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using webport_comport_scanner.Exceptions;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Scanners
{
    /// <summary>
    /// Provides functionality of scanning serial ports.
    /// </summary>
    public class SerialPortScanner : IPortScanner
    {
        /// <summary>
        /// Scans for ports and their status.
        /// </summary>
        /// <param name="properties">Scan properties</param>
        /// <param name="cToken">CancellationToken object.</param>
        /// <returns>A collection of port status.</returns>
        public async Task<IEnumerable<PortStatusData>> ScanAsync(ScanProperties properties, CancellationToken cToken)
        {
            var serialPorts = GetPortNames(properties).ToList();

            if (serialPorts.Count < 1)
                throw new SerialPortNotFoundException();
            
            var taskList = new List<Task<PortStatusData>>((properties.MaxPort - properties.MinPort) + 1);

            for (var i = properties.MinPort; i < properties.MaxPort + 1; i++)
            {
                cToken.ThrowIfCancellationRequested();
                taskList.Add(Task.FromResult(GetPortStatus(serialPorts[i])));
            }

            return await Task.Run(() => Task.WhenAll(taskList), cToken);
        }

        /// <summary>
        /// Gets serial port names in range.
        /// </summary>
        /// <param name="properties">Scan properties</param>
        /// <returns>Collection of port names in requested range.</returns>
        private static IEnumerable<string> GetPortNames(ScanProperties properties)
        {
            return SerialPort.GetPortNames()
                .Where(x => int.Parse(x.Substring(3)) >= properties.MinPort 
                            && int.Parse(x.Substring(3)) <= properties.MaxPort);
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

