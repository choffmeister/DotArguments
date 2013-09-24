using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DotArguments.Attributes;

namespace DotArguments
{
    /// <summary>
    /// Definition for a container for command line arguments.
    /// </summary>
    public class ArgumentDefinition
    {
        private static readonly Regex LongNameRegex = new Regex(@"^[a-zA-Z]([\-|_]?[a-zA-Z0-9]+)*$", RegexOptions.Compiled);
        private readonly Type containerType;
        private readonly Dictionary<int, ArgumentProperty<PositionalArgumentAttribute>> positionalArguments;
        private readonly Dictionary<string, ArgumentProperty<NamedArgumentAttribute>> longNamedArguments;
        private readonly Dictionary<char, ArgumentProperty<NamedArgumentAttribute>> shortNamedArguments;
        private ArgumentProperty<RemainingArgumentsAttribute> remainingArguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDefinition"/> class.
        /// </summary>
        /// <param name="containerType">The type of the container.</param>
        public ArgumentDefinition(Type containerType)
        {
            // initialize
            this.containerType = containerType;
            this.positionalArguments = new Dictionary<int, ArgumentProperty<PositionalArgumentAttribute>>();
            this.longNamedArguments = new Dictionary<string, ArgumentProperty<NamedArgumentAttribute>>();
            this.shortNamedArguments = new Dictionary<char, ArgumentProperty<NamedArgumentAttribute>>();
            this.remainingArguments = null;

            this.ReflectContainer();
        }

        /// <summary>
        /// Gets the type of the container.
        /// </summary>
        /// <value>The type of the container.</value>
        public Type ContainerType
        {
            get { return this.containerType; }
        }

        /// <summary>
        /// Gets the positional arguments.
        /// </summary>
        /// <value>The positional arguments.</value>
        public Dictionary<int, ArgumentProperty<PositionalArgumentAttribute>> PositionalArguments
        {
            get { return this.positionalArguments; }
        }

        /// <summary>
        /// Gets the long named arguments.
        /// </summary>
        /// <value>The long named arguments.</value>
        public Dictionary<string, ArgumentProperty<NamedArgumentAttribute>> LongNamedArguments
        {
            get { return this.longNamedArguments; }
        }

        /// <summary>
        /// Gets the short named arguments.
        /// </summary>
        /// <value>The short named arguments.</value>
        public Dictionary<char, ArgumentProperty<NamedArgumentAttribute>> ShortNamedArguments
        {
            get { return this.shortNamedArguments; }
        }

        /// <summary>
        /// Gets the remaining arguments.
        /// </summary>
        /// <value>The remaining arguments.</value>
        public ArgumentProperty<RemainingArgumentsAttribute> RemainingArguments
        {
            get { return this.remainingArguments; }
        }

        private void ReflectContainer()
        {
            // reflect container type
            foreach (PropertyInfo pi in this.containerType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                List<ArgumentAttribute> attributes = pi.GetCustomAttributes(true).OfType<ArgumentAttribute>().ToList();

                if (attributes.Count == 1)
                {
                    ArgumentAttribute attribute = attributes.Single();
                    ArgumentDescriptionAttribute descriptionAttribute = pi.GetCustomAttributes(true).OfType<ArgumentDescriptionAttribute>().SingleOrDefault();
                    Type attributeType = attribute.GetType();

                    if (attributeType == typeof(PositionalValueArgumentAttribute))
                    {
                        var castedAttribute = attribute as PositionalValueArgumentAttribute;
                        var argumentProperty = new ArgumentProperty<PositionalArgumentAttribute>(castedAttribute, descriptionAttribute, pi);

                        this.EnsureIndexIsFree(castedAttribute.Index);
                        this.positionalArguments.Add(castedAttribute.Index, argumentProperty);
                    }
                    else if (attributeType == typeof(NamedValueArgumentAttribute))
                    {
                        var castedAttribute = attribute as NamedValueArgumentAttribute;
                        var argumentProperty = new ArgumentProperty<NamedArgumentAttribute>(castedAttribute, descriptionAttribute, pi);

                        this.EnsureLongNameIsFree(castedAttribute.LongName);
                        this.EnsureLongNameFormat(castedAttribute.LongName);
                        this.longNamedArguments.Add(castedAttribute.LongName, argumentProperty);

                        if (castedAttribute.ShortName.HasValue)
                        {
                            this.EnsureShortNameIsFree(castedAttribute.ShortName.Value);
                            this.shortNamedArguments.Add(castedAttribute.ShortName.Value, argumentProperty);
                        }
                    }
                    else if (attributeType == typeof(NamedSwitchArgumentAttribute))
                    {
                        var castedAttribute = attribute as NamedSwitchArgumentAttribute;
                        var argumentProperty = new ArgumentProperty<NamedArgumentAttribute>(castedAttribute, descriptionAttribute, pi);

                        this.EnsureLongNameIsFree(castedAttribute.LongName);
                        this.EnsureLongNameFormat(castedAttribute.LongName);
                        this.EnsurePropertyType(pi, typeof(bool));
                        this.longNamedArguments.Add(castedAttribute.LongName, argumentProperty);

                        if (castedAttribute.ShortName.HasValue)
                        {
                            this.EnsureShortNameIsFree(castedAttribute.ShortName.Value);
                            this.shortNamedArguments.Add(castedAttribute.ShortName.Value, argumentProperty);
                        }
                    }
                    else if (attributeType == typeof(RemainingArgumentsAttribute))
                    {
                        var castedAttribute = attribute as RemainingArgumentsAttribute;
                        var argumentProperty = new ArgumentProperty<RemainingArgumentsAttribute>(castedAttribute, descriptionAttribute, pi);

                        this.EnsureRemainingArgumentsIsFree();
                        this.EnsurePropertyType(pi, typeof(string[]));
                        this.remainingArguments = argumentProperty;
                    }
                    else
                    {
                        throw new ArgumentDefinitionException(string.Format("The property {0}::{1} has an unsupported {2} of type {3}", this.containerType.FullName, pi.Name, typeof(ArgumentAttribute).Name, attributeType.FullName));
                    }
                }
                else if (attributes.Count > 1)
                {
                    throw new ArgumentDefinitionException(string.Format("The property {0}::{1} has more than one {2}", this.containerType.FullName, pi.Name, typeof(ArgumentAttribute).Name));
                }
            }

            this.EnsureIndexCompleteness();
        }

        private void EnsurePropertyType(PropertyInfo pi, Type neededType)
        {
            if (pi.PropertyType != neededType)
            {
                throw new ArgumentDefinitionException(string.Format("The property {0}::{1} must have type {2}", this.containerType.FullName, pi.Name, neededType.FullName));
            }
        }

        private void EnsureRemainingArgumentsIsFree()
        {
            if (this.remainingArguments != null)
            {
                throw new ArgumentDefinitionException(string.Format("The {0} can only be used once", typeof(RemainingArgumentsAttribute).Name));
            }
        }

        private void EnsureIndexIsFree(int index)
        {
            if (this.positionalArguments.ContainsKey(index))
            {
                throw new ArgumentDefinitionException(string.Format("Index {0} is already in use", index));
            }
        }

        private void EnsureLongNameIsFree(string longName)
        {
            if (this.longNamedArguments.ContainsKey(longName))
            {
                throw new ArgumentDefinitionException(string.Format("Long name {0} is already in use", longName));
            }
        }

        private void EnsureShortNameIsFree(char shortName)
        {
            if (this.shortNamedArguments.ContainsKey(shortName))
            {
                throw new ArgumentDefinitionException(string.Format("Short name {0} is already in use", shortName));
            }
        }

        private void EnsureLongNameFormat(string longName)
        {
            if (!LongNameRegex.Match(longName).Success)
            {
                throw new ArgumentDefinitionException(string.Format("Long name {0} is invalid. Must match regex ^[a-zA-Z]([\\-|_]?[a-zA-Z0-9]+)*$", longName));
            }

            if (longName.Length < 2)
            {
                throw new ArgumentDefinitionException(string.Format("Long name {0} is invalid. Must have at least 2 characters", longName));
            }
        }

        /// <summary>
        /// Ensures, that the indices of positional arguments start at 0 and are gapless.
        /// </summary>
        private void EnsureIndexCompleteness()
        {
            List<int> indices = this.positionalArguments.Keys.OrderBy(i => i).ToList();

            if (indices.Count > 0 && indices[0] != 0)
            {
                throw new ArgumentDefinitionException("Indices must start at 0");
            }

            for (int i = 1; i < indices.Count; i++)
            {
                if (indices[i] != i)
                {
                    throw new ArgumentDefinitionException(string.Format("Index {0} is missing", i));
                }
            }
        }

        /// <summary>
        /// A pair of a property and the corrosponding argument attribute.
        /// </summary>
        /// <typeparam name="T">The concrete argment attribute type.</typeparam>
        public class ArgumentProperty<T>
            where T : ArgumentAttribute
        {
            private readonly T attribute;
            private readonly ArgumentDescriptionAttribute descriptionAttribute;
            private readonly PropertyInfo property;

            /// <summary>
            /// Initializes a new instance of the <see cref="ArgumentProperty{T}"/> class.
            /// </summary>
            /// <param name="attribute">The argument attribute.</param>
            /// <param name="descriptionAttribute">The description attribute.</param>
            /// <param name="property">The property info.</param>
            public ArgumentProperty(T attribute, ArgumentDescriptionAttribute descriptionAttribute, PropertyInfo property)
            {
                this.attribute = attribute;
                this.descriptionAttribute = descriptionAttribute;
                this.property = property;
            }

            /// <summary>
            /// Gets the argument attribute.
            /// </summary>
            /// <value>The attribute.</value>
            public T Attribute
            {
                get { return this.attribute; }
            }

            /// <summary>
            /// Gets the description attribute.
            /// </summary>
            /// <value>The description attribute.</value>
            public ArgumentDescriptionAttribute DescriptionAttribute
            {
                get { return this.descriptionAttribute; }
            }

            /// <summary>
            /// Gets the property info.
            /// </summary>
            /// <value>The property.</value>
            public PropertyInfo Property
            {
                get { return this.property; }
            }
        }
    }
}
