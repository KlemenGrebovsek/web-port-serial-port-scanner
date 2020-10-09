using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using webport_comport_scanner.Scanners;
using webport_comport_scanner.Models;

namespace webport_comport_scanner.Test
{
    public class SerialPortScannerTest
    {
        [Fact]
        public void TestMinPortLimit()
        {
            SerialPortScanner serialPortScanner = new SerialPortScanner();
            bool exceptionThrown = false;

            try
            {
                serialPortScanner.ScanStatus(-1, 100, PortStatus.ANY);
            }
            catch (Exception){
                exceptionThrown = true;
            }
            
            Assert.True(exceptionThrown);
        }


        [Fact]
        public void TestMaxPortLimit()
        {
            SerialPortScanner serialPortScanner = new SerialPortScanner();
            bool exceptionThrown = false;

            try
            {
                serialPortScanner.ScanStatus(10, 99999, PortStatus.ANY);
            }
            catch (Exception){
                exceptionThrown = true;
            }
            
            Assert.True(exceptionThrown);
        }

        [Fact]
        public void TestInvalidPortScanRange()
        {
            SerialPortScanner serialPortScanner = new SerialPortScanner();
            bool exceptionThrown = false;

            try
            {
                serialPortScanner.ScanStatus(30, 15, PortStatus.ANY);
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
            SerialPortScanner serialPortScanner = new SerialPortScanner();
            IEnumerable<IPrintablePortStatus> scanResult;

            try
            {
                scanResult = serialPortScanner.ScanStatus(0, 65535, status);
            }
            catch (Exception)
            {
                // No serial ports found...
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
            SerialPortScanner WebPortScanner = new SerialPortScanner();
            IEnumerable<IPrintablePortStatus> scanResult;

            try
            {
                scanResult = WebPortScanner.ScanStatus(minPort, maxPort, PortStatus.ANY);
            }
            catch (Exception)
            {
                // No serial ports found...
                return;
            }
            
            Assert.Equal(maxPort - minPort, scanResult.Count());
        }
    }
}