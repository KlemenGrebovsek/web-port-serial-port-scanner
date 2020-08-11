namespace webport_comport_scanner.Models
{
    public interface IPrintableScanResult
    {
        /// <summary>
        /// Calculates maximum lenght of print string for this object.
        /// </summary>
        /// <returns>An integer representing maximum size of print string.</returns>
        public int GetMaxPrintLenght();

        /// <summary>
        /// Gets port name.
        /// </summary>
        /// <returns>A string representing port name.</returns>
        public string GetName();

        /// <summary>
        /// Gets port status.
        /// </summary>
        /// <returns>A string representing port status.</returns>
        public string GetStatus();
    }
}
