using DotArguments.Tests.TestContainers;
using NUnit.Framework;

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
    }
}
