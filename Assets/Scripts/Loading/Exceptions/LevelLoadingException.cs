using System;
using System.Runtime.Serialization;

namespace GMTKGameJam2022.Loading.Exceptions
{
    /// <summary>
    /// Exception for level loading errors.
    /// </summary>
    [Serializable]
    internal class LevelLoadingException : Exception
    {
        public LevelLoadingException()
        {
        }

        public LevelLoadingException(string message) : base(message)
        {
        }

        public LevelLoadingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LevelLoadingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}