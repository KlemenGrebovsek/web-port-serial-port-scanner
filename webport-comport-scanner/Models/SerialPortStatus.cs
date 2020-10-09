namespace webport_comport_scanner.Models
{
    /// <summary>
    /// Represents serial port and its status.
    /// </summary>
    public class SerialPortStatus : IPrintablePortStatus
    {
        private string name;
        private PortStatus status;

        public SerialPortStatus(string name, PortStatus status)
        {
            this.name = name;
            this.status = status;
        }

        public string GetName()
        {
            return name;
        }

        public string GetStatus()
        {
            return status.ToString();
        }

        public PortStatus GetStatusEnum()
        {
            return status;
        }

        public int GetMaxPrintLenght()
        {
            int statusLen = status.ToString().Length;

            return (name.Length > statusLen) ? name.Length : statusLen;
        }
 
    }
}
