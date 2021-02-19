using System;

namespace webport_comport_scanner.Exceptions
{
    public class SerialPortNotFoundException: Exception
    {
        private const string ErrorMessage = "No serial port found.";
        
        public SerialPortNotFoundException() : base(ErrorMessage)
        {
        }
    }
}