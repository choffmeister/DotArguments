using DotArguments.Attributes;

namespace DotArguments.Tests.TestContainers
{
    /// <summary>
    /// Example argument container (corrupt).
    /// </summary>
    public class CorruptArgumentContainer7
    {
        /// <summary>
        /// Gets or sets the remaining arguments.
        /// </summary>
        /// <value>The remaining.</value>
        [RemainingArguments]
        public string[] Remaining { get; set; }

        /// <summary>
        /// Gets or sets the remaining arguments 2.
        /// </summary>
        /// <value>The remaining2.</value>
        [RemainingArguments]
        public string[] Remaining2 { get; set; }
    }
}
