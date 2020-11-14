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
            Assert.Throws<ArgumentOutOfRangeException>(() => spScanner.Scan(-1, 100, PortStatus.ANY));
        }

        [Fact]
        public void Test_MaxPortLimit()
        {
            IPortScanner spScanner = new SerialPortScanner();
            Assert.Throws<ArgumentOutOfRangeException>(() => spScanner.Scan(10, 65536, PortStatus.ANY));
        }

        [Fact]
        public void Test_InvalidPortScanRange()
        {
            IPortScanner spScanner = new SerialPortScanner();
            Assert.Throws<ArgumentException>(() => spScanner.Scan(30, 15, PortStatus.ANY));
        }

        [Fact]
        public void Test_ValidPortScanRange()
        {
            IPortScanner spScanner = new SerialPortScanner();
            
            try
            {
                spScanner.Scan(15, 30, PortStatus.ANY);
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
        [InlineData(PortStatus.FREE)]
        [InlineData(PortStatus.IN_USE)]
        [InlineData(PortStatus.UNKNOWN)]
        public void Test_ValidPortStatus(PortStatus status)
        {
            IPortScanner spScanner = new SerialPortScanner();
            IEnumerable<IPrintablePortStatus> sResult = default;

            try
            {
                sResult = spScanner.Scan(0, 65535, status);
            }
            catch (Exception e)
            {
                if (!_anyAvailablePort)
                    return;

                Assert.True(false, e.Message);
            }
            
            var portStatusString = status.ToString();
            Assert.True(sResult.All(x => x.GetStatus() == portStatusString));
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(100, 200)]
        [InlineData(150, 500)]
        public void Test_PortScanRangeEquals(int minPort, int maxPort)
        {
            IPortScanner spScanner = new SerialPortScanner();
            IEnumerable<IPrintablePortStatus> sResult = default;

            try
            {
                sResult = spScanner.Scan(minPort, maxPort, PortStatus.ANY);
            }
            catch (Exception e)
            {
               if (!_anyAvailablePort)
                    return;

               Assert.True(false, e.Message);
            }
            
            Assert.Equal(maxPort - minPort, sResult.Count());
        }
    }
}