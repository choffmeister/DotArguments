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
        private readonly string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionalArgumentAttribute"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="name">The name.</param>
        internal PositionalArgumentAttribute(int index, string name)
            : base()
        {
            this.index = index;
            this.name = name;
        }

        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index
        {
            get { return this.index; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is optional.
        /// </summary>
        /// <value><c>true</c> if this instance is optional; otherwise, <c>false</c>.</value>
        public bool IsOptional { get; set; }
    }
}
