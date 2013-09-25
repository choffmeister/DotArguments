using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using DotArguments;
using DotArguments.Attributes;

namespace DotArguments
{
    /// <summary>
    /// Parser for command line arguments.
    /// </summary>
    public class GNUArgumentParser : IArgumentParser
    {
        /// <summary>
        /// Parse the specified arguments and returns a new argument container
        /// object.
        /// </summary>
        /// <returns>The argument container.</returns>
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

                ConsumeArguments(definition, container, arguments);

                return container;
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
        }

        /// <summary>
        /// Generates a string that explaines which arguments are available.
        /// </summary>
        /// <returns>The usage string.</returns>
        /// <param name="definition">The argument definition.</param>
        public string GenerateUsageString(ArgumentDefinition definition)
        {
            string executableName = System.AppDomain.CurrentDomain.FriendlyName;
            bool hasPositionalArguments = definition.PositionalArguments.Count > 0;
            bool hasNamedArguments = definition.LongNamedArguments.Count > 0;
            bool hasRemainingArguments = definition.RemainingArguments != null;

            StringBuilder sb = new StringBuilder();

            sb.Append(executableName);

            if (hasNamedArguments)
            {
                sb.Append(" [options] [--]");
            }

            foreach (var arg in definition.PositionalArguments.OrderBy(n => n.Key).Select(n => n.Value))
            {
                var attr = arg.Attribute;

                if (!attr.IsOptional)
                {
                    sb.Append(string.Format(" {0}", attr.Name));
                }
                else
                {
                    sb.Append(string.Format(" [{0}]", attr.Name));
                }
            }

            if (hasRemainingArguments)
            {
                sb.Append(" [...]");
            }

            sb.AppendLine();

            if (hasPositionalArguments)
            {
                sb.AppendLine();

                foreach (var arg in definition.PositionalArguments.OrderBy(n => n.Key).Select(n => n.Value))
                {
                    var attr = arg.Attribute;
                    var desc = arg.DescriptionAttribute;

                    sb.AppendLine(string.Format("  {0,-16}   {1}", attr.Name, desc != null && desc.Short != null ? desc.Short : string.Empty));
                }
            }

            if (hasNamedArguments)
            {
                sb.AppendLine();

                foreach (var arg in definition.LongNamedArguments.OrderBy(n => n.Key).Select(n => n.Value))
                {
                    var attr = arg.Attribute;
                    var desc = arg.DescriptionAttribute;

                    if (attr.ShortName.HasValue)
                    {
                        sb.AppendLine(string.Format("  -{0}, --{1,-10}   {2}", attr.ShortName, attr.LongName, desc != null && desc.Short != null ? desc.Short : string.Empty));
                    }
                    else
                    {
                        sb.AppendLine(string.Format("  --{0,-14}   {1}", attr.LongName, desc != null && desc.Short != null ? desc.Short : string.Empty));
                    }
                }
            }

            return sb.ToString();
        }

        private static void ConsumeArguments(ArgumentDefinition definition, object container, string[] arguments)
        {
            bool hadDoubleDash = false;
            var foundNamedArguments = new List<ArgumentDefinition.ArgumentProperty<NamedArgumentAttribute>>();
            var foundPositionArguments = new List<ArgumentDefinition.ArgumentProperty<PositionalArgumentAttribute>>();

            int currentPositionalIndex = 0;
            ArgumentDefinition.ArgumentProperty<NamedArgumentAttribute> currentNamedArgument = null;
            List<string> remainingArguments = new List<string>();

            for (int i = 0; i < arguments.Length; i++)
            {
                string currentArgument = arguments[i];

                if (currentArgument == "--" && !hadDoubleDash)
                {
                    hadDoubleDash = true;
                }
                else if (currentNamedArgument == null)
                {
                    if (currentArgument.StartsWith("--", StringComparison.InvariantCulture) && !hadDoubleDash)
                    {
                        string[] parts = currentArgument.Substring(2).Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);

                        // a new long named argument
                        string longName = parts[0];
                        currentNamedArgument = definition.LongNamedArguments[longName];

                        if (currentNamedArgument.Attribute.GetType() == typeof(NamedValueArgumentAttribute))
                        {
                            if (parts.Length == 2)
                            {
                                string value = parts[1];

                                SetValue(currentNamedArgument.Property, container, value);

                                foundNamedArguments.Add(currentNamedArgument);
                                currentNamedArgument = null;
                            }
                            else
                            {
                                throw new ArgumentParserException("Present long named value arguments must have a value");
                            }
                        }
                        else if (currentNamedArgument.Attribute.GetType() == typeof(NamedSwitchArgumentAttribute))
                        {
                            // a named switch
                            currentNamedArgument.Property.SetValue(container, true, new object[0]);

                            foundNamedArguments.Add(currentNamedArgument);
                            currentNamedArgument = null;
                        }
                    }
                    else if (currentArgument.StartsWith("-", StringComparison.InvariantCulture) && !hadDoubleDash)
                    {
                        char shortName = currentArgument[1];
                        currentNamedArgument = definition.ShortNamedArguments[shortName];

                        if (currentNamedArgument.Attribute.GetType() == typeof(NamedValueArgumentAttribute))
                        {
                            if (currentArgument.Length == 2)
                            {
                                currentNamedArgument = definition.ShortNamedArguments[shortName];
                            }
                            else
                            {
                                string value = currentArgument.Substring(2);

                                SetValue(currentNamedArgument.Property, container, value);

                                foundNamedArguments.Add(currentNamedArgument);
                                currentNamedArgument = null;
                            }
                        }
                        else if (currentNamedArgument.Attribute.GetType() == typeof(NamedSwitchArgumentAttribute))
                        {
                            for (int j = 1; j < currentArgument.Length; j++)
                            {
                                shortName = currentArgument[j];
                                currentNamedArgument = definition.ShortNamedArguments[shortName];

                                currentNamedArgument.Property.SetValue(container, true, new object[0]);

                                foundNamedArguments.Add(currentNamedArgument);
                                currentNamedArgument = null;
                            }
                        }
                    }
                    else
                    {
                        if (currentPositionalIndex < definition.PositionalArguments.Count)
                        {
                            // a positional argument
                            ArgumentDefinition.ArgumentProperty<PositionalArgumentAttribute> currentPositionalArgument = definition.PositionalArguments[currentPositionalIndex];
                            SetValue(currentPositionalArgument.Property, container, currentArgument);

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
                    SetValue(currentNamedArgument.Property, container, currentArgument);

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
        /// Converts and sets a string value to a property.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="stringValue">The string value.</param>
        private static void SetValue(PropertyInfo propertyInfo, object instance, string stringValue)
        {
            Type type = propertyInfo.PropertyType;
            type = Nullable.GetUnderlyingType(type) ?? type;

            object value = stringValue != null ? 
                Convert.ChangeType(stringValue, type, CultureInfo.InvariantCulture) :
                GetDefaultValue(type);

            propertyInfo.SetValue(instance, value, new object[0]);
        }

        /// <summary>
        /// Gets the default value of a type.
        /// </summary>
        /// <returns>The default value.</returns>
        /// <param name="type">The type.</param>
        private static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
