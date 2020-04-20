namespace webport_comport_scanner.Models
{
    public class WebPortInfo : IPrintable
    {
        public int Port { get; set; }
        public PortStatus Status { get; set; }

        public string GetName()
        {
            return Port.ToString();
        }

        public int GetPrintMaxLenght()
        {
            int nameLen = Port.ToString().Length;
            int statusLen = Status.ToString().Length;

            return (nameLen > statusLen) ? nameLen : statusLen;
        }

        public string GetValue()
        {
            return Status.ToString();
        }
    }  
}
