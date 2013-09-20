using DotArguments.Attributes;

namespace DotArguments.Tests.TestContainers
{
    /// <summary>
    /// Example argument container (corrupt).
    /// </summary>
    public class CorruptArgumentContainer11
    {
        /// <summary>
        /// Gets or sets the remaining arguments.
        /// </summary>
        /// <value>The remaining.</value>
        [NamedSwitchArgument("voo-", 'v')]
        public string Verbose { get; set; }
    }
}
