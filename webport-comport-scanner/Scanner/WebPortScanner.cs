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
        /// <exception cref="Exception">If scan of ports can't be started or any other reason.</exception>
        /// <param name="minPort">Scan from this port (including).</param>
        /// <param name="maxPort">Scan to this port (including).</param>
        /// <param name="cToken">CancellationToken object.</param>
        /// <returns>A collection of type web port statuses.</returns>
        public async Task<IEnumerable<IPrintablePortStatus>> ScanAsync(int minPort, int maxPort, CancellationToken cToken)
        {
            var iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            
            if (iPHostEntry.AddressList.Length < 1)
                throw new Exception("Web port scan can't be started.");

            var host = iPHostEntry.AddressList[0];
            
            var taskList = new List<Task<IPrintablePortStatus>>((maxPort - minPort) + 1);

            for (var i = minPort; i < maxPort + 1; i++)
            {
                cToken.ThrowIfCancellationRequested();
                taskList.Add(Task.FromResult(GetPortStatus(host, i)));
            }
            
            return await Task.Run(() => Task.WhenAll(taskList), cToken);
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
            PortStatusData portStatusData;

            try
            {
                tcpListener = new TcpListener(address, port);
                tcpListener.Start();
                portStatusData = new PortStatusData(port.ToString(), PortStatus.Free.ToString());
            }
            catch (SocketException)
            {
                portStatusData = new PortStatusData(port.ToString(), PortStatus.InUse.ToString());
            }
            catch (Exception)
            {
                portStatusData = new PortStatusData(port.ToString(), PortStatus.Unknown.ToString());
            }
            finally
            {
                tcpListener?.Stop();
            }

            return portStatusData;   
        }
    }
}
