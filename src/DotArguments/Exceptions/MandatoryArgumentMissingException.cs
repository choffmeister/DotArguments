using System.Reflection;
using DotArguments.Attributes;

namespace DotArguments.Exceptions
{
    /// <summary>
    /// Mandatory argument missing exception.
    /// </summary>
    public sealed class MandatoryArgumentMissingException : ArgumentParserException
    {
        private readonly ArgumentAttribute attribute;
        private readonly PropertyInfo property;

        /// <summary>
        /// Initializes a new instance of the <see cref="MandatoryArgumentMissingException"/> class.
        /// </summary>
        /// <param name="argumentParser">The argument parser.</param>
        /// <param name="argument">The argument description.</param>
        public MandatoryArgumentMissingException(IArgumentParser argumentParser, ArgumentDefinition.ArgumentProperty<NamedArgumentAttribute> argument)
            : base(argumentParser, string.Format("Mandatory argument {0} missing", argument.Attribute.LongName))
        {
            this.attribute = argument.Attribute;
            this.property = argument.Property;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MandatoryArgumentMissingException"/> class.
        /// </summary>
        /// <param name="argumentParser">The argument parser.</param>
        /// <param name="argument">The argument description.</param>
        public MandatoryArgumentMissingException(IArgumentParser argumentParser, ArgumentDefinition.ArgumentProperty<PositionalArgumentAttribute> argument)
            : base(argumentParser, string.Format("Mandatory argument {0} missing", argument.Attribute.Name))
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
