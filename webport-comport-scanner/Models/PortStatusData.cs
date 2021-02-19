namespace webport_comport_scanner.Models
{
    /// <summary>
    /// Represents port and its status after performed scan.
    /// </summary>
    public class PortStatusData : IPrintablePortStatus
    {
        private readonly string _name;
        private readonly string _status;
        public PortStatus PortStatus { get; }

        public PortStatusData(string port, PortStatus status)
        {
            _name = port;
            _status = status.ToString();
            PortStatus = status;
        }
        
        public string GetName()
        {
            return _name;
        }

        public string GetStatusString()
        {
            return _status;
        }
    }  
}
