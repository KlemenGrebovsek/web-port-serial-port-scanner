using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using webport_comport_scanner.Architecture;
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
        /// <exception cref="ArgumentOutOfRangeException">If min and max are outside the port range. </exception>
        /// <exception cref="Exception">If scan of ports can't be started or any other reason.</exception>
        /// <param name="minPort">Minimum port (including).</param>
        /// <param name="maxPort">Maximum port (including).</param>
        /// <param name="status">Filter ports by this status.</param>
        /// <returns>A collection of type serial port status in range of [min-max].</returns>
        public IEnumerable<IPrintablePortStatus> Scan(int minPort, int maxPort, PortStatus status)
        {
            if (maxPort < minPort)
                throw new ArgumentException("Max port cannot be less than min port.");

            // Physically there probably won't be more than 265 serial ports at max, but anyway.
            if (minPort < 0 || maxPort > 65535)
                throw new ArgumentOutOfRangeException($"Min and max port ranges should be in range [0-65535].");

            var serialPorts = SerialPort.GetPortNames()
                                                        .Where(x => int.Parse(x.Substring(3)) >= minPort && 
                                                                    int.Parse(x.Substring(3)) <= maxPort).ToList();

            if (serialPorts == null || serialPorts.Count < 1)
                throw new Exception("No serial ports found.");

            var sResult = GetPortStatus(serialPorts);

            return status != PortStatus.ANY ? sResult.Where(x => x.GetStatusEnum() == status) : sResult;
        }

        /// <summary>
        /// Checks status of serial ports.
        /// </summary>
        /// <returns>A collection of serial port status.</returns>
        private static IEnumerable<SerialPortStatus> GetPortStatus(IEnumerable<string> serialPorts)
        {
            SerialPort serialPort = null;
            SerialPortStatus serialPortStatus = null;

            foreach(var portName in serialPorts)
            {
                try
                {
                    serialPort = new SerialPort(portName);
                    serialPort.Open();
                    serialPortStatus = new SerialPortStatus(portName, PortStatus.FREE);
                }
                catch (UnauthorizedAccessException)
                {
                    serialPortStatus = new SerialPortStatus(portName, PortStatus.IN_USE);
                }
                catch (Exception)
                {
                    serialPortStatus = new SerialPortStatus(portName, PortStatus.UNKNOWN);
                }
                finally
                {
                    if (serialPort != null && serialPort.IsOpen)
                        serialPort.Close();
                }
            }

            yield return serialPortStatus;
        }
    }
}

