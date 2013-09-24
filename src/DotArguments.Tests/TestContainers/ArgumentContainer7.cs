using System;
using DotArguments.Attributes;

namespace DotArguments.Tests.TestContainers
{
    /// <summary>
    /// Example argument container.
    /// </summary>
    public class ArgumentContainer7
    {
        /// <summary>
        /// Gets or sets the name1.
        /// </summary>
        /// <value>The name1.</value>
        [NamedValueArgument("name1")]
        [ArgumentDescription(Short = "desc-short-1", Long = "desc-long-1")]
        public string Name1 { get; set; }

        /// <summary>
        /// Gets or sets the name2.
        /// </summary>
        /// <value>The name2.</value>
        [NamedValueArgument("name2")]
        [ArgumentDescription(Short = "desc-short-2", Long = "desc-long-2")]
        public string Name2 { get; set; }

        /// <summary>
        /// Gets or sets the name3.
        /// </summary>
        /// <value>The name3.</value>
        [NamedValueArgument("name3")]
        public string Name3 { get; set; }
    }
}
