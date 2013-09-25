using System;
using System.Runtime.Serialization;

namespace DotArguments.Exceptions
{
    /// <summary>
    /// Base exception the indicates a problem with an argument argument definition.
    /// </summary>
    [Serializable]
    public class ArgumentDefinitionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDefinitionException"/> class
        /// </summary>
        public ArgumentDefinitionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDefinitionException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public ArgumentDefinitionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDefinitionException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public ArgumentDefinitionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDefinitionException"/> class
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected ArgumentDefinitionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
