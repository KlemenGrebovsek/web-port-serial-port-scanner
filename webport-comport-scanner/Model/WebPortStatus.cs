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

        public string GetStatusString()
        {
            return _status.ToString();
        }

        public PortStatus GetStatus()
        {
            return _status;
        }
    }  
}
