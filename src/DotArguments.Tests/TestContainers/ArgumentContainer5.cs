using System;
using DotArguments.Attributes;

namespace DotArguments.Tests.TestContainers
{
    /// <summary>
    /// Example argument container.
    /// </summary>
    public class ArgumentContainer5
    {
        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="DotArguments.Tests.TestContainers.ArgumentContainer4"/> is verbose.
        /// </summary>
        /// <value><c>true</c> if verbose; otherwise, <c>false</c>.</value>
        [NamedSwitchArgument("verbose", 'v')]
        public bool Verbose { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [NamedValueArgument("name")]
        public string Name { get; set; }
    }
}
