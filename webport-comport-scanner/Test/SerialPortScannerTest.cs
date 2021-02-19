using System;
using System.Linq;
using Xunit;
using System.Threading;

namespace webport_comport_scanner.Test
{
    public class SerialPortScannerTest
    {
        /*
        [Fact]
        public void Test_MinPortLimit()
        {
            var spScanner = new SerialPortScanner();
            using var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token; 
            
            try
            {
                Assert.ThrowsAsync<ArgumentOutOfRangeException>( async () =>
                {
                    await spScanner.ScanAsync(-1, 3, cToken);
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
            var cToken = cancellationTokenSource.Token; 
            
            try
            {
                Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                {
                    await spScanner.ScanAsync(1, 65536, cToken);
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
            var cToken = cancellationTokenSource.Token; 
            
            try
            {
                Assert.ThrowsAsync<ArgumentException>(async () =>
                {
                    await spScanner.ScanAsync(3, 1, cToken);
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
            var cToken = cancellationTokenSource.Token;

            var totalPorts = (maxPort - minPort) + 1;
            var actual = -1;
            
            try
            {
                actual = spScanner.ScanAsync(minPort, maxPort, cToken).Result.Count();
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
            
            Assert.Equal(totalPorts, actual);
        }
        */
    }
}