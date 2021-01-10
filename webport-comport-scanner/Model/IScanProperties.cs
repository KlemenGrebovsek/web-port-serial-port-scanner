namespace webport_comport_scanner.Model
{
    public interface IScanProperties
    {
        int GetMinPort();
        
        int GetMaxPort();
        
        PortStatus GetSearchStatus();
    }
}