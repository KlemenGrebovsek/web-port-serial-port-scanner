using webport_comport_scanner.Error;

namespace webport_comport_scanner.Validator
{
    public static class PortRangeValidator
    {
        /// <summary>
        /// Validates port range.
        /// </summary>
        /// <exception cref="InvalidPortRangeException">If min and max port are logically wrong.</exception>
        /// <exception cref="PortOutOfRangeException">If min or max value is outside the port range. </exception>
        /// <param name="minPort">Min port value.</param>
        /// <param name="maxPort">Max port value.</param>
        public static void Validate(int minPort, int maxPort)
        {
            if (maxPort < minPort)
                throw new InvalidPortRangeException("Max port value is less than min port value.");
            
            if (minPort < 0 || minPort > 65535)
                throw new PortOutOfRangeException("Min port in out of range");
            
            if (maxPort < 0 || maxPort > 65535)
                throw new PortOutOfRangeException("Max port in out of range");
        }
    }
}