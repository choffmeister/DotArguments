using System;

namespace DotArguments.Attributes
{
    /// <summary>
    /// Basic attribute for positional arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public abstract class PositionalArgumentAttribute : ArgumentAttribute
    {
        private readonly int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionalArgumentAttribute"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        protected PositionalArgumentAttribute(int index)
            : base()
        {
            this.index = index;
        }

        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index
        {
            get { return this.index; }
        }
    }
}
