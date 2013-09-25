using System;

namespace DotArguments
{
    /// <summary>
    /// Basic interface for a command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns>The exit code.</returns>
        int Execute();
    }
}
