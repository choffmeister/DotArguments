using System;
using System.Collections.Generic;
using System.Linq;

namespace DotArguments
{
    /// <summary>
    /// Allows registering of <see cref="ICommand"/> implementations and
    /// delegates invocation.
    /// </summary>
    public class CommandManager
    {
        private readonly Dictionary<string, CommandDefinition> commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager"/> class.
        /// </summary>
        public CommandManager()
        {
            this.commands = new Dictionary<string, CommandDefinition>();
        }

        /// <summary>
        /// Gets the commands.
        /// </summary>
        /// <value>The commands.</value>
        public Dictionary<string, CommandDefinition> Commands
        {
            get { return this.commands; }
        }

        /// <summary>
        /// Takes the command-line arguments, instantiates the requested command, populates the command
        /// instance with the arguments and invokes it.
        /// </summary>
        /// <returns>The exit code.</returns>
        /// <param name="arguments">The command-line arguments whereas the first argument is the name of the command.</param>
        public int Execute(string[] arguments)
        {
            if (arguments == null)
                throw new ArgumentNullException("arguments");
            if (arguments.Length == 0)
                throw new CommandManagerException("Command name is missing");

            string commandName = arguments.First();
            string[] commandArguments = arguments.Skip(1).ToArray();
            CommandDefinition commandDefinition = null;

            if (!this.commands.TryGetValue(commandName, out commandDefinition))
            {
                throw new CommandManagerException(string.Format("Command name {0} is unknown", commandName));
            }

            ICommand command = (ICommand)commandDefinition.ArgumentDefinition.Parse(commandArguments);
            return command.Execute();
        }

        /// <summary>
        /// Registers a new command type. The type must implement <see cref="ICommand"/>.
        /// </summary>
        /// <param name="type">The command type.</param>
        public void RegisterCommand(Type type)
        {
            CommandDefinition definition = new CommandDefinition(type);

            this.commands.Add(definition.CommandAttribute.Name, definition);
        }

        /// <summary>
        /// Combined information about an command.
        /// </summary>
        public class CommandDefinition
        {
            private readonly Type type;
            private readonly CommandAttribute commandAttribute;
            private readonly ArgumentDefinition argumentDefinition;

            /// <summary>
            /// Initializes a new instance of the <see cref="CommandDefinition"/> class.
            /// </summary>
            /// <param name="type">The command type.</param>
            public CommandDefinition(Type type)
            {
                if (!typeof(ICommand).IsAssignableFrom(type))
                    throw new ArgumentException("type must implement DotArguments.ICommand", "type");

                this.type = type;
                this.commandAttribute = type.GetCustomAttributes(false).OfType<CommandAttribute>().Single();
                this.argumentDefinition = new ArgumentDefinition(type);
            }

            /// <summary>
            /// Gets the type.
            /// </summary>
            /// <value>The type.</value>
            public Type Type
            {
                get { return this.type; }
            }

            /// <summary>
            /// Gets the attribute.
            /// </summary>
            /// <value>The attribute.</value>
            public CommandAttribute CommandAttribute
            {
                get { return this.commandAttribute; }
            }

            /// <summary>
            /// Gets the argument definition.
            /// </summary>
            /// <value>The argument definition.</value>
            public ArgumentDefinition ArgumentDefinition
            {
                get { return this.argumentDefinition; }
            }
        }
    }
}
