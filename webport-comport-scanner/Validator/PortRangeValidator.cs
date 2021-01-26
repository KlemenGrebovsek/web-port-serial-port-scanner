using System;

namespace webport_comport_scanner.Validator
{
    public static class PortRangeValidator
    {
        /// <summary>
        /// Provides functionality to validate port range.
        /// </summary>
        /// <exception cref="ArgumentException">If min and max port are logically wrong.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If min or max value is outside the port range. </exception>
        /// <param name="minPort">Scan from this port (including).</param>
        /// <param name="maxPort">Scan to this port (including).</param>
        public static void Validate(int minPort, int maxPort)
        {
            if (maxPort < minPort)
                throw new ArgumentException("Max port value is less than min port value.");
            
            if (minPort < 0)
                throw new ArgumentOutOfRangeException(nameof(minPort));
            
            if (maxPort > 65535)
                throw new ArgumentOutOfRangeException(nameof(maxPort));
        }
    }
}