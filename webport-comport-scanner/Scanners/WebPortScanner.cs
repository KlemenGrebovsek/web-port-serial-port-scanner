using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using webport_comport_scanner.Exceptions;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Scanners
{
    /// <summary>
    /// Provides functionality of scanning web ports.
    /// </summary>
    public class WebPortScanner : IPortScanner
    {
        /// <summary>
        /// Scans for ports and their status.
        /// </summary>
        /// <param name="properties">Scan properties</param>
        /// <param name="cToken">CancellationToken object.</param>
        /// <returns>A collection of port status.</returns>
        public async Task<IEnumerable<PortStatusData>> ScanAsync(ScanProperties properties, CancellationToken cToken)
        {
            var host = GetHost();
            
            if (host == null)
                throw new DefaultHostException();
            
            var taskList = new List<Task<PortStatusData>>((properties.MaxPort - properties.MinPort) + 1);

            for (var i = properties.MinPort; i < properties.MaxPort + 1; i++)
            {
                cToken.ThrowIfCancellationRequested();
                taskList.Add(Task.FromResult(GetPortStatus(host, i)));
            }
            
            return await Task.Run(() => Task.WhenAll(taskList), cToken);
        }

        /// <summary>
        /// Gets default host.
        /// </summary>
        /// <returns>Ip address of host.</returns>
        private static IPAddress GetHost()
        {
            return Dns.GetHostEntry(Dns.GetHostName())
                .AddressList[0];
        }

        /// <summary>
        /// Scans status of web port.
        /// </summary>
        /// <param name="address">Host ip address.</param>
        /// <param name="port">Port number.</param>
        /// <returns>Web port status.</returns>
        private static PortStatusData GetPortStatus(IPAddress address, int port)
        {
            TcpListener tcpListener = null;
            PortStatusData portStatusData;

            try
            {
                tcpListener = new TcpListener(address, port);
                tcpListener.Start();
                portStatusData = new PortStatusData(port.ToString(), PortStatus.Free);
            }
            catch (SocketException)
            {
                portStatusData = new PortStatusData(port.ToString(), PortStatus.InUse);
            }
            catch (Exception)
            {
                portStatusData = new PortStatusData(port.ToString(), PortStatus.Unknown);
            }
            finally
            {
                tcpListener?.Stop();
            }

            return portStatusData;   
        }
    }
}
