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
        public NamedValueArgumentAttribute(string longName, string shortName)
            : base(longName, shortName)
        {
        }
    }
}
