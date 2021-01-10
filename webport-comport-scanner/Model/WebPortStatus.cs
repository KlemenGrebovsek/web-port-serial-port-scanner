namespace webport_comport_scanner.Model
{
    /// <summary>
    /// Represents web port and its status.
    /// </summary>
    public class WebPortStatus : IPrintablePortStatus
    {
        private readonly string _name;
        private readonly string _status;

        public WebPortStatus(int port, PortStatus status)
        {
            _name = port.ToString();
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
