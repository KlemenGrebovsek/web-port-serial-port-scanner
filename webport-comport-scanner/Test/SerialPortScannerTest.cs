using System;
using System.IO.Ports;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using webport_comport_scanner.Model;
using webport_comport_scanner.Scanner;

namespace webport_comport_scanner.Test
{
    public class SerialPortScannerTest
    {
        [Fact]
        public void Test_MinPortLimit()
        {
            var spScanner = new SerialPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token; 
            
            try
            {
                Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                {
                    var scanProperties = new ScanProperties(-1, 3, PortStatus.Any);
                    await spScanner.ScanAsync(scanProperties, cToken);
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
            var spScanner = new SerialPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token; 
            
            try
            {
                Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                {
                    var scanProperties = new ScanProperties(1, 65536, PortStatus.Any);
                    await spScanner.ScanAsync(scanProperties, cToken);
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
            var spScanner = new SerialPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token; 
            
            try
            {
                Assert.ThrowsAsync<ArgumentException>(async () =>
                {
                    var scanProperties = new ScanProperties(3, 1, PortStatus.Any);
                    await spScanner.ScanAsync(scanProperties, cToken);
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
            var spScanner = new SerialPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token; 
            
            try
            {
                var task = spScanner.ScanAsync(new ScanProperties(1, 3, PortStatus.Any), cToken);
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
        [InlineData(PortStatus.In_use)]
        [InlineData(PortStatus.Any)]
        public void Test_ValidPortStatus(PortStatus status)
        {
            const int minPort = 1;
            const int maxPort = 2;
            
            var spScanner = new SerialPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token;
            
            IList<IPrintablePortStatus> sResult = default;
            
            try
            {
                var task = spScanner.ScanAsync(new ScanProperties(minPort, maxPort, status), cToken);
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
                statusOk = sResult.All(x => x.GetStatusString() == portStatusString ||
                                            x.GetStatusString() == "Unknown");
            }
            
            Assert.True(statusOk);
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(2, 4)]
        [InlineData(1, 3)]
        public void Test_PortScanRangeEquals(int minPort, int maxPort)
        {
            var spScanner = new SerialPortScanner();
            var cancellationTokenSource = new CancellationTokenSource();
            var cToken = cancellationTokenSource.Token;
            
            IList<IPrintablePortStatus> sResult = default;

            try
            {
                var task = spScanner.ScanAsync(new ScanProperties(minPort, maxPort, PortStatus.Any), cToken);
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