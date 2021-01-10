namespace webport_comport_scanner.Model
{
    /// <summary>
    /// Represents serial port and its status.
    /// </summary>
    public class SerialPortStatus : IPrintablePortStatus
    {
        private readonly string _name;
        private readonly string _status;

        public SerialPortStatus(string name, PortStatus status)
        {
            _name = name;
            _status = status.ToString();
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
