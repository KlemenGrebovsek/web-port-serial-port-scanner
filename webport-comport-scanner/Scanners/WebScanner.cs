using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using webport_comport_scanner.Options;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Scanners
{
    public class WebScanner : IScanner
    {
        public IEnumerable<IPrintable> Scan(ProgramOptions options)
        {
            if (options.MaxPort < options.MinPort) throw new Exception("MaxPort value is less than MinPort value.");

            return CheckPortsStatus(options).Result;
        }
        private async Task<IList<WebPortInfo>> CheckPortsStatus(ProgramOptions options)
        {
            IList<Task<WebPortInfo>> portCheckTasks = new List<Task<WebPortInfo>>();

            for (int currentPort = options.MinPort; currentPort <= options.MaxPort; currentPort++)
            {
                Task<WebPortInfo> job = CheckPort(currentPort);
          
                if (job.Result.GetPortStatus() != PortStatus.FREE)
                    portCheckTasks.Add(job);
            }

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
                    tcpListener.Stop();

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
