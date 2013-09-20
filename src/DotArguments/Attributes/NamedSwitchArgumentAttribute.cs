using System;

namespace DotArguments.Attributes
{
    /// <summary>
    /// Named switch argument attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class NamedSwitchArgumentAttribute : NamedArgumentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedSwitchArgumentAttribute"/> class.
        /// </summary>
        /// <param name="longName">The long name.</param>
        /// <param name="shortName">The short name.</param>
        public NamedSwitchArgumentAttribute(string longName, char shortName)
            : base(longName, shortName)
        {
        }
    }
}
