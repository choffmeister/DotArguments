using System;

namespace DotArguments.Attributes
{
    /// <summary>
    /// Positional value argument attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class PositionalValueArgumentAttribute : PositionalArgumentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PositionalValueArgumentAttribute"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        public PositionalValueArgumentAttribute(int index)
            : base(index)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is optional.
        /// </summary>
        /// <value><c>true</c> if this instance is optional; otherwise, <c>false</c>.</value>
        public bool IsOptional { get; set; }
    }
}
