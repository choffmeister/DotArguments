using DotArguments.Attributes;

namespace DotArguments.Tests.TestContainers
{
    /// <summary>
    /// Example argument container (corrupt).
    /// </summary>
    public class CorruptArgumentContainer1
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [PositionalValueArgumentAttribute(0, "age")]
        [NamedValueArgumentAttribute("age", 'a')]
        public string Name { get; set; }
    }
}
