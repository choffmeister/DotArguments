using DotArguments.Attributes;

namespace DotArguments.Tests.TestContainers
{
    /// <summary>
    /// Example argument container (corrupt).
    /// </summary>
    public class CorruptArgumentContainer8
    {
        /// <summary>
        /// Gets or sets the remaining arguments.
        /// </summary>
        /// <value>The remaining.</value>
        [RemainingArguments]
        public string Remaining { get; set; }
    }
}
