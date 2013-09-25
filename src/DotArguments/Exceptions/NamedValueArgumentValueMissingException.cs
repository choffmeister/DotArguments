using System.Reflection;
using DotArguments.Attributes;

namespace DotArguments.Exceptions
{
    /// <summary>
    /// Named value argument value missing.
    /// </summary>
    public sealed class NamedValueArgumentValueMissingException : ArgumentParserException
    {
        private readonly ArgumentAttribute attribute;
        private readonly PropertyInfo property;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedValueArgumentValueMissingException"/> class.
        /// </summary>
        /// <param name="argumentParser">The argument parser.</param>
        /// <param name="argument">The argument description.</param>
        public NamedValueArgumentValueMissingException(IArgumentParser argumentParser, ArgumentDefinition.ArgumentProperty<NamedArgumentAttribute> argument)
            : base(argumentParser, string.Format("Value of argument {0} missing", argument.Attribute.LongName))
        {
            this.attribute = argument.Attribute;
            this.property = argument.Property;
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <value>The attribute.</value>
        public ArgumentAttribute Attribute
        {
            get { return this.attribute; }
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <value>The property.</value>
        public PropertyInfo Property
        {
            get { return this.property; }
        }
    }
}
