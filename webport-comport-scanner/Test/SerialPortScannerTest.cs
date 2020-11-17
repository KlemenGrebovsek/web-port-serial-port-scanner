using System;
using System.IO.Ports;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using webport_comport_scanner.Architecture;
using webport_comport_scanner.Model;
using webport_comport_scanner.Scanner;

namespace webport_comport_scanner.Test
{
    public class SerialPortScannerTest
    {
        private readonly bool _anyAvailablePort;

        public SerialPortScannerTest()
        {
            _anyAvailablePort = SerialPort.GetPortNames().Any();
        }

        [Fact]
        public void Test_MinPortLimit()
        {
            IPortScanner spScanner = new SerialPortScanner();
            Assert.Throws<ArgumentOutOfRangeException>(() => spScanner.Scan(-1, 100, PortStatus.Any));
        }

        [Fact]
        public void Test_MaxPortLimit()
        {
            IPortScanner spScanner = new SerialPortScanner();
            Assert.Throws<ArgumentOutOfRangeException>(() => spScanner.Scan(10, 65536, PortStatus.Any));
        }

        [Fact]
        public void Test_InvalidPortScanRange()
        {
            IPortScanner spScanner = new SerialPortScanner();
            Assert.Throws<ArgumentException>(() => spScanner.Scan(30, 15, PortStatus.Any));
        }

        [Fact]
        public void Test_ValidPortScanRange()
        {
            IPortScanner spScanner = new SerialPortScanner();
            
            try
            {
                spScanner.Scan(15, 30, PortStatus.Any);
            }
            catch (Exception e)
            {
                if (!_anyAvailablePort)
                    return;

                Assert.True(false, e.Message);
            }
            
            Assert.True(true);
        }

        [Theory]
        [InlineData(PortStatus.Free)]
        [InlineData(PortStatus.In_use)]
        [InlineData(PortStatus.Unknown)]
        public void Test_ValidPortStatus(PortStatus status)
        {
            IPortScanner spScanner = new SerialPortScanner();
            IEnumerable<IPrintablePortStatus> sResult = default;

            try
            {
                sResult = spScanner.Scan(0, 3000, status);
            }
            catch (Exception e)
            {
                if (!_anyAvailablePort)
                    return;

                Assert.True(false, e.Message);
            }
            
            var portStatusString = status.ToString();
            Assert.True(sResult.All(x => x.GetStatusString() == portStatusString));
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(2, 4)]
        [InlineData(1, 3)]
        public void Test_PortScanRangeEquals(int minPort, int maxPort)
        {
            IPortScanner spScanner = new SerialPortScanner();
            IEnumerable<IPrintablePortStatus> sResult = default;

            try
            {
                sResult = spScanner.Scan(minPort, maxPort, PortStatus.Any);
            }
            catch (Exception e)
            {
               if (!_anyAvailablePort)
                    return;

               Assert.True(false, e.Message);
            }
            
            Assert.Equal((maxPort - minPort) + 1, sResult.Count());
        }
    }
}