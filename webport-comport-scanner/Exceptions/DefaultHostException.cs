using System;

namespace webport_comport_scanner.Exceptions
{
    public class DefaultHostException : Exception
    {
        private const string ErrorMessage = "Default host not found.";
        
        public DefaultHostException() : base(ErrorMessage)
        {
        }
    }
}