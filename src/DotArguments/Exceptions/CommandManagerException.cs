using System;
using System.Runtime.Serialization;

namespace DotArguments.Exceptions
{
    /// <summary>
    /// Base exception for errors of <see cref="CommandManager"/>.
    /// </summary>
    [Serializable]
    public class CommandManagerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManagerException"/> class.
        /// </summary>
        public CommandManagerException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManagerException"/> class.
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public CommandManagerException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManagerException"/> class.
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public CommandManagerException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManagerException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected CommandManagerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
