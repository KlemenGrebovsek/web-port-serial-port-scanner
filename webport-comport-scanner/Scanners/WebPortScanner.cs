using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using webport_comport_scanner.Options;
using webport_comport_scanner.Models;
using System.Linq;

namespace webport_comport_scanner.Scanners
{
    public class WebPortScanner : IPortScanner
    {
        public IEnumerable<IPrintableScanResult> Scan(ProgramOptions options)
        {
            if (options.MaxPort < options.MinPort)
                return Enumerable.Empty<IPrintableScanResult>();

            return CheckPortsStatus(options)
                .Result
                .Where(x => x.GetStatusEnum() != PortStatus.FREE);
        }
        private async Task<IEnumerable<WebPortStatus>> CheckPortsStatus(ProgramOptions options)
        {
            List<Task<WebPortStatus>> checkPortStatusTaskCollection = new List<Task<WebPortStatus>>(options.MaxPort - options.MinPort);

            for (int currPort = options.MinPort; currPort <= options.MaxPort; currPort++)
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
