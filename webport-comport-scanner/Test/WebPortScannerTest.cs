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
        public void TestMinPortLimit()
        {
            WebPortScanner WebPortScanner = new WebPortScanner();
            bool exceptionThrown = false;

            try
            {
                WebPortScanner.Scan(-1, 100, PortStatus.ANY);
            }
            catch (Exception){
                exceptionThrown = true;
            }
            
            Assert.True(exceptionThrown);
        }


        [Fact]
        public void TestMaxPortLimit()
        {
            WebPortScanner WebPortScanner = new WebPortScanner();
            bool exceptionThrown = false;

            try
            {
                WebPortScanner.Scan(10, 99999, PortStatus.ANY);
            }
            catch (Exception){
                exceptionThrown = true;
            }
            
            Assert.True(exceptionThrown);
        }

        [Fact]
        public void TestInvalidPortScanRange()
        {
            WebPortScanner WebPortScanner = new WebPortScanner();
            bool exceptionThrown = false;

            try
            {
                WebPortScanner.Scan(30, 15, PortStatus.ANY);
            }
            catch (Exception){
                exceptionThrown = true;
            }
            
            Assert.True(exceptionThrown);
        }

        [Theory]
        [InlineData(PortStatus.FREE)]
        [InlineData(PortStatus.IN_USE)]
        [InlineData(PortStatus.UNKNOWN)]
        public void TestValidPortStatus(PortStatus status)
        {
            WebPortScanner WebPortScanner = new WebPortScanner();
            IEnumerable<IPrintablePortStatus> scanResult;

            try
            {
                scanResult = WebPortScanner.Scan(0, 65535, status);
            }
            catch (Exception)
            {
                // No web ports found...
                return;
            }
            
            string portStatusString = status.ToString();
            Assert.True(scanResult.All(x => x.GetStatus() == portStatusString));
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(100, 200)]
        [InlineData(150, 500)]
        public void TestPortScanRange(int minPort, int maxPort)
        {
            WebPortScanner WebPortScanner = new WebPortScanner();
            IEnumerable<IPrintablePortStatus> scanResult;

            try
            {
                scanResult = WebPortScanner.Scan(minPort, maxPort, PortStatus.ANY);
            }
            catch (Exception)
            {
                // No web ports found...
                return;
            }
            
            Assert.Equal(maxPort - minPort, scanResult.Count());
        }
    }
}