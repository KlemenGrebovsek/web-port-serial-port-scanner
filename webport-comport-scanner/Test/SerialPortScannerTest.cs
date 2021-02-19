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
            using var cancellationTokenSource = new CancellationTokenSource();
            
            try
            {
                Assert.ThrowsAsync<ArgumentOutOfRangeException>( async () =>
                {
                    var scanProperties = new ScanProperties()
                    {
                        MinPort = -1,
                        MaxPort = 3
                    };
                    await spScanner.ScanAsync(scanProperties, cancellationTokenSource.Token);
                });
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }

        [Fact]
        public void Test_MaxPortLimit()
        {
            var spScanner = new SerialPortScanner();
            using var cancellationTokenSource = new CancellationTokenSource();
            
            try
            {
                Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                {
                    var scanProperties = new ScanProperties()
                    {
                        MinPort = 1,
                        MaxPort = 65536
                    };
                    await spScanner.ScanAsync(scanProperties, cancellationTokenSource.Token);
                });
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }

        [Fact]
        public void Test_InvalidPortScanRange()
        {
            var spScanner = new SerialPortScanner();
            using var cancellationTokenSource = new CancellationTokenSource();
            
            try
            {
                Assert.ThrowsAsync<ArgumentException>(async () =>
                {
                    var scanProperties = new ScanProperties()
                    {
                        MinPort = 3,
                        MaxPort = 1
                    };
                    await spScanner.ScanAsync(scanProperties, cancellationTokenSource.Token);
                });
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }

        [Theory]
        [InlineData(15, 30)]
        [InlineData(40, 100)]
        [InlineData(10000, 10050)]
        public void Test_PortScanRange(int minPort, int maxPort)
        {
            var spScanner = new SerialPortScanner();
            using var cancellationTokenSource = new CancellationTokenSource();
            
            var totalPorts = (maxPort - minPort) + 1;
            var actual = -1;
            
            var scanProperties = new ScanProperties()
            {
                MinPort = minPort,
                MaxPort = maxPort
            };
            
            try
            {
                actual = spScanner.ScanAsync(scanProperties, cancellationTokenSource.Token).Result.Count();
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
            
            Assert.Equal(totalPorts, actual);
        }
    }
}