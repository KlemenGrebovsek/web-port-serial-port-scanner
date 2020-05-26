namespace webport_comport_scanner.Models
{
    public class WebPortInfo : IPrintable
    {
        private int port;
        private PortStatus status;

        public WebPortInfo(int port, PortStatus status)
        {
            this.port = port;
            this.status = status;
        }

        public PortStatus GetPortStatus()
        {
            return status;
        }

        public string GetName()
        {
            return port.ToString();
        }

        public string GetValue()
        {
            return status.ToString();
        }

        public int GetPrintMaxLenght()
        {
            int nameLen = port.ToString().Length;
            int statusLen = status.ToString().Length;

            return (nameLen > statusLen) ? nameLen : statusLen;
        }

    }  
}
