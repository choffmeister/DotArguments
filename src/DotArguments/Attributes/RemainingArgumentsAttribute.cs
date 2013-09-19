using System;

namespace DotArguments.Attributes
{
    /// <summary>
    /// Attribute that catches all remaining arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class RemainingArgumentsAttribute : ArgumentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemainingArgumentsAttribute"/> class.
        /// </summary>
        public RemainingArgumentsAttribute()
        {
        }
    }
}
