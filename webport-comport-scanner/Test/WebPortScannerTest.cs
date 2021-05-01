using System;
using Xunit;
using System.Linq;
using System.Threading;
using webport_comport_scanner.Models;
using webport_comport_scanner.Scanners;

namespace webport_comport_scanner.Test
{
    public class WebPortScannerTest
    {
        [Fact]
        public void Test_MinPortLimit()
        {
            var wpScanner = new WebPortScanner();
            
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                var scanProperties = new ScanProperties
                {
                    MinPort = -1,
                    MaxPort = 3
                };
                
                await wpScanner.ScanAsync(scanProperties, CancellationToken.None);
            });
        }
        
        [Fact]
        public void Test_MaxPortLimit()
        {
            var wpScanner = new WebPortScanner();

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                var scanProperties = new ScanProperties
                {
                    MinPort = 1,
                    MaxPort = 65536
                };
                
                await wpScanner.ScanAsync(scanProperties, CancellationToken.None);
            });
        }
   
        [Theory]
        [InlineData(3, 1)]
        [InlineData(-3, 0)]
        [InlineData(-5, -6)]
        public void Test_InvalidPortScanRange(int minPort, int maxPort)
        {
            var wpScanner = new WebPortScanner();

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                var scanProperties = new ScanProperties
                {
                    MinPort = minPort,
                    MaxPort = maxPort
                };
                
                await wpScanner.ScanAsync(scanProperties, CancellationToken.None);
            });
        }
  
       [Theory]
       [InlineData(15, 30)]
       [InlineData(40, 100)]
       [InlineData(10000, 10050)]
       public void Test_ValidPortScanRange(int minPort, int maxPort)
       {
           var wpScanner = new WebPortScanner();
           var totalPorts = (maxPort - minPort) + 1;
           
           var scanProperties = new ScanProperties
           {
               MinPort = minPort,
               MaxPort = maxPort
           };

           var actual = wpScanner.ScanAsync(scanProperties, CancellationToken.None)
               .GetAwaiter()
               .GetResult()
               .Count();
            
           Assert.Equal(totalPorts, actual);
       }
    }
}