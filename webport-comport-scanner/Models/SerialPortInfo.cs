namespace webport_comport_scanner.Models
{
    public class SerialPortInfo : IPrintableScanResult
    {
        private string name;
        private PortStatus status;

        public SerialPortInfo(string name, PortStatus status)
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

        public int GetMaxPrintLenght()
        {
            int nameLen = name.Length;
            int statusLen = status.ToString().Length;

            return (nameLen > statusLen) ? nameLen : statusLen;
        }
 
    }
}
