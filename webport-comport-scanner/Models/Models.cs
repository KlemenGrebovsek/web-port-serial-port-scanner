using System;
using System.Collections.Generic;
using System.Text;

namespace webport_comport_scanner.Models
{

    public class ComPortInfo
    {
        public string Name { get; set; }

        public PortStatus Status { get; set; }

    }
    public enum PortStatus
    {
        IN_USE, FREE, UNKNOWN
    }

    public class WebPortInfo
    {
        public int Port { get; set; }

        public PortStatus Status { get; set; }
    }
    
}
