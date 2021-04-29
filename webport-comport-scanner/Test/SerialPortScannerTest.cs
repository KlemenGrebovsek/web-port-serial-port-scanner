using System;
using System.Linq;
using Xunit;
using System.Threading;
using webport_comport_scanner.Models;
using webport_comport_scanner.Scanners;

namespace webport_comport_scanner.Test
{
    public class SerialPortScannerTest
    {
        [Fact]
        public void Test_MinPortLimit()
        {
            var spScanner = new SerialPortScanner();
            
            Assert.ThrowsAsync<ArgumentOutOfRangeException>( async () =>
            {
                var scanProperties = new ScanProperties
                {
                    MinPort = -1,
                    MaxPort = 3
                };
                
                await spScanner.ScanAsync(scanProperties, CancellationToken.None);
            });
        }

        [Fact]
        public void Test_MaxPortLimit()
        {
            var spScanner = new SerialPortScanner();
            
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                var scanProperties = new ScanProperties
                {
                    MinPort = 1,
                    MaxPort = 65536
                };
                
                await spScanner.ScanAsync(scanProperties, CancellationToken.None);
            });
        }

        [Theory]
        [InlineData(3, 1)]
        [InlineData(-3, 0)]
        [InlineData(-5, -6)]
        public void Test_InvalidPortScanRange(int minPort, int maxPort)
        {
            var spScanner = new SerialPortScanner();
            
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                var scanProperties = new ScanProperties
                {
                    MinPort = minPort,
                    MaxPort = maxPort
                };
                
                await spScanner.ScanAsync(scanProperties, CancellationToken.None);
            });
        }

        [Theory]
        [InlineData(15, 30)]
        [InlineData(40, 100)]
        [InlineData(10000, 10050)]
        public void Test_ValidPortScanRange(int minPort, int maxPort)
        {
            var spScanner = new SerialPortScanner();
            var totalPorts = (maxPort - minPort) + 1;
            
            var scanProperties = new ScanProperties
            {
                MinPort = minPort,
                MaxPort = maxPort
            };
            
            var actual = spScanner.ScanAsync(scanProperties, CancellationToken.None)
                .GetAwaiter()
                .GetResult()
                .Count();
            
            Assert.Equal(totalPorts, actual);
        }
    }
}