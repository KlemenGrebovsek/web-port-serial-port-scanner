using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using webport_comport_scanner.Options;
using webport_comport_scanner.Models;
using webport_comport_scanner.Printer;

namespace webport_comport_scanner.Scanners
{
    public class WebScanner : IScanner
    {
        public void Scan(ProgramOptions options)
        {

            if (options.MaxPort < options.MinPort)
            {
                Console.WriteLine("Error: MaxPort value is less than MinPort value.");
                return;
            }

            ResultPrinter printer = new ResultPrinter();
            printer.PrintR(CheckPorts(options).Result, "PORT", "STATUS");
        }

        private async Task<WebPortInfo[]> CheckPorts(ProgramOptions options)
        {
            List<Task<WebPortInfo>> portsToCheck = new List<Task<WebPortInfo>>();
             
            for (int currentPort = options.MinPort; currentPort <= options.MaxPort; currentPort++)
            {
                Task<WebPortInfo> job = CheckPort(currentPort);

                if (job.Result.GetPortStatus() != PortStatus.FREE)
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
