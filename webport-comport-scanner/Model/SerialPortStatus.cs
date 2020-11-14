using webport_comport_scanner.Architecture;

namespace webport_comport_scanner.Model
{
    /// <summary>
    /// Represents serial port and its status.
    /// </summary>
    public class SerialPortStatus : IPrintablePortStatus
    {
        private readonly string _name;
        private readonly PortStatus _status;

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

        public int GetMaxPrintLen()
        {
            var statusLen = _status.ToString().Length;

            return (_name.Length > statusLen) ? _name.Length : statusLen;
        }
 
    }
}
