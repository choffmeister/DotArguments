using System;
using System.Collections.Generic;
using System.Globalization;
using DotArguments;
using DotArguments.Attributes;

namespace DotArguments
{
    /// <summary>
    /// Parser for command line arguments.
    /// </summary>
    /// <typeparam name="T">The type of the argument container.</typeparam>
    public static class ArgumentParser<T>
        where T : new()
    {
        /// <summary>
        /// Parse the specified arguments and returns a new argument container
        /// object.
        /// </summary>
        /// <returns>The argument container.</returns>
        /// <param name="arguments">The arguments.</param>
        public static T Parse(string[] arguments)
        {
            if (arguments == null)
                throw new ArgumentNullException("arguments");

            ContainerDefinition definition = new ContainerDefinition(typeof(T));
            T container = new T();

            // ensure that any exception is wrapped into an ArgumentParserException
            try
            {
                ConsumeArguments(definition, container, arguments);
            }
            catch (ArgumentParserException ex)
            {
                // do not wrap
                throw ex;
            }
            catch (Exception ex)
            {
                // wrap
                throw new ArgumentParserException("Error while parsing", ex);
            }

            return container;
        }

        private static void ConsumeArguments(ContainerDefinition definition, T container, string[] arguments)
        {
            var foundNamedArguments = new List<ContainerDefinition.ArgumentProperty<NamedArgumentAttribute>>();
            var foundPositionArguments = new List<ContainerDefinition.ArgumentProperty<PositionalArgumentAttribute>>();

            int currentPositionalIndex = 0;
            ContainerDefinition.ArgumentProperty<NamedArgumentAttribute> currentNamedArgument = null;
            List<string> remainingArguments = new List<string>();

            for (int i = 0; i < arguments.Length; i++)
            {
                string currentArgument = arguments[i];

                if (currentNamedArgument == null)
                {
                    if (currentArgument.StartsWith("--", StringComparison.InvariantCulture))
                    {
                        // a new long named argument
                        string longName = currentArgument.Substring(2);
                        currentNamedArgument = definition.LongNamedArguments[longName];

                        if (currentNamedArgument.Attribute.GetType() == typeof(NamedSwitchArgumentAttribute))
                        {
                            // a named switch
                            currentNamedArgument.Property.SetValue(container, true, new object[0]);

                            foundNamedArguments.Add(currentNamedArgument);
                            currentNamedArgument = null;
                        }
                    }
                    else if (currentArgument.StartsWith("-", StringComparison.InvariantCulture))
                    {
                        if (currentArgument.Length == 2)
                        {
                            // a new short named argument
                            char shortName = currentArgument[1];
                            currentNamedArgument = definition.ShortNamedArguments[shortName];

                            if (currentNamedArgument.Attribute.GetType() == typeof(NamedSwitchArgumentAttribute))
                            {
                                // a named switch
                                currentNamedArgument.Property.SetValue(container, true, new object[0]);

                                foundNamedArguments.Add(currentNamedArgument);
                                currentNamedArgument = null;
                            }
                        }
                        else
                        {
                            throw new ArgumentParserException("Short named arguments must contain out of one character");
                        }
                    }
                    else
                    {
                        if (currentPositionalIndex < definition.PositionalArguments.Count)
                        {
                            // a positional argument
                            ContainerDefinition.ArgumentProperty<PositionalArgumentAttribute> currentPositionalArgument = definition.PositionalArguments[currentPositionalIndex];
                            currentPositionalArgument.Property.SetValue(container, ConvertValue(currentPositionalArgument.Property.PropertyType, currentArgument), new object[0]);

                            foundPositionArguments.Add(currentPositionalArgument);
                            currentPositionalIndex++;
                        }
                        else
                        {
                            // a remaining argument
                            remainingArguments.Add(currentArgument);
                        }
                    }
                }
                else
                {
                    currentNamedArgument.Property.SetValue(container, ConvertValue(currentNamedArgument.Property.PropertyType, currentArgument), new object[0]);

                    foundNamedArguments.Add(currentNamedArgument);
                    currentNamedArgument = null;
                }
            }

            if (definition.RemainingArguments != null)
            {
                definition.RemainingArguments.Property.SetValue(container, remainingArguments.ToArray(), new object[0]);
            }
            else if (remainingArguments.Count > 0)
            {
                throw new ArgumentParserException("Too many positional arguments");
            }

            // check if all non optional arguments were present
            foreach (var namedArgument in definition.LongNamedArguments.Values)
            {
                if (!foundNamedArguments.Contains(namedArgument))
                {
                    if (namedArgument.Attribute is NamedValueArgumentAttribute)
                    {
                        NamedValueArgumentAttribute castedAttribute = namedArgument.Attribute as NamedValueArgumentAttribute;

                        if (!castedAttribute.IsOptional)
                        {
                            throw new ArgumentParserException(string.Format("Mandatory argument {0} is missing", namedArgument.Attribute.LongName));
                        }
                    }
                }
            }

            foreach (var positionalArgument in definition.PositionalArguments.Values)
            {
                if (!foundPositionArguments.Contains(positionalArgument))
                {
                    if (positionalArgument.Attribute is PositionalValueArgumentAttribute)
                    {
                        PositionalValueArgumentAttribute castedAttribute = positionalArgument.Attribute as PositionalValueArgumentAttribute;

                        if (!castedAttribute.IsOptional)
                        {
                            throw new ArgumentParserException(string.Format("Mandatory argument at position {0} is missing", positionalArgument.Attribute.Index));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Converts a value into the desired target type with invarian culture formatter.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="targetType">Target type.</param>
        /// <param name="source">The source value.</param>
        private static object ConvertValue(Type targetType, string source)
        {
            return Convert.ChangeType(source, targetType, CultureInfo.InvariantCulture);
        }
    }
}
