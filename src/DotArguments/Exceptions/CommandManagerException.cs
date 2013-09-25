using System;
using System.Runtime.Serialization;
using DotArguments;

namespace DotArguments.Exceptions
{
    /// <summary>
    /// Base exception for errors of <see cref="CommandManager"/>.
    /// </summary>
    [Serializable]
    public class CommandManagerException : Exception
    {
        private readonly CommandManager.CommandDefinition commandDefinition;

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
        /// <param name="commandDefinition">The command definition.</param>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public CommandManagerException(CommandManager.CommandDefinition commandDefinition, string message, Exception inner)
            : base(message, inner)
        {
            this.commandDefinition = commandDefinition;
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

        /// <summary>
        /// Gets the command definition.
        /// </summary>
        /// <value>The command definition.</value>
        public CommandManager.CommandDefinition CommandDefinition
        {
            get { return this.commandDefinition; }
        }
    }
}
