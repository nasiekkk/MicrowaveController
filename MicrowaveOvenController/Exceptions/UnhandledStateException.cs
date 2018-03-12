using System;

namespace MicrowaveOvenController
{
    public class UnhandledStateException : Exception
    {
        public UnhandledStateException() { }
        public UnhandledStateException(string message)
            : base(message){}
        public UnhandledStateException(string message, Exception innerException) : base(message, innerException) { }
    }
}
