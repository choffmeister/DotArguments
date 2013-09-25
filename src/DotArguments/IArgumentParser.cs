using System;

namespace DotArguments
{
    /// <summary>
    /// Basic interface for argument parsers.
    /// </summary>
    public interface IArgumentParser
    {
        /// <summary>
        /// Parses the specified arguments according to the argument definition.
        /// </summary>
        /// <returns>The populated argument container.</returns>
        /// <param name="definition">The argument definition.</param>
        /// <param name="arguments">The arguments.</param>
        object Parse(ArgumentDefinition definition, string[] arguments);

        /// <summary>
        /// Generates a string that explaines which arguments are available.
        /// </summary>
        /// <returns>The usage string.</returns>
        /// <param name="definition">The argument definition.</param>
        string GenerateUsageString(ArgumentDefinition definition);
    }

    /// <summary>
    /// Extension methods for <see cref="IArgumentParser"/> implementations.
    /// </summary>
    public static class ArgumentParserExtensions
    {
        /// <summary>
        /// Parses the specified arguments according to the argument definition.
        /// </summary>
        /// <returns>The populated argument container.</returns>
        /// <param name="parser">The argument parser.</param>
        /// <param name="definition">The argument definition.</param>
        /// <param name="arguments">The arguments.</param>
        /// <typeparam name="T">The argument container type.</typeparam>
        public static T Parse<T>(this IArgumentParser parser, ArgumentDefinition definition, string[] arguments)
        {
            if (typeof(T) != definition.ContainerType)
                throw new Exception("Incompatible container type");

            return (T)parser.Parse(definition, arguments);
        }
    }
}
