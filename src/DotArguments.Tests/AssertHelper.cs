using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace DotArguments.Tests
{
    /// <summary>
    /// Some useful assert methods.
    /// </summary>
    internal static class AssertHelper
    {
        /// <summary>
        /// Assert that the console output fulfills a given constraint.
        /// </summary>
        /// <param name="consoleOutputConstraint">The console output constraint.</param>
        /// <param name="action">The action to execute.</param>
        public static void AssertWithConsoleOutput(IResolveConstraint consoleOutputConstraint, Action action)
        {
            StringWriter writer = new StringWriter();
            Console.SetOut(writer);

            action();

            Assert.That(writer.ToString(), consoleOutputConstraint);
        }

        /// <summary>
        /// Assert that a given exception is thrown and that the message of the exceptions
        /// fulfills a given constraint.
        /// </summary>
        /// <param name="exceptionType">The exception type.</param>
        /// <param name="messageConstraint">The message constraint.</param>
        /// <param name="action">The action to execute.</param>
        public static void AssertExceptionWithMessage(Type exceptionType, IResolveConstraint messageConstraint, Action action)
        {
            try
            {
                action();

                Assert.Fail(string.Format("Expected exception"));
            }
            catch (AssertionException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exceptionType, ex.GetType());
                Assert.That(ex.Message, messageConstraint);
            }
        }
    }
}
