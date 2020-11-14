using webport_comport_scanner.Architecture;

namespace webport_comport_scanner.Model
{
    /// <summary>
    /// Represents web port and its status.
    /// </summary>
    public class WebPortStatus : IPrintablePortStatus
    {
        private readonly int _port;
        private readonly PortStatus _status;

        public WebPortStatus(int port, PortStatus status)
        {
            _port = port;
            _status = status;
        }
        
        public string GetName()
        {
            return _port.ToString();
        }

        public string GetStatus()
        {
            return _status.ToString();
        }

        public PortStatus GetStatusEnum()
        {
            return _status;
        }

        public int GetMaxPrintLen()
        {
            var nameLen = _port.ToString().Length;
            var statusLen = _status.ToString().Length;

            return (nameLen > statusLen) ? nameLen : statusLen;
        }

    }  
}
