using System;

namespace DotArguments.Attributes
{
    /// <summary>
    /// Attribute for textual descriptions of arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class ArgumentDescriptionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDescriptionAttribute"/> class.
        /// </summary>
        public ArgumentDescriptionAttribute()
        {
        }

        /// <summary>
        /// Gets or sets the short description.
        /// </summary>
        /// <value>The short description.</value>
        public string Short { get; set; }

        /// <summary>
        /// Gets or sets the long description.
        /// </summary>
        /// <value>The long description.</value>
        public string Long { get; set; }
    }
}
