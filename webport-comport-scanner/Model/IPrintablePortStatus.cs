namespace webport_comport_scanner.Model
{
    public interface IPrintablePortStatus
    {
        /// <summary>
        /// Get port name as string.
        /// </summary>
        /// <returns>A string representing port name.</returns>
        public string GetName();

        /// <summary>
        /// Get port status as string.
        /// </summary>
        /// <returns>A string representing port status.</returns>
        public string GetStatusString();
    }
}
