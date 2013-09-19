using System;

namespace DotArguments.Attributes
{
    /// <summary>
    /// Basic attribute for arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public abstract class ArgumentAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentAttribute"/> class.
        /// </summary>
        internal ArgumentAttribute()
        {
        }
    }
}
