using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using webport_comport_scanner.Scanners;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Test
{
    public class WebPortScannerTest
    {
        [Fact]
        public void Test_MinPortLimit()
        {
            IPortScanner webPortScanner = new WebPortScanner();
            Assert.Throws<ArgumentOutOfRangeException>(() => webPortScanner.Scan(-1, 100, PortStatus.ANY));
        }

        [Fact]
        public void Test_MaxPortLimit()
        {
            IPortScanner webPortScanner = new WebPortScanner();
            Assert.Throws<ArgumentOutOfRangeException>(() => webPortScanner.Scan(10, 65536, PortStatus.ANY));
        }

        [Fact]
        public void Test_InvalidPortScanRange()
        {
            IPortScanner webPortScanner = new WebPortScanner();
            Assert.Throws<ArgumentException>(() => webPortScanner.Scan(30, 15, PortStatus.ANY));
        }

        [Fact]
        public void Test_ValidPortScanRange()
        {
            IPortScanner webPortScanner = new WebPortScanner();
            
            try
            {
                webPortScanner.Scan(15, 30, PortStatus.ANY);
                Assert.True(true);
            }
            catch (Exception e)
            {
               Assert.True(false, e.Message);
            }
        }

        [Theory]
        [InlineData(PortStatus.FREE)]
        [InlineData(PortStatus.IN_USE)]
        [InlineData(PortStatus.UNKNOWN)]
        public void Test_ValidPortStatus(PortStatus status)
        {
            IPortScanner WebPortScanner = new WebPortScanner();
            IEnumerable<IPrintablePortStatus> scanResult = default;

            try
            {
                scanResult = WebPortScanner.Scan(0, 65535, status);
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
            
            string portStatusString = status.ToString();
            Assert.True(scanResult.All(x => x.GetStatus() == portStatusString));
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(100, 200)]
        [InlineData(150, 500)]
        public void Test_PortScanRangeEquals(int minPort, int maxPort)
        {
            IPortScanner WebPortScanner = new WebPortScanner();
            IEnumerable<IPrintablePortStatus> scanResult = default;

            try
            {
                scanResult = WebPortScanner.Scan(minPort, maxPort, PortStatus.ANY);
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
            
            Assert.Equal(maxPort - minPort, scanResult.Count());
        }
    }
}