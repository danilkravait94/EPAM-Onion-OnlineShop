using System;
using System.Runtime.Serialization;

namespace ConsoleEShop
{
    [Serializable]
    internal class AccessException : Exception
    {
        public AccessException()
        {
        }

        public AccessException(string message) : base(message)
        {
        }

        public AccessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}