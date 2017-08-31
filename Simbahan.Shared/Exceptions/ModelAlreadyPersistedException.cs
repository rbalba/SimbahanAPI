using System;
using System.Runtime.Serialization;

namespace Simbahan.Exceptions
{
    [Serializable]
    public class ModelAlreadyPersistedException : Exception
    {
        public ModelAlreadyPersistedException()
        {
        }

        public ModelAlreadyPersistedException(string message) : base(message)
        {
        }

        public ModelAlreadyPersistedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ModelAlreadyPersistedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}