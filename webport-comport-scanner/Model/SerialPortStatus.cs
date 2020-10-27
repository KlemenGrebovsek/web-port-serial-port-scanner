namespace webport_comport_scanner.Models
{
    /// <summary>
    /// Represents serial port and its status.
    /// </summary>
    public class SerialPortStatus : IPrintablePortStatus
    {
        private string _name;
        private PortStatus _status;

        public SerialPortStatus(string name, PortStatus status)
        {
            _name = name;
            _status = status;
        }

        public string GetName()
        {
            return _name;
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
            int statusLen = _status.ToString().Length;

            return (_name.Length > statusLen) ? _name.Length : statusLen;
        }
 
    }
}
