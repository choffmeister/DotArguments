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
        private readonly CommandManager commandManager;
        private readonly CommandManager.CommandDefinition commandDefinition;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManagerException"/> class.
        /// </summary>
        /// <param name="commandManager">The command manager.</param>
        public CommandManagerException(CommandManager commandManager)
        {
            this.commandManager = commandManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManagerException"/> class.
        /// </summary>
        /// <param name="commandManager">The command manager.</param>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public CommandManagerException(CommandManager commandManager, string message)
            : base(message)
        {
            this.commandManager = commandManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManagerException"/> class.
        /// </summary>
        /// <param name="commandManager">The command manager.</param>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public CommandManagerException(CommandManager commandManager, string message, Exception inner)
            : base(message, inner)
        {
            this.commandManager = commandManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManagerException"/> class.
        /// </summary>
        /// <param name="commandManager">The command manager.</param>
        /// <param name="commandDefinition">The command definition.</param>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public CommandManagerException(CommandManager commandManager, CommandManager.CommandDefinition commandDefinition, string message, Exception inner)
            : base(message, inner)
        {
            this.commandManager = commandManager;
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
        /// Gets the command manager.
        /// </summary>
        /// <value>The command manager.</value>
        public CommandManager CommandManager
        {
            get { return this.commandManager; }
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
