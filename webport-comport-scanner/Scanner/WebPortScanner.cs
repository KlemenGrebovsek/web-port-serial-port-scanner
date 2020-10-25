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
    /// Provides functionality of scanning web ports.
    /// </summary>
    public class WebPortScanner : IPortScanner
    {
        /// <summary>
        /// Scans for web ports and their status.
        /// </summary>
        /// <exception cref="ArgumentException">If min and max port are logically wrong.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If min and max are outside the port range. </exception>
        /// <exception cref="Exception">If scan of ports can't be started or any other reason.</exception>
        /// <param name="minPort">Minimum port (including).</param>
        /// <param name="maxPort">Maximum port (including).</param>
        /// <param name="status">Filter ports by this status.</param>
        /// <returns>A collection of type web port status in range of [min-max].</returns>
        public IEnumerable<IPrintablePortStatus> Scan(int minPort, int maxPort, PortStatus status)
        {
            if (maxPort < minPort)
                throw new ArgumentException("Max port cannot be less than min port.");

            if (minPort < 0 || maxPort > 65535)
                throw new ArgumentOutOfRangeException("Min and max port values should be in range [0-65535].");

            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());

            if (iPHostEntry.AddressList.Length < 1)
                throw new Exception("Web port scan couldn't be started.");

            IEnumerable<WebPortStatus> scanResult = GetPortsStatus(iPHostEntry.AddressList[0], minPort, maxPort);

            if (status != PortStatus.ANY)
                return scanResult.Where(x => x.GetStatusEnum() == status);
                
            return scanResult;
        }

        /// <summary>
        /// Checks status of all ports async.
        /// </summary>
        /// <param name="address">IP address.</param>
        /// <param name="minPort">Minimum port (including).</param>
        /// <param name="maxPort">Maximum port (including).</param>
        /// <exception cref="AggregateException">If any task within method was canceled.</exception>
        /// <exception cref="ObjectDisposableException">If any task within method was disposed while being in use.</exception>
        /// <exception cref="Exception">If any task failed for unknown reason.</exception>
        /// <returns>A collection of web port status in range (min-max).</returns>
        private IEnumerable<WebPortStatus> GetPortsStatus(IPAddress address, int minPort, int maxPort)
        {
            List<Task<WebPortStatus>> scanTasks = new List<Task<WebPortStatus>>(maxPort - minPort);

            for (; minPort < maxPort; minPort++)
                scanTasks.Add(Task.FromResult(GetPortStatus(address, minPort)));

            Task<WebPortStatus[]> masterTask = Task.WhenAll(scanTasks);

            masterTask.Wait();

            if (masterTask.Status != TaskStatus.RanToCompletion)
                throw new Exception("Ran into problem while trying to scan web port status:" +
                                                $"{masterTask.Exception.InnerException.Message}");

            return masterTask.Result;
        }

        /// <summary>
        /// Checks status of given port.
        /// </summary>
        /// <param name="address">Host ip address.</param>
        /// <param name="port">Port number.</param>
        /// <returns>Port status.</returns>
        private WebPortStatus GetPortStatus(IPAddress address, int port)
        {
            TcpListener tcpListener = default;
            WebPortStatus portStatus;

            try
            {
                tcpListener = new TcpListener(address, port);
                tcpListener.Start();

                portStatus = new WebPortStatus(port, PortStatus.FREE);
            }
            catch (SocketException)
            {
                portStatus = new WebPortStatus(port, PortStatus.IN_USE);
            }
            catch (Exception)
            {
                portStatus = new WebPortStatus(port, PortStatus.UNKNOWN);
            }
            finally
            {
                if (tcpListener != default)
                    tcpListener.Stop();
            }

            return portStatus;   
        }
    }
}
