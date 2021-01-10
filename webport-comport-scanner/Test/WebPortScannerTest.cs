using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using webport_comport_scanner.Model;
using webport_comport_scanner.Scanner;

namespace webport_comport_scanner.Test
{
    public class WebPortScannerTest
    {
        [Fact]
        public void Test_MinPortLimit()
        {
            var webScanner = new WebPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token; 
            
            try
            {
                Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                {
                    var scanProperties = new ScanProperties(-1, 100, PortStatus.Any);
                    await webScanner.ScanAsync(scanProperties, cToken);
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
            var wpScanner = new WebPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token; 
            
            try
            {
                Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                {
                    var scanProperties = new ScanProperties(10, 65536, PortStatus.Any);
                    await wpScanner.ScanAsync(scanProperties, cToken);
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
            var wpScanner = new WebPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token; 
            
            try
            {
                Assert.ThrowsAsync<ArgumentException>(async () =>
                {
                    var scanProperties = new ScanProperties(30, 15, PortStatus.Any);
                    await wpScanner.ScanAsync(scanProperties, cToken);
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
        public void Test_ValidPortScanRange()
        {
            var wpScanner = new WebPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token; 
            
            try
            {
                var task = wpScanner.ScanAsync(new ScanProperties(15, 30, PortStatus.Any), cToken);
                var result = task.Result;
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
            finally
            {
                cancellationTokenSource.Dispose(); 
            }
            
            Assert.True(true);
        }

        [Theory]
        [InlineData(PortStatus.Free)]
        [InlineData(PortStatus.InUse)]
        [InlineData(PortStatus.Any)]
        [InlineData(PortStatus.Unknown)]
        public void Test_ValidPortStatus(PortStatus status)
        {
            const int minPort = 0;
            const int maxPort = 3000;
            
            var wpScanner = new WebPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token;
            
            IList<IPrintablePortStatus> sResult = default;
            
            try
            {
                var task = wpScanner.ScanAsync(new ScanProperties(minPort, maxPort, status), cToken);
                sResult = task.Result.ToList();
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
            finally
            {
                cancellationTokenSource.Dispose(); 
            }

            bool statusOk;

            if (status == PortStatus.Any)
                statusOk = ((maxPort - minPort) + 1) == sResult.Count;
            else
            {
                var portStatusString = status.ToString();
                statusOk = sResult.All(x => x.GetStatusString() == portStatusString);
            }
            
            Assert.True(statusOk);
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(100, 200)]
        [InlineData(150, 500)]
        public void Test_PortScanRangeEquals(int minPort, int maxPort)
        {
            var wpScanner = new WebPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token;
            
            IList<IPrintablePortStatus> sResult = default;

            try
            {
                var task = wpScanner.ScanAsync(new ScanProperties(minPort, maxPort, PortStatus.Any), cToken);
                sResult = task.Result.ToList();
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
            
            Assert.Equal((maxPort - minPort) + 1, sResult.Count);
        }
    }
}