using DotArguments.Attributes;

namespace DotArguments.Tests.TestContainers
{
    /// <summary>
    /// Example argument container (corrupt).
    /// </summary>
    public class CorruptArgumentContainer9
    {
        /// <summary>
        /// Gets or sets the remaining arguments.
        /// </summary>
        /// <value>The remaining.</value>
        [NamedSwitchArgument("verbose", 'v')]
        public string Verbose { get; set; }
    }
}
