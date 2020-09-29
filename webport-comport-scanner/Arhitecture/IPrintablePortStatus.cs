namespace webport_comport_scanner.Models
{
    public interface IPrintablePortStatus
    {
        /// <summary>
        /// Gets maximum length of print string for this object.
        /// </summary>
        /// <returns>An integer representing maximum length of print string.</returns>
        public int GetMaxPrintLenght();

        /// <summary>
        /// Gets port name as string.
        /// </summary>
        /// <returns>A string representing port name.</returns>
        public string GetName();

        /// <summary>
        /// Gets port status as string.
        /// </summary>
        /// <returns>A string representing port status.</returns>
        public string GetStatus();
    }
}
