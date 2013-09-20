using System;
using System.Runtime.Serialization;

namespace DotArguments
{
    /// <summary>
    /// Base exception for errors while argument parsing.
    /// </summary>
    [Serializable]
    public class ArgumentParserException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentParserException"/> class.
        /// </summary>
        public ArgumentParserException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentParserException"/> class.
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public ArgumentParserException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentParserException"/> class.
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public ArgumentParserException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentParserException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected ArgumentParserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
