namespace webport_comport_scanner.Models
{
    public class PortStatusData : IPrintablePortStatus
    {
        private readonly string _name;
        private readonly string _status;
        private readonly PortStatus _portStatus;

        public PortStatusData(string port, PortStatus status)
        {
            _name = port;
            _status = status.ToString();
            _portStatus = status;
        }

        public string GetName() => _name;

        public string GetStatusString() => _status;

        public PortStatus GetPortStatus() => _portStatus;
    }  
}
