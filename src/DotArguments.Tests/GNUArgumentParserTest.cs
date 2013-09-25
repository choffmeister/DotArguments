using DotArguments.Exceptions;
using DotArguments.Tests.TestContainers;
using NUnit.Framework;

namespace DotArguments.Tests
{
    /// <summary>
    /// Argument parser test.
    /// </summary>
    [TestFixture]
    public class GNUArgumentParserTest
    {
        private readonly IArgumentParser parser = new GNUArgumentParser();

        /// <summary>
        /// Test case 1.
        /// </summary>
        [Test]
        public void TestCase1()
        {
            ArgumentDefinition definition = new ArgumentDefinition(typeof(ArgumentContainer1));
            object c1 = this.parser.Parse(definition, new string[] { });
            Assert.IsInstanceOf(typeof(ArgumentContainer1), c1);
        }

        /// <summary>
        /// Test case 2.
        /// </summary>
        [Test]
        public void TestCase2()
        {
            ArgumentDefinition definition = new ArgumentDefinition(typeof(ArgumentContainer2));

            ArgumentContainer2 c1 = this.parser.Parse<ArgumentContainer2>(definition, new string[] { "foo1", "--age=11" });
            Assert.AreEqual("foo1", c1.Name);
            Assert.AreEqual(11, c1.Age);

            ArgumentContainer2 c2 = this.parser.Parse<ArgumentContainer2>(definition, new string[] { "--age=12", "foo2" });
            Assert.AreEqual("foo2", c2.Name);
            Assert.AreEqual(12, c2.Age);

            ArgumentContainer2 c3 = this.parser.Parse<ArgumentContainer2>(definition, new string[] { "foo3", "-a", "13" });
            Assert.AreEqual("foo3", c3.Name);
            Assert.AreEqual(13, c3.Age);

            ArgumentContainer2 c4 = this.parser.Parse<ArgumentContainer2>(definition, new string[] { "-a", "14", "foo4" });
            Assert.AreEqual("foo4", c4.Name);
            Assert.AreEqual(14, c4.Age);

            ArgumentContainer2 c5 = this.parser.Parse<ArgumentContainer2>(definition, new string[] { "foo5", "-a15" });
            Assert.AreEqual("foo5", c5.Name);
            Assert.AreEqual(15, c5.Age);

            ArgumentContainer2 c6 = this.parser.Parse<ArgumentContainer2>(definition, new string[] { "-a16", "foo6" });
            Assert.AreEqual("foo6", c6.Name);
            Assert.AreEqual(16, c6.Age);
        }

        /// <summary>
        /// Test case 3.
        /// </summary>
        [Test]
        public void TestCase3()
        {
            ArgumentDefinition definition = new ArgumentDefinition(typeof(ArgumentContainer3));

            ArgumentContainer3 c1 = this.parser.Parse<ArgumentContainer3>(definition, new string[] { "foo1" });
            Assert.AreEqual("foo1", c1.Name);
            Assert.AreEqual(new string[] { }, c1.Remaining);

            ArgumentContainer3 c2 = this.parser.Parse<ArgumentContainer3>(definition, new string[] { "foo1", "foo2", "foo3" });
            Assert.AreEqual("foo1", c2.Name);
            Assert.AreEqual(new string[] { "foo2", "foo3" }, c2.Remaining);
        }

        /// <summary>
        /// Test case 4.
        /// </summary>
        [Test]
        public void TestCase4()
        {
            ArgumentDefinition definition = new ArgumentDefinition(typeof(ArgumentContainer4));

            ArgumentContainer4 c1 = this.parser.Parse<ArgumentContainer4>(definition, new string[] { });
            Assert.AreEqual(false, c1.Verbose);

            ArgumentContainer4 c2 = this.parser.Parse<ArgumentContainer4>(definition, new string[] { "--verbose" });
            Assert.AreEqual(true, c2.Verbose);

            ArgumentContainer4 c3 = this.parser.Parse<ArgumentContainer4>(definition, new string[] { "-v" });
            Assert.AreEqual(true, c3.Verbose);
        }

        /// <summary>
        /// Test case 5.
        /// </summary>
        [Test]
        public void TestCase5()
        {
            ArgumentDefinition definition = new ArgumentDefinition(typeof(ArgumentContainer6));

            ArgumentContainer6 c1 = this.parser.Parse<ArgumentContainer6>(definition, new string[] { "--name1=n1", "1" });
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
            ArgumentDefinition definition = new ArgumentDefinition(typeof(ArgumentContainer6));

            AssertHelper.AssertExceptionWithMessage(
                typeof(ArgumentParserException),
                Is.StringContaining("name1").And.StringContaining("missing"),
                () =>
            {
                this.parser.Parse(definition, new string[] { "1" });
            });
        }

        /// <summary>
        /// Test case 7.
        /// </summary>
        [Test]
        public void TestCase7()
        {
            ArgumentDefinition definition = new ArgumentDefinition(typeof(ArgumentContainer6));

            AssertHelper.AssertExceptionWithMessage(
                typeof(ArgumentParserException),
                Is.StringContaining("0").And.StringContaining("missing"),
                () =>
            {
                this.parser.Parse(definition, new string[] { "--name1=n1" });
            });
        }

        /// <summary>
        /// Test case 8.
        /// </summary>
        [Test]
        public void TestCase8()
        {
            ArgumentDefinition definition = new ArgumentDefinition(typeof(ArgumentContainer2));

            AssertHelper.AssertExceptionWithMessage(
                typeof(ArgumentParserException),
                Is.StringContaining("Too many"),
                () =>
            {
                this.parser.Parse(definition, new string[] { "--age=10", "pos1", "pos2" });
            });
        }

        /// <summary>
        /// Test case 9.
        /// </summary>
        [Test]
        public void TestCase9()
        {
            ArgumentDefinition definition = new ArgumentDefinition(typeof(ArgumentContainer8));

            ArgumentContainer8 c1 = this.parser.Parse<ArgumentContainer8>(definition, new string[] { });
            Assert.IsFalse(c1.A);
            Assert.IsFalse(c1.B);
            Assert.IsFalse(c1.C);

            ArgumentContainer8 c2 = this.parser.Parse<ArgumentContainer8>(definition, new string[] { "-a" });
            Assert.IsTrue(c2.A);
            Assert.IsFalse(c2.B);
            Assert.IsFalse(c2.C);

            ArgumentContainer8 c3 = this.parser.Parse<ArgumentContainer8>(definition, new string[] { "-ab" });
            Assert.IsTrue(c3.A);
            Assert.IsTrue(c3.B);
            Assert.IsFalse(c3.C);

            ArgumentContainer8 c4 = this.parser.Parse<ArgumentContainer8>(definition, new string[] { "-abc" });
            Assert.IsTrue(c4.A);
            Assert.IsTrue(c4.B);
            Assert.IsTrue(c4.C);

            ArgumentContainer8 c5 = this.parser.Parse<ArgumentContainer8>(definition, new string[] { "-ab", "-c" });
            Assert.IsTrue(c5.A);
            Assert.IsTrue(c5.B);
            Assert.IsTrue(c5.C);
        }

        /// <summary>
        /// Test case 10.
        /// </summary>
        [Test]
        public void TestCase10()
        {
            ArgumentDefinition definition = new ArgumentDefinition(typeof(ArgumentContainer2));

            ArgumentContainer2 c1 = this.parser.Parse<ArgumentContainer2>(definition, new string[] { "--age=10", "--", "--a10" });
            Assert.AreEqual("--a10", c1.Name);
            Assert.AreEqual(10, c1.Age);

            ArgumentContainer2 c2 = this.parser.Parse<ArgumentContainer2>(definition, new string[] { "-a10", "--", "-a10" });
            Assert.AreEqual("-a10", c2.Name);
            Assert.AreEqual(10, c2.Age);
        }
    }
}
