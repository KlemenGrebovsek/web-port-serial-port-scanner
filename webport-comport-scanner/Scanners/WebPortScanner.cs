using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Scanners
{
    public class WebPortScanner : IPortScanner
    {
        public async Task<IEnumerable<PortStatusData>> ScanAsync(ScanProperties properties, CancellationToken cToken)
        {
            var host = Dns.GetHostEntry(Dns.GetHostName())
                .AddressList[0];

            var taskList = new List<Task<PortStatusData>>((properties.MaxPort - properties.MinPort) + 1);

            for (var i = properties.MinPort; i < properties.MaxPort + 1; i++)
            {
                cToken.ThrowIfCancellationRequested();
                
                var portNumber = i;
                var t = new Task<PortStatusData>(() => GetPortStatus(host, portNumber), cToken);
                t.Start();
                
                taskList.Add(t);
            }
            
            return await Task.WhenAll(taskList);
        }
        
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
