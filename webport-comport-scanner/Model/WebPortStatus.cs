namespace webport_comport_scanner.Models
{
    /// <summary>
    /// Represents web port and its status.
    /// </summary>
    public class WebPortStatus : IPrintablePortStatus
    {
        private int _port;
        private PortStatus _status;

        public WebPortStatus(int port, PortStatus status)
        {
            _port = port;
            _status = status;
        }

        public PortStatus GetPortStatus()
        {
            return _status;
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

        public int GetMaxPrintLenght()
        {
            int nameLen = _port.ToString().Length;
            int statusLen = _status.ToString().Length;

            return (nameLen > statusLen) ? nameLen : statusLen;
        }

    }  
}
