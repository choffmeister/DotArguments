using System.Reflection;
using DotArguments.Attributes;

namespace DotArguments.Exceptions
{
    /// <summary>
    /// Too many positional arguments exception.
    /// </summary>
    public sealed class TooManyPositionalArgumentsException : ArgumentParserException
    {
        private readonly string[] additionalArguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyPositionalArgumentsException"/> class.
        /// </summary>
        /// <param name="additionalArguments">Additional arguments.</param>
        public TooManyPositionalArgumentsException(string[] additionalArguments)
            : base("Too many positional arguments")
        {
            this.additionalArguments = additionalArguments;
        }

        /// <summary>
        /// Gets the additional arguments.
        /// </summary>
        /// <value>The additional arguments.</value>
        public string[] AdditionalArguments
        {
            get { return this.additionalArguments; }
        }
    }
}
