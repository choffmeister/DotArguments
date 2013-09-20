using System;

namespace DotArguments.Attributes
{
    /// <summary>
    /// Basic attribute for named arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public abstract class NamedArgumentAttribute : ArgumentAttribute
    {
        private readonly string longName;
        private readonly char? shortName;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedArgumentAttribute"/> class.
        /// </summary>
        /// <param name="longName">The long name.</param>
        /// <param name="shortName">The short name.</param>
        internal NamedArgumentAttribute(string longName, char shortName)
            : base()
        {
            this.longName = longName;
            this.shortName = shortName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedArgumentAttribute"/> class.
        /// </summary>
        /// <param name="longName">The long name.</param>
        internal NamedArgumentAttribute(string longName)
            : base()
        {
            this.longName = longName;
        }

        /// <summary>
        /// Gets the long name.
        /// </summary>
        /// <value>The long name.</value>
        public string LongName
        {
            get { return this.longName; }
        }

        /// <summary>
        /// Gets the short name.
        /// </summary>
        /// <value>The short name.</value>
        public char? ShortName
        {
            get { return this.shortName; }
        }
    }
}
