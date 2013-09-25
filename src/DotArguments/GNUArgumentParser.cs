using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotArguments;
using DotArguments.Attributes;
using DotArguments.Exceptions;

namespace DotArguments
{
    /// <summary>
    /// Parser for command line arguments.
    /// </summary>
    public class GNUArgumentParser : ArgumentParserBase
    {
        /// <summary>
        /// Populates the arguments into the container.
        /// </summary>
        /// <param name="definition">The argument definition.</param>
        /// <param name="container">The argument container.</param>
        /// <param name="arguments">The arguments.</param>
        public override void PopulateArguments(ArgumentDefinition definition, object container, string[] arguments)
        {
            bool hadDoubleDash = false;
            var usedNamedArguments = new List<ArgumentDefinition.ArgumentProperty<NamedArgumentAttribute>>();
            var usedPositionalArguments = new List<ArgumentDefinition.ArgumentProperty<PositionalArgumentAttribute>>();

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
                        currentNamedArgument = this.GetLongNamedArgument(definition, longName);

                        if (currentNamedArgument.Attribute.GetType() == typeof(NamedValueArgumentAttribute))
                        {
                            if (parts.Length == 2)
                            {
                                string value = parts[1];

                                this.SetValue(currentNamedArgument, container, value);

                                usedNamedArguments.Add(currentNamedArgument);
                                currentNamedArgument = null;
                            }
                            else
                            {
                                throw new NamedValueArgumentValueMissingException(this, currentNamedArgument);
                            }
                        }
                        else if (currentNamedArgument.Attribute.GetType() == typeof(NamedSwitchArgumentAttribute))
                        {
                            // a named switch
                            currentNamedArgument.Property.SetValue(container, true, new object[0]);

                            usedNamedArguments.Add(currentNamedArgument);
                            currentNamedArgument = null;
                        }
                    }
                    else if (currentArgument.StartsWith("-", StringComparison.InvariantCulture) && !hadDoubleDash)
                    {
                        char shortName = currentArgument[1];
                        currentNamedArgument = this.GetShortNamedArgument(definition, shortName);

                        if (currentNamedArgument.Attribute.GetType() == typeof(NamedValueArgumentAttribute))
                        {
                            if (currentArgument.Length == 2)
                            {
                                currentNamedArgument = this.GetShortNamedArgument(definition, shortName);
                            }
                            else
                            {
                                string value = currentArgument.Substring(2);

                                this.SetValue(currentNamedArgument, container, value);

                                usedNamedArguments.Add(currentNamedArgument);
                                currentNamedArgument = null;
                            }
                        }
                        else if (currentNamedArgument.Attribute.GetType() == typeof(NamedSwitchArgumentAttribute))
                        {
                            for (int j = 1; j < currentArgument.Length; j++)
                            {
                                shortName = currentArgument[j];
                                currentNamedArgument = this.GetShortNamedArgument(definition, shortName);

                                currentNamedArgument.Property.SetValue(container, true, new object[0]);

                                usedNamedArguments.Add(currentNamedArgument);
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
                            this.SetValue(currentPositionalArgument, container, currentArgument);

                            usedPositionalArguments.Add(currentPositionalArgument);
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
                    this.SetValue(currentNamedArgument, container, currentArgument);

                    usedNamedArguments.Add(currentNamedArgument);
                    currentNamedArgument = null;
                }
            }

            if (definition.RemainingArguments != null)
            {
                definition.RemainingArguments.Property.SetValue(container, remainingArguments.ToArray(), new object[0]);
            }
            else if (remainingArguments.Count > 0)
            {
                throw new TooManyPositionalArgumentsException(this, remainingArguments.ToArray());
            }

            if (currentNamedArgument != null)
            {
                throw new NamedValueArgumentValueMissingException(this, currentNamedArgument);
            }

            this.EnsureAllMandatoryArgumentsArePresent(definition, usedNamedArguments, usedPositionalArguments);
        }

        /// <summary>
        /// Generates a string that explaines which arguments are available.
        /// </summary>
        /// <returns>The usage string.</returns>
        /// <param name="definition">The argument definition.</param>
        public override string GenerateUsageString(ArgumentDefinition definition)
        {
            bool hasPositionalArguments = definition.PositionalArguments.Count > 0;
            bool hasNamedArguments = definition.LongNamedArguments.Count > 0;
            bool hasRemainingArguments = definition.RemainingArguments != null;

            StringBuilder sb = new StringBuilder();

            if (hasNamedArguments)
            {
                sb.Append(" [options]");
            }

            if (hasPositionalArguments)
            {
                sb.Append(" [--]");
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

        private void SetValue(ArgumentDefinition.ArgumentProperty<NamedArgumentAttribute> argument, object container, string stringValue)
        {
            try
            {
                ArgumentParserBase.SetValue(argument.Property, container, stringValue);
            }
            catch (FormatException)
            {
                throw new ArgumentFormatException(this, argument, stringValue);
            }
        }

        private void SetValue(ArgumentDefinition.ArgumentProperty<PositionalArgumentAttribute> argument, object container, string stringValue)
        {
            try
            {
                ArgumentParserBase.SetValue(argument.Property, container, stringValue);
            }
            catch (FormatException)
            {
                throw new ArgumentFormatException(this, argument, stringValue);
            }
        }
    }
}
