namespace webport_comport_scanner.Models
{
    public class ComPortInfo : IPrintable
    {
        private string name;
        private PortStatus status;

        public ComPortInfo(string name, PortStatus status)
        {
            this.name = name;
            this.status = status;
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetValue()
        {
            return status.ToString();
        }

        public int GetPrintMaxLenght()
        {
            int nameLen = name.Length;
            int statusLen = status.ToString().Length;

            return (nameLen > statusLen) ? nameLen : statusLen;
        }
 
    }
}
