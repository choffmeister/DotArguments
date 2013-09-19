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
    }
}
