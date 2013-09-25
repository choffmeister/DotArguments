using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using DotArguments.Attributes;
using DotArguments.Exceptions;

namespace DotArguments
{
    /// <summary>
    /// Base implementation for <see cref="IArgumentParser"/>.
    /// </summary>
    public abstract class ArgumentParserBase : IArgumentParser
    {
        /// <summary>
        /// Parses the specified arguments according to the argument definition.
        /// </summary>
        /// <returns>The populated argument container.</returns>
        /// <param name="definition">The argument definition.</param>
        /// <param name="arguments">The arguments.</param>
        public object Parse(ArgumentDefinition definition, string[] arguments)
        {
            if (definition == null)
                throw new ArgumentNullException("argumentDefinition");
            if (arguments == null)
                throw new ArgumentNullException("arguments");

            // ensure that any exception is wrapped into an ArgumentParserException
            try
            {
                object container = Activator.CreateInstance(definition.ContainerType);

                this.PopulateArguments(definition, container, arguments);

                return container;
            }
            catch (ArgumentParserException ex)
            {
                // just rethrow
                throw ex;
            }
            catch (Exception ex)
            {
                // ensure exception type
                throw new ArgumentParserException("Unknown error while parsing", ex);
            }
        }

        /// <summary>
        /// Populates the arguments into the container.
        /// </summary>
        /// <param name="definition">The argument definition.</param>
        /// <param name="container">The argument container.</param>
        /// <param name="arguments">The arguments.</param>
        public abstract void PopulateArguments(ArgumentDefinition definition, object container, string[] arguments);

        /// <summary>
        /// Generates a string that explaines which arguments are available.
        /// </summary>
        /// <returns>The usage string.</returns>
        /// <param name="definition">The argument definition.</param>
        public abstract string GenerateUsageString(ArgumentDefinition definition);

        /// <summary>
        /// Ensures that all mandatory arguments are present. This method throws appropriate
        /// exceptions if one is missing.
        /// </summary>
        /// <param name="definition">The argument definition.</param>
        /// <param name="usedNamedArguments">The used named arguments.</param>
        /// <param name="usedPositionalArguments">The used positional arguments.</param>
        protected static void EnsureAllMandatoryArgumentsArePresent(
            ArgumentDefinition definition,
            ICollection<ArgumentDefinition.ArgumentProperty<NamedArgumentAttribute>> usedNamedArguments,
            ICollection<ArgumentDefinition.ArgumentProperty<PositionalArgumentAttribute>> usedPositionalArguments)
        {
            foreach (var namedArgument in definition.LongNamedArguments.Values)
            {
                if (!usedNamedArguments.Contains(namedArgument))
                {
                    if (namedArgument.Attribute is NamedValueArgumentAttribute)
                    {
                        NamedValueArgumentAttribute castedAttribute = namedArgument.Attribute as NamedValueArgumentAttribute;

                        if (!castedAttribute.IsOptional)
                        {
                            throw new MandatoryArgumentMissingException(namedArgument);
                        }
                    }
                }
            }

            foreach (var positionalArgument in definition.PositionalArguments.Values)
            {
                if (!usedPositionalArguments.Contains(positionalArgument))
                {
                    if (positionalArgument.Attribute is PositionalValueArgumentAttribute)
                    {
                        PositionalValueArgumentAttribute castedAttribute = positionalArgument.Attribute as PositionalValueArgumentAttribute;

                        if (!castedAttribute.IsOptional)
                        {
                            throw new MandatoryArgumentMissingException(positionalArgument);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Converts and sets a string value to a property.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="stringValue">The string value.</param>
        protected static void SetValue(PropertyInfo propertyInfo, object instance, string stringValue)
        {
            Type type = propertyInfo.PropertyType;
            type = Nullable.GetUnderlyingType(type) ?? type;

            object value = stringValue != null ? 
                Convert.ChangeType(stringValue, type, CultureInfo.InvariantCulture) :
                    GetDefaultValue(type);

            propertyInfo.SetValue(instance, value, new object[0]);
        }

        /// <summary>
        /// Gets the default value of a type.
        /// </summary>
        /// <returns>The default value.</returns>
        /// <param name="type">The type.</param>
        protected static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
