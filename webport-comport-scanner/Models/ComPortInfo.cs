namespace webport_comport_scanner.Models
{
    public class ComPortInfo : IPrintable
    {
        public string Name { get; set; }
        public PortStatus Status { get; set; }

        public string GetName()
        {
            return Name;
        }

        public int GetPrintMaxLenght()
        {
            int nameLen = Name.Length;
            int statusLen = Status.ToString().Length;

            return (nameLen > statusLen) ? nameLen : statusLen;
        }

        public string GetValue()
        {
            return Status.ToString();
        }
    }
}
