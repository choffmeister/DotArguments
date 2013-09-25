using System.Reflection;
using DotArguments.Attributes;

namespace DotArguments.Exceptions
{
    /// <summary>
    /// Mandatory argument missing exception.
    /// </summary>
    public sealed class ArgumentFormatException : ArgumentParserException
    {
        private readonly ArgumentAttribute attribute;
        private readonly PropertyInfo property;
        private readonly string stringValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentFormatException"/> class.
        /// </summary>
        /// <param name="argumentParser">The argument parser.</param>
        /// <param name="argument">The argument description.</param>
        /// <param name="stringValue">The incorrect value.</param>
        public ArgumentFormatException(IArgumentParser argumentParser, ArgumentDefinition.ArgumentProperty<NamedArgumentAttribute> argument, string stringValue)
            : base(argumentParser, string.Format("Argument {0} cannot take value {1}", argument.Attribute.LongName, stringValue))
        {
            this.attribute = argument.Attribute;
            this.property = argument.Property;
            this.stringValue = stringValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentFormatException"/> class.
        /// </summary>
        /// <param name="argumentParser">The argument parser.</param>
        /// <param name="argument">The argument description.</param>
        /// <param name="stringValue">The incorrect value.</param>
        public ArgumentFormatException(IArgumentParser argumentParser, ArgumentDefinition.ArgumentProperty<PositionalArgumentAttribute> argument, string stringValue)
            : base(argumentParser, string.Format("Argument {0} cannot take value {1}", argument.Attribute.Name, stringValue))
        {
            this.attribute = argument.Attribute;
            this.property = argument.Property;
            this.stringValue = stringValue;
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

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return this.stringValue; }
        }
    }
}
