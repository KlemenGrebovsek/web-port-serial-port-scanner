namespace webport_comport_scanner.Model
{
    /// <summary>
    /// Represents port and its status after performed scan.
    /// </summary>
    public class PortStatusData : IPrintablePortStatus
    {
        private readonly string _name;
        private readonly string _status;

        public PortStatusData(string port, string status)
        {
            _name = port;
            _status = status;
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
