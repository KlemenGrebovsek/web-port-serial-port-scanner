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
        /// <param name="sSettings">Object containing all scan settings.</param>
        /// <param name="cToken">CancellationToken object.</param>
        /// <returns>A collection of type serial port status in range of [min-max].</returns>
        public async Task<IEnumerable<IPrintablePortStatus>> ScanAsync(IScanProperties sSettings, CancellationToken cToken)
        {
            var minPort = sSettings.GetMinPort();
            var maxPort = sSettings.GetMaxPort();
            
            if (maxPort < minPort)
                throw new ArgumentException("Max port value is less than min port value.");
            
            if (minPort < 0)
                throw new ArgumentOutOfRangeException(nameof(minPort));
            
            if (maxPort > 65535)
                throw new ArgumentOutOfRangeException(nameof(maxPort));

            var serialPorts = SerialPort.GetPortNames()
                                                  .Where(x => int.Parse(x.Substring(3)) >= minPort && 
                                                                    int.Parse(x.Substring(3)) <= maxPort).ToList();

            if (serialPorts.Count < 1)
                throw new Exception("No serial port found.");

            var sResult = await Task.Run(() => GetPortStatus(serialPorts), cToken);

            var status = sSettings.GetSearchStatus();
            var targetStatus = status.ToString();

            return status != PortStatus.Any ? sResult.Where(x => x.GetStatusString() == targetStatus 
                                                                 || x.GetStatusString() == "Unknown") : sResult;
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
                    serialPortStatus = new SerialPortStatus(portName, PortStatus.Free);
                }
                catch (UnauthorizedAccessException)
                {
                    serialPortStatus = new SerialPortStatus(portName, PortStatus.In_use);
                }
                catch (Exception)
                {
                    serialPortStatus = new SerialPortStatus(portName, PortStatus.Unknown);
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

