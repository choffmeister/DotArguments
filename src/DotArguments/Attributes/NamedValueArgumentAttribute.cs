using System;

namespace DotArguments.Attributes
{
    /// <summary>
    /// Named value argument attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class NamedValueArgumentAttribute : NamedArgumentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedValueArgumentAttribute"/> class.
        /// </summary>
        /// <param name="longName">The long name.</param>
        /// <param name="shortName">The short name.</param>
        public NamedValueArgumentAttribute(string longName, char shortName)
            : base(longName, shortName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedValueArgumentAttribute"/> class.
        /// </summary>
        /// <param name="longName">The long name.</param>
        public NamedValueArgumentAttribute(string longName)
            : base(longName)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is optional.
        /// </summary>
        /// <value><c>true</c> if this instance is optional; otherwise, <c>false</c>.</value>
        public bool IsOptional { get; set; }
    }
}
