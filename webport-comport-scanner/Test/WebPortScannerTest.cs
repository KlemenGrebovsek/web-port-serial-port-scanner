using System;
using Xunit;
using System.Linq;
using System.Threading;
using webport_comport_scanner.Scanner;

namespace webport_comport_scanner.Test
{
    public class WebPortScannerTest
    {
        [Fact]
        public void Test_MinPortLimit()
        {
            var spScanner = new WebPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token; 
            
            try
            {
                Assert.ThrowsAsync<ArgumentOutOfRangeException>( () =>
                {
                    spScanner.Scan(-1, 3, cToken);
                    return null;
                });
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
            finally
            {
                cancellationTokenSource.Dispose(); 
            }
        }

        [Fact]
        public void Test_MaxPortLimit()
        {
            var spScanner = new WebPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token; 
            
            try
            {
                Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                {
                    spScanner.Scan(1, 65536, cToken);
                    return null;
                });
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
            finally
            {
                cancellationTokenSource.Dispose(); 
            }
        }

        [Fact]
        public void Test_InvalidPortScanRange()
        {
            var spScanner = new WebPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token; 
            
            try
            {
                Assert.ThrowsAsync<ArgumentException>(() =>
                {
                    spScanner.Scan(3, 1, cToken);
                    return null;
                });
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
            finally
            {
                cancellationTokenSource.Dispose(); 
            }
        }

        [Theory]
        [InlineData(15, 30)]
        [InlineData(40, 100)]
        [InlineData(10000, 10050)]
        public void Test_PortScanRange(int minPort, int maxPort)
        {
            var spScanner = new WebPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token;

            var totalPorts = (maxPort - minPort) + 1;
            var actual = -1;
            
            try
            {
                actual = spScanner.Scan(minPort, maxPort, cToken).Count();
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
            
            Assert.Equal(totalPorts, actual);
        }
    }
}