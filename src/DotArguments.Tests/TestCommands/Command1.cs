using System;
using DotArguments.Attributes;

namespace DotArguments.Tests.TestCommands
{
    /// <summary>
    /// Demo command.
    /// </summary>
    [Command("cmd1")]
    public class Command1 : ICommand
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [PositionalValueArgument(0, "name")]
        [ArgumentDescription(Short = "your name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>The name.</value>
        [NamedValueArgument("age", 'a', IsOptional = true)]
        [ArgumentDescription(Short = "your age")]
        public int? Age { get; set; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns>The exit code.</returns>
        public int Execute()
        {
            Console.WriteLine("Name: {0}", this.Name);
            Console.WriteLine("Age: {0}", this.Age.HasValue ? this.Age.Value.ToString() : "(null)");

            return 0;
        }
    }
}
