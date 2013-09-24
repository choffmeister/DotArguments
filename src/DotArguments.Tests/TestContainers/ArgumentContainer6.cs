using System;
using DotArguments.Attributes;

namespace DotArguments.Tests.TestContainers
{
    /// <summary>
    /// Example argument container.
    /// </summary>
    public class ArgumentContainer6
    {
        /// <summary>
        /// Gets or sets the name1.
        /// </summary>
        /// <value>The name1.</value>
        [NamedValueArgument("name1")]
        public string Name1 { get; set; }

        /// <summary>
        /// Gets or sets the name2.
        /// </summary>
        /// <value>The name2.</value>
        [NamedValueArgument("name2", IsOptional = true)]
        public string Name2 { get; set; }

        /// <summary>
        /// Gets or sets the age1.
        /// </summary>
        /// <value>The age1.</value>
        [PositionalValueArgument(0, "age")]
        public int Age1 { get; set; }

        /// <summary>
        /// Gets or sets the age2.
        /// </summary>
        /// <value>The age2.</value>
        [PositionalValueArgument(1, "age2", IsOptional = true)]
        public int? Age2 { get; set; }
    }
}
