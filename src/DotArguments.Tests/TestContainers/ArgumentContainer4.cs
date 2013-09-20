using DotArguments.Attributes;

namespace DotArguments.Tests.TestContainers
{
    /// <summary>
    /// Example argument container.
    /// </summary>
    public class ArgumentContainer4
    {
        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="DotArguments.Tests.TestContainers.ArgumentContainer4"/> is verbose.
        /// </summary>
        /// <value><c>true</c> if verbose; otherwise, <c>false</c>.</value>
        [NamedSwitchArgument("verbose", 'v')]
        public bool Verbose { get; set; }
    }
}
