namespace webport_comport_scanner.Models
{
    public interface IPrintablePortStatus
    {
        public string GetName();
        
        public string GetStatusString();

        public PortStatus GetPortStatus();
    }
}
