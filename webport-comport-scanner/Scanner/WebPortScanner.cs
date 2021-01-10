using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using webport_comport_scanner.Model;

namespace webport_comport_scanner.Scanner
{
    /// <summary>
    /// Provides functionality of scanning web ports.
    /// </summary>
    public class WebPortScanner : IPortScanner
    {
        /// <summary>
        /// Scan for web ports and their status async.
        /// </summary>
        /// <exception cref="ArgumentException">If min and max port are logically wrong.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If min or max value is outside the port range. </exception>
        /// <exception cref="Exception">If scan of ports can't be started or any other reason.</exception>
        /// <param name="sSettings">Object containing all scan settings.</param>
        /// <param name="cToken">CancellationToken object.</param>
        /// <returns>A collection of type web port statuses.</returns>
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
            
            var iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            
            if (iPHostEntry.AddressList.Length < 1)
                throw new Exception("Web port scan can't be started.");

            var host = iPHostEntry.AddressList[0];

            var taskArray = Enumerable.Range(minPort, (maxPort - minPort) + 1)
                                                        .Select(x => Task.FromResult(GetPortStatus(host, x)))
                                                        .ToArray();
            
            var masterTask =  Task.Run(()=> Task.WhenAll(taskArray), cToken);
            
            var sResult = await masterTask;
            
            var status = sSettings.GetSearchStatus();
            var targetStatusStr = status.ToString();
            
            if (masterTask.Status != TaskStatus.RanToCompletion)
                throw new Exception("Failed to scan web ports.");
            
            return status != PortStatus.Any ? sResult.Where(x => 
                                                            x.GetStatusString() == targetStatusStr) : sResult;
        }

        /// <summary>
        /// Check status of specific port.
        /// </summary>
        /// <param name="address">Host ip address.</param>
        /// <param name="port">Port number.</param>
        /// <returns>Port status.</returns>
        private static WebPortStatus GetPortStatus(IPAddress address, int port)
        {
            TcpListener tcpListener = null;
            WebPortStatus portStatus;

            try
            {
                tcpListener = new TcpListener(address, port);
                tcpListener.Start();
                portStatus = new WebPortStatus(port, PortStatus.Free);
            }
            catch (SocketException)
            {
                portStatus = new WebPortStatus(port, PortStatus.InUse);
            }
            catch (Exception)
            {
                portStatus = new WebPortStatus(port, PortStatus.Unknown);
            }
            finally
            {
                tcpListener?.Stop();
            }

            return portStatus;   
        }
    }
}
