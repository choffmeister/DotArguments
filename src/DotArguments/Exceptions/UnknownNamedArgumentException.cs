using System.Reflection;
using DotArguments.Attributes;

namespace DotArguments.Exceptions
{
    /// <summary>
    /// Unknown named argument exception.
    /// </summary>
    public sealed class UnknownNamedArgumentException : ArgumentParserException
    {
        private readonly string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownNamedArgumentException"/> class.
        /// </summary>
        /// <param name="name">The unknown name.</param>
        public UnknownNamedArgumentException(string name)
            : base(string.Format("Named argument {0} is unknown", name))
        {
            this.name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return this.name; }
        }
    }
}
