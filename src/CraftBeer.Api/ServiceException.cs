using System;
using System.Runtime.Serialization;

namespace CraftBeer.Api
{
    public class ServiceExceptionException : Exception
    {
        public ServiceExceptionException() { }

        public ServiceExceptionException(string message) : base(message) { }

        public ServiceExceptionException(string message, Exception inner) : base(message, inner) { }
        
        protected ServiceExceptionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}