using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotArguments
{
    /// <summary>
    /// Extension methods for <see cref="argumentDefinition"/> objects.
    /// </summary>
    public static class ArgumentDefinitionExtensions
    {
        /// <summary>
        /// Parse the specified arguments against the definition.
        /// </summary>
        /// <returns>The populated container.</returns>
        /// <param name="definition">The definition.</param>
        /// <param name="arguments">The arguments.</param>
        /// <typeparam name="T">The container type.</typeparam>
        public static T Parse<T>(this ArgumentDefinition definition, string[] arguments)
            where T : new()
        {
            return ArgumentParser<T>.Parse(definition, arguments);
        }

        /// <summary>
        /// Generates a string for console output that explains the usage.
        /// </summary>
        /// <returns>The usage string.</returns>
        /// <param name="definition">The argument definition.</param>
        public static string GenerateUsageString(this ArgumentDefinition definition)
        {
            string executableName = System.AppDomain.CurrentDomain.FriendlyName;
            bool hasPositionalArguments = definition.PositionalArguments.Count > 0;
            bool hasNamedArguments = definition.LongNamedArguments.Count > 0;
            bool hasRemainingArguments = definition.RemainingArguments != null;

            StringBuilder sb = new StringBuilder();

            sb.Append(executableName);

            if (hasNamedArguments)
            {
                sb.Append(" [options]");
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
    }
}
