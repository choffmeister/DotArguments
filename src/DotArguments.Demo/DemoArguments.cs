using DotArguments.Attributes;

namespace DotArgumentsDemo
{
    /// <summary>
    /// Container for the demo arguments.
    /// </summary>
    public class DemoArguments
    {
        /// <summary>
        /// Gets or sets the input path.
        /// </summary>
        /// <value>The input path.</value>
        [PositionalValueArgument(0, "inputpath")]
        [ArgumentDescription(Short = "the input path")]
        public string InputPath { get; set; }

        /// <summary>
        /// Gets or sets the output path.
        /// </summary>
        /// <value>The output path.</value>
        [PositionalValueArgument(1, "outputpath", IsOptional = true)]
        [ArgumentDescription(Short = "the output path")]
        public string OutputPath { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [NamedValueArgument("name", 'n', IsOptional = true)]
        [ArgumentDescription(Short = "the name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>The age.</value>
        [NamedValueArgument("age", IsOptional = true)]
        [ArgumentDescription(Short = "the age")]
        public int? Age { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DotArgumentsDemo.DemoArguments"/> is verbose.
        /// </summary>
        /// <value><c>true</c> if verbose; otherwise, <c>false</c>.</value>
        [NamedSwitchArgument("verbose", 'v')]
        [ArgumentDescription(Short = "enable verbose console output")]
        public bool Verbose { get; set; }

        /// <summary>
        /// Gets or sets the remaining arguments.
        /// </summary>
        /// <value>The remaining arguments.</value>
        [RemainingArguments]
        public string[] RemainingArguments { get; set; }
    }
}
