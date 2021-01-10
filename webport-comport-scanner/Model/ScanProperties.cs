namespace webport_comport_scanner.Model
{
    /// <summary>
    /// Contains scan properties for scan commands.
    /// </summary>
    public class ScanProperties : IScanProperties
    {
        private readonly int _minPort;
        
        private readonly int _maxPort;

        private readonly PortStatus _portStatus;

        public ScanProperties(int minPort, int maxPort, PortStatus portStatus)
        {
            _minPort = minPort;
            _maxPort = maxPort;
            _portStatus = portStatus;
        }
        
        public int GetMinPort()
        {
            return _minPort;
        }

        public int GetMaxPort()
        {
            return _maxPort;
        }

        public PortStatus GetSearchStatus()
        {
            return _portStatus;
        }
    }
}