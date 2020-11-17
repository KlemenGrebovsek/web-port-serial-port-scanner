namespace webport_comport_scanner.Architecture
{
    public interface IPrintablePortStatus
    {
        /// <summary>
        /// Gets port name as string.
        /// </summary>
        /// <returns>A string representing port name.</returns>
        public string GetName();

        /// <summary>
        /// Gets port status as string.
        /// </summary>
        /// <returns>A string representing port status.</returns>
        public string GetStatusString();
    }
}
