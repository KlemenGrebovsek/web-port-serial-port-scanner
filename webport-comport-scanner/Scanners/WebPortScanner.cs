using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using webport_comport_scanner.Models;
using System.Linq;

namespace webport_comport_scanner.Scanners
{
    /// <summary>
    /// Scans for web ports and their status. 
    /// Returns only ports that are in use or in unknown state.
    /// </summary>
    public class WebPortScanner : IPortScanner
    {
        public IEnumerable<IPrintableScanResult> Scan(int minPort, int maxPort)
        {
            if (maxPort < minPort)
                return Enumerable.Empty<IPrintableScanResult>();

            return CheckPortsStatus(minPort, maxPort)
                .Result
                .Where(x => x.GetStatusEnum() != PortStatus.FREE);
        }

        private async Task<IEnumerable<WebPortStatus>> CheckPortsStatus(int minPort, int maxPort)
        {
            List<Task<WebPortStatus>> checkPortStatusTaskCollection = new List<Task<WebPortStatus>>(maxPort - minPort);

            for (int currPort = minPort; currPort <= maxPort; currPort++)
                checkPortStatusTaskCollection.Add(CheckPort(currPort));

            return await Task.WhenAll(checkPortStatusTaskCollection);
        }

        private Task<WebPortStatus> CheckPort(int port)
        {
            return Task.Run(() =>
            {
                TcpListener tcpListener = default;
                
                try
                {
                    tcpListener = new TcpListener(Dns.GetHostEntry(Dns.GetHostName()).AddressList[0], port);
                    tcpListener.Start();

                    return new WebPortStatus(port, PortStatus.FREE);
                }
                catch (SocketException)
                {
                    return new WebPortStatus(port, PortStatus.IN_USE);
                }
                catch (Exception)
                {
                    return new WebPortStatus(port, PortStatus.UNKNOWN);
                }
                finally
                {
                    tcpListener.Stop();
                }
            });
        }
    }
}
