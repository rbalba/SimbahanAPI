using System;
using System.Runtime.Serialization;

namespace Simbahan.Exceptions
{
    [Serializable]
    public class ModelNotFoundException : Exception
    {
        public ModelNotFoundException()
        {
        }

        public ModelNotFoundException(string message) : base(message)
        {
        }

        public ModelNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ModelNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}