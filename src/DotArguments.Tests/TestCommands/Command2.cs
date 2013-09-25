using System;
using DotArguments.Attributes;

namespace DotArguments.Tests.TestCommands
{
    /// <summary>
    /// Demo command 2.
    /// </summary>
    [Command("cmd2")]
    public class Command2 : ICommand
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [PositionalValueArgument(0, "path")]
        [ArgumentDescription(Short = "the file path")]
        public string Path { get; set; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns>The exit code.</returns>
        public int Execute()
        {
            Console.WriteLine("Path: {0}", this.Path);

            return 0;
        }
    }
}
