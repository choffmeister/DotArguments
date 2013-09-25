using System;
using System.Runtime.Serialization;

namespace DotArguments.Exceptions
{
    /// <summary>
    /// Base exception for errors while argument parsing.
    /// </summary>
    [Serializable]
    public class ArgumentParserException : Exception
    {
        private readonly IArgumentParser argumentParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentParserException"/> class.
        /// </summary>
        /// <param name="argumentParser">The argument parser.</param>
        public ArgumentParserException(IArgumentParser argumentParser)
        {
            this.argumentParser = argumentParser;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentParserException"/> class.
        /// </summary>
        /// <param name="argumentParser">The argument parser.</param>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public ArgumentParserException(IArgumentParser argumentParser, string message)
            : base(message)
        {
            this.argumentParser = argumentParser;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentParserException"/> class.
        /// </summary>
        /// <param name="argumentParser">The argument parser.</param>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public ArgumentParserException(IArgumentParser argumentParser, string message, Exception inner)
            : base(message, inner)
        {
            this.argumentParser = argumentParser;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentParserException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected ArgumentParserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Gets the argument parser.
        /// </summary>
        /// <value>The argument parser.</value>
        public IArgumentParser ArgumentParser
        {
            get { return this.argumentParser; }
        }
    }
}
