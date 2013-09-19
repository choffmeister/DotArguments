using System;
using System.Runtime.Serialization;

namespace DotArguments
{
    /// <summary>
    /// Base exception the indicates a problem with an argument container definition.
    /// </summary>
    [Serializable]
    public class ContainerDefinitionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerDefinitionException"/> class
        /// </summary>
        public ContainerDefinitionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerDefinitionException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public ContainerDefinitionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerDefinitionException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public ContainerDefinitionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerDefinitionException"/> class
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected ContainerDefinitionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
