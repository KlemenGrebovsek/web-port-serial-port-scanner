using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using webport_comport_scanner.Options;
using static webport_comport_scanner.Models.Models;

namespace webport_comport_scanner.Scanners
{
    public class WebScanner : IScanner
    {
        public void Scan(ProgramOptions options)
        {
            Task<WebPortInfo[]> task = CheckPorts(options);
            WebPortInfo[] webPortInfos = task.Result;
            PrintR(ref webPortInfos);
        }

        private async Task<WebPortInfo[]> CheckPorts(ProgramOptions options)
        {
            List<Task<WebPortInfo>> portsToCheck = new List<Task<WebPortInfo>>();
             
            for (int currentPort = options.MinPort; currentPort <= options.MaxPort; currentPort++)
            {
                Task<WebPortInfo> job = CheckPort(currentPort);
                portsToCheck.Add(job);
            }

            return await Task.WhenAll(portsToCheck);
        }

        private Task<WebPortInfo> CheckPort(int port)
        {
            return Task<WebPortInfo>.Factory.StartNew(() =>
            {
                TcpListener tcpListener = default;

                try
                {
                    tcpListener = new TcpListener(Dns.GetHostEntry("localhost").AddressList[0], port);
                    tcpListener.Start();
                    tcpListener.Stop();
                    return new WebPortInfo { Status = PortStatus.FREE };

                }
                catch (SocketException)
                {
                    return new WebPortInfo { Port = port, Status = PortStatus.IN_USE };
                }
                catch (Exception)
                {
                    return new WebPortInfo { Port = port, Status = PortStatus.UNKNOWN };
                }
                finally
                {
                  tcpListener.Stop();
                }
            });
        }

        private void PrintR(ref WebPortInfo[] webPortInfos)
        {
            for (int i = 0; i < webPortInfos.Length; i++)
            {
                Console.WriteLine($"Port: {webPortInfos[i].Port} , Status: {webPortInfos[i].Status.ToString()}");
            }
        }

    }
}
