using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using webport_comport_scanner.Architecture;
using webport_comport_scanner.Model;
using webport_comport_scanner.Scanner;

namespace webport_comport_scanner.Test
{
    public class WebPortScannerTest
    {
        [Fact]
        public void Test_MinPortLimit()
        {
            IPortScanner wpScanner = new WebPortScanner();
            
            Assert.Throws<ArgumentOutOfRangeException>(() => wpScanner.Scan(-1, 100, PortStatus.Any));
        }

        [Fact]
        public void Test_MaxPortLimit()
        {
            IPortScanner wpScanner = new WebPortScanner();
            Assert.Throws<ArgumentOutOfRangeException>(() => wpScanner.Scan(10, 65536, PortStatus.Any));
        }

        [Fact]
        public void Test_InvalidPortScanRange()
        {
            IPortScanner wpScanner = new WebPortScanner();
            Assert.Throws<ArgumentException>(() => wpScanner.Scan(30, 15, PortStatus.Any));
        }

        [Fact]
        public void Test_ValidPortScanRange()
        {
            IPortScanner wpScanner = new WebPortScanner();
            
            try
            {
                wpScanner.Scan(15, 30, PortStatus.Any);
            }
            catch (Exception e)
            {
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
            IPortScanner wpScanner = new WebPortScanner();
            IEnumerable<IPrintablePortStatus> sResult = default;

            try
            {
                sResult = wpScanner.Scan(0, 3000, status);
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
            
            var portStatusString = status.ToString();
            Assert.True(sResult.All(x => x.GetStatusString() == portStatusString));
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(100, 200)]
        [InlineData(150, 500)]
        public void Test_PortScanRangeEquals(int minPort, int maxPort)
        {
            IPortScanner wpScanner = new WebPortScanner();
            IEnumerable<IPrintablePortStatus> sResult = default;

            try
            {
                sResult = wpScanner.Scan(minPort, maxPort, PortStatus.Any);
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
            
            Assert.Equal((maxPort - minPort) + 1, sResult.Count());
        }
    }
}