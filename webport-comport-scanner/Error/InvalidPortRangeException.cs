using System;

namespace webport_comport_scanner.Error
{
    public class InvalidPortRangeException : Exception
    {
        public InvalidPortRangeException()
        {
        }

        public InvalidPortRangeException(string message)
            : base(message)
        {
        }

        public InvalidPortRangeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}