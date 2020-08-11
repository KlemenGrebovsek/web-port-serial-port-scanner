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
        public IEnumerable<IPrintablePortResult> Scan(ProgramOptions options)
        {
            return CheckPortsStatus(options).Result.Where(x => x.GetStatusRaw() != PortStatus.FREE);
        }
        private async Task<IEnumerable<WebPortInfo>> CheckPortsStatus(ProgramOptions options)
        {
            ICollection<Task<WebPortInfo>> portCheckTasks = new List<Task<WebPortInfo>>();

            for (int currentPort = options.MinPort; currentPort <= options.MaxPort; currentPort++)
                portCheckTasks.Add(CheckPort(currentPort));

            return await Task.WhenAll(portCheckTasks);
        }

        private Task<WebPortInfo> CheckPort(int port)
        {
            return Task.Run(() =>
            {
                TcpListener tcpListener = default;
                
                try
                {
                    tcpListener = new TcpListener(Dns.GetHostEntry(Dns.GetHostName()).AddressList[0], port);
                    tcpListener.Start();

                    return new WebPortInfo(port, PortStatus.FREE);
                }
                catch (SocketException)
                {
                    return new WebPortInfo(port, PortStatus.IN_USE);
                }
                catch (Exception)
                {
                    return new WebPortInfo(port, PortStatus.UNKNOWN);
                }
                finally
                {
                    tcpListener.Stop();
                }
            });
        }
    }
}
