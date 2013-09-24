using System;
using System.Linq;
using System.Text;

namespace DotArguments
{
    /// <summary>
    /// Extension methods for <see cref="ContainerDefinition"/> objects.
    /// </summary>
    public static class ContainerDefinitionExtensions
    {
        /// <summary>
        /// Generates a string for console output that explains the usage.
        /// </summary>
        /// <returns>The usage string.</returns>
        /// <param name="definition">The container definition.</param>
        /// <param name="executableName">The name of the executable.</param>
        public static string GenerateUsageString(this ContainerDefinition definition, string executableName)
        {
            bool hasPositionalArguments = definition.PositionalArguments.Count > 0;
            bool hasNamedArguments = definition.LongNamedArguments.Count > 0;

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
