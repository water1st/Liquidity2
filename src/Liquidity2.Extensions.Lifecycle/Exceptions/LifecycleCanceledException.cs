using System;
using System.Runtime.Serialization;

namespace Liquidity2.Extensions.Lifecycle
{
    public class LifecycleCanceledException : Exception
    {
        public LifecycleCanceledException()
        {
        }

        public LifecycleCanceledException(string message) : base(message)
        {
        }

        public LifecycleCanceledException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LifecycleCanceledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
