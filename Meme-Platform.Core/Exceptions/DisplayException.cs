using System;

namespace Meme_Platform.Core.Exceptions
{
    public class DisplayException : Exception
    {
        public string Reason { get; set; }

        public DisplayException(string message) : base(message)
        {
            Reason = message;
        }

        public DisplayException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
