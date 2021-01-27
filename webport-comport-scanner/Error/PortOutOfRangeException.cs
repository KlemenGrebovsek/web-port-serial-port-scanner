using System;

namespace webport_comport_scanner.Error
{
    public class PortOutOfRangeException : Exception
    {
        public PortOutOfRangeException()
        {
        }

        public PortOutOfRangeException(string message)
            : base(message)
        {
        }

        public PortOutOfRangeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}