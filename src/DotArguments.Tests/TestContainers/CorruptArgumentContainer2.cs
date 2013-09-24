using DotArguments.Attributes;

namespace DotArguments.Tests.TestContainers
{
    /// <summary>
    /// Example argument container (corrupt).
    /// </summary>
    public class CorruptArgumentContainer2
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [PositionalValueArgumentAttribute(0, "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>The name.</value>
        [PositionalValueArgumentAttribute(0, "age")]
        public int Age { get; set; }
    }
}
