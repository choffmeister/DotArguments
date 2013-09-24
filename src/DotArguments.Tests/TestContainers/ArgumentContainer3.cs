using DotArguments.Attributes;

namespace DotArguments.Tests.TestContainers
{
    /// <summary>
    /// Example argument container.
    /// </summary>
    public class ArgumentContainer3
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [PositionalValueArgumentAttribute(0, "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the remaining arguments.
        /// </summary>
        /// <value>The remaining arguments.</value>
        [RemainingArguments]
        public string[] Remaining { get; set; }
    }
}
