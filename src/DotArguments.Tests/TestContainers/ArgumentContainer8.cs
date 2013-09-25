using System;
using DotArguments.Attributes;

namespace DotArguments.Tests.TestContainers
{
    /// <summary>
    /// Example argument container.
    /// </summary>
    public class ArgumentContainer8
    {
        /// <summary>
        /// Gets or sets a value indicating whether a.
        /// </summary>
        /// <value><c>true</c> if a; otherwise, <c>false</c>.</value>
        [NamedSwitchArgument("switch-a", 'a')]
        public bool A { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether b.
        /// </summary>
        /// <value><c>true</c> if b; otherwise, <c>false</c>.</value>
        [NamedSwitchArgument("switch-b", 'b')]
        public bool B { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether c.
        /// </summary>
        /// <value><c>true</c> if c; otherwise, <c>false</c>.</value>
        [NamedSwitchArgument("switch-c", 'c')]
        public bool C { get; set; }
    }
}
