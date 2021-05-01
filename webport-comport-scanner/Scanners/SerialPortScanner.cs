using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Scanners
{
    public class SerialPortScanner : IPortScanner
    {
        public async Task<IEnumerable<PortStatusData>> ScanAsync(ScanProperties properties, CancellationToken cToken)
        {
            var serialPorts = GetPortNames(properties).ToList();

            if (serialPorts.Count < 1)
                return ArraySegment<PortStatusData>.Empty;
            
            var taskList = new List<Task<PortStatusData>>((properties.MaxPort - properties.MinPort) + 1);

            for (var i = properties.MinPort; i < properties.MaxPort + 1; i++)
            {
                var index = i;
                var t = new Task<PortStatusData>(() => GetPortStatus(serialPorts[index]), cToken);
                t.Start();
                
                taskList.Add(t);
            }
            
            return await Task.WhenAll(taskList);
        }
        
        private static IEnumerable<string> GetPortNames(ScanProperties properties)
        {
            return SerialPort.GetPortNames()
                .Where(x => int.Parse(x.Substring(3)) >= properties.MinPort 
                            && int.Parse(x.Substring(3)) <= properties.MaxPort);
        }

        private static PortStatusData GetPortStatus(string portName)
        {
            PortStatusData serialPortStatus;

            try
            {
                using var serialPort  = new SerialPort(portName);
                serialPort.Open();
                serialPortStatus = new PortStatusData(portName, PortStatus.Free);
            }
            catch (UnauthorizedAccessException)
            {
                serialPortStatus = new PortStatusData(portName, PortStatus.InUse);
            }
            catch (Exception)
            {
                serialPortStatus = new PortStatusData(portName, PortStatus.Unknown);
            }

            return serialPortStatus;
        }
    }
}