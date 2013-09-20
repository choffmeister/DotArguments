using System;
using DotArguments.Tests.TestContainers;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace DotArguments.Tests
{
    /// <summary>
    /// Argument parser test.
    /// </summary>
    [TestFixture]
    public class ArgumentParserTest
    {
        /// <summary>
        /// Test case 1.
        /// </summary>
        [Test]
        public void TestCase1()
        {
            object c1 = ArgumentParser<ArgumentContainer1>.Parse(new string[] { });
            Assert.IsInstanceOf(typeof(ArgumentContainer1), c1);
        }

        /// <summary>
        /// Test case 2.
        /// </summary>
        [Test]
        public void TestCase2()
        {
            ArgumentContainer2 c1 = ArgumentParser<ArgumentContainer2>.Parse(new string[] { "foo1", "--age", "11" });
            Assert.AreEqual("foo1", c1.Name);
            Assert.AreEqual(11, c1.Age);

            ArgumentContainer2 c2 = ArgumentParser<ArgumentContainer2>.Parse(new string[] { "--age", "12", "foo2" });
            Assert.AreEqual("foo2", c2.Name);
            Assert.AreEqual(12, c2.Age);

            ArgumentContainer2 c3 = ArgumentParser<ArgumentContainer2>.Parse(new string[] { "foo3", "-a", "13" });
            Assert.AreEqual("foo3", c3.Name);
            Assert.AreEqual(13, c3.Age);

            ArgumentContainer2 c4 = ArgumentParser<ArgumentContainer2>.Parse(new string[] { "-a", "14", "foo4" });
            Assert.AreEqual("foo4", c4.Name);
            Assert.AreEqual(14, c4.Age);
        }

        /// <summary>
        /// Test case 3.
        /// </summary>
        [Test]
        public void TestCase3()
        {
            ArgumentContainer3 c1 = ArgumentParser<ArgumentContainer3>.Parse(new string[] { "foo1" });
            Assert.AreEqual("foo1", c1.Name);
            Assert.AreEqual(new string[] { }, c1.Remaining);

            ArgumentContainer3 c2 = ArgumentParser<ArgumentContainer3>.Parse(new string[] { "foo1", "foo2", "foo3" });
            Assert.AreEqual("foo1", c2.Name);
            Assert.AreEqual(new string[] { "foo2", "foo3" }, c2.Remaining);
        }

        /// <summary>
        /// Test case 4.
        /// </summary>
        [Test]
        public void TestCase4()
        {
            ArgumentContainer4 c1 = ArgumentParser<ArgumentContainer4>.Parse(new string[] { });
            Assert.AreEqual(false, c1.Verbose);

            ArgumentContainer4 c2 = ArgumentParser<ArgumentContainer4>.Parse(new string[] { "--verbose" });
            Assert.AreEqual(true, c2.Verbose);

            ArgumentContainer4 c3 = ArgumentParser<ArgumentContainer4>.Parse(new string[] { "-v" });
            Assert.AreEqual(true, c3.Verbose);
        }

        /// <summary>
        /// Test case 5.
        /// </summary>
        [Test]
        public void TestCase5()
        {
            ArgumentContainer6 c1 = ArgumentParser<ArgumentContainer6>.Parse(new string[] { "--name1", "n1", "1" });
            Assert.AreEqual("n1", c1.Name1);
            Assert.IsNull(c1.Name2);
            Assert.AreEqual(1, c1.Age1);
            Assert.IsNull(c1.Age2);
        }

        /// <summary>
        /// Test case 6.
        /// </summary>
        [Test]
        public void TestCase6()
        {
            AssertExceptionWithMessage(
                typeof(ArgumentParserException),
                Is.StringContaining("name1").And.StringContaining("missing"),
                () =>
            {
                ArgumentParser<ArgumentContainer6>.Parse(new string[] { "1" });
            });
        }

        /// <summary>
        /// Test case 7.
        /// </summary>
        [Test]
        public void TestCase7()
        {
            AssertExceptionWithMessage(
                typeof(ArgumentParserException),
                Is.StringContaining("0").And.StringContaining("missing"),
                () =>
            {
                ArgumentParser<ArgumentContainer6>.Parse(new string[] { "--name1", "n1" });
            });
        }

        /// <summary>
        /// Test case 8.
        /// </summary>
        [Test]
        public void TestCase8()
        {
            AssertExceptionWithMessage(
                typeof(ArgumentParserException),
                Is.StringContaining("Too many"),
                () =>
            {
                ArgumentParser<ArgumentContainer2>.Parse(new string[] { "--age", "10", "pos1", "pos2" });
            });
        }

        /// <summary>
        /// Test case 9.
        /// </summary>
        [Test]
        public void TestCase9()
        {
            AssertExceptionWithMessage(
                typeof(ArgumentParserException),
                Is.StringContaining("out of one character"),
            () =>
            {
                ArgumentParser<ArgumentContainer2>.Parse(new string[] { "-ab", "10", "pos1" });
            });
        }

        private static void AssertExceptionWithMessage(Type exceptionType, IResolveConstraint messageConstraint, Action action)
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
