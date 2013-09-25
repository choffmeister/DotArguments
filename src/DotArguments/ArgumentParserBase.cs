using System;
using DotArguments.Exceptions;

namespace DotArguments
{
    /// <summary>
    /// Base implementation for <see cref="IArgumentParser"/>.
    /// </summary>
    public abstract class ArgumentParserBase : IArgumentParser
    {
        /// <summary>
        /// Parses the specified arguments according to the argument definition.
        /// </summary>
        /// <returns>The populated argument container.</returns>
        /// <param name="definition">The argument definition.</param>
        /// <param name="arguments">The arguments.</param>
        public object Parse(ArgumentDefinition definition, string[] arguments)
        {
            if (definition == null)
                throw new ArgumentNullException("argumentDefinition");
            if (arguments == null)
                throw new ArgumentNullException("arguments");

            // ensure that any exception is wrapped into an ArgumentParserException
            try
            {
                object container = Activator.CreateInstance(definition.ContainerType);

                this.PopulateArguments(definition, container, arguments);

                return container;
            }
            catch (ArgumentParserException ex)
            {
                // just rethrow
                throw ex;
            }
            catch (Exception ex)
            {
                // ensure exception type
                throw new ArgumentParserException("Unknown error while parsing", ex);
            }
        }

        /// <summary>
        /// Populates the arguments into the container.
        /// </summary>
        /// <param name="definition">The argument definition.</param>
        /// <param name="container">The argument container.</param>
        /// <param name="arguments">The arguments.</param>
        public abstract void PopulateArguments(ArgumentDefinition definition, object container, string[] arguments);

        /// <summary>
        /// Generates a string that explaines which arguments are available.
        /// </summary>
        /// <returns>The usage string.</returns>
        /// <param name="definition">The argument definition.</param>
        public abstract string GenerateUsageString(ArgumentDefinition definition);
    }
}
