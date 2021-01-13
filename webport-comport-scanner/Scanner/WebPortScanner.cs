using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
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
        /// Scan for web ports and their status.
        /// </summary>
        /// <exception cref="ArgumentException">If min and max port are logically wrong.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If min or max value is outside the port range. </exception>
        /// <exception cref="Exception">If scan of ports can't be started or any other reason.</exception>
        /// <param name="minPort">Scan from this port (including).</param>
        /// <param name="maxPort">Scan to this port (including).</param>
        /// <param name="cToken">CancellationToken object.</param>
        /// <returns>A collection of type web port statuses.</returns>
        public IEnumerable<Task<IPrintablePortStatus>> Scan(int minPort, int maxPort, CancellationToken cToken)
        {
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
            
            var totalTasks = (maxPort - minPort) + 1;
            var taskList = new List<Task<IPrintablePortStatus>>(totalTasks);

            for (var i = minPort; i < maxPort + 1; i++)
            {
                cToken.ThrowIfCancellationRequested();
                taskList.Add(Task.FromResult(GetPortStatus(host, i)));
            }
            
            return taskList;
        }

        /// <summary>
        /// Check status of specific port.
        /// </summary>
        /// <param name="address">Host ip address.</param>
        /// <param name="port">Port number.</param>
        /// <returns>Port status.</returns>
        private static IPrintablePortStatus GetPortStatus(IPAddress address, int port)
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
