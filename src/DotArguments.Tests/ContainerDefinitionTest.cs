using System;
using DotArguments.Tests.TestContainers;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace DotArguments.Tests
{
    /// <summary>
    /// Container definition test.
    /// </summary>
    [TestFixture]
    public class ContainerDefinitionTest
    {
        /// <summary>
        /// Test non corrupt containers.
        /// </summary>
        [Test]
        public void TestNonCorruptContainer1()
        {
            var def = new ContainerDefinition(typeof(ArgumentContainer1));

            Assert.AreEqual(0, def.PositionalArguments.Count);
            Assert.AreEqual(0, def.LongNamedArguments.Count);
            Assert.AreEqual(0, def.ShortNamedArguments.Count);
            Assert.IsNull(def.RemainingArguments);
        }

        /// <summary>
        /// Test non corrupt containers.
        /// </summary>
        [Test]
        public void TestNonCorruptContainer2()
        {
            var def = new ContainerDefinition(typeof(ArgumentContainer2));

            Assert.AreEqual(1, def.PositionalArguments.Count);
            Assert.AreEqual(1, def.LongNamedArguments.Count);
            Assert.AreEqual(1, def.ShortNamedArguments.Count);
            Assert.IsNull(def.RemainingArguments);

            Assert.AreEqual("Name", def.PositionalArguments[0].Property.Name);
            Assert.AreEqual("Age", def.LongNamedArguments["age"].Property.Name);
            Assert.AreEqual("Age", def.ShortNamedArguments['a'].Property.Name);
        }

        /// <summary>
        /// Test non corrupt containers.
        /// </summary>
        [Test]
        public void TestNonCorruptContainer3()
        {
            var def = new ContainerDefinition(typeof(ArgumentContainer3));

            Assert.AreEqual(1, def.PositionalArguments.Count);
            Assert.AreEqual(0, def.LongNamedArguments.Count);
            Assert.AreEqual(0, def.ShortNamedArguments.Count);
            Assert.IsNotNull(def.RemainingArguments);

            Assert.AreEqual("Name", def.PositionalArguments[0].Property.Name);
            Assert.AreEqual("Remaining", def.RemainingArguments.Property.Name);
        }

        /// <summary>
        /// Test non corrupt containers.
        /// </summary>
        [Test]
        public void TestNonCorruptContainer4()
        {
            var def = new ContainerDefinition(typeof(ArgumentContainer4));

            Assert.AreEqual(0, def.PositionalArguments.Count);
            Assert.AreEqual(1, def.LongNamedArguments.Count);
            Assert.AreEqual(1, def.ShortNamedArguments.Count);
            Assert.IsNull(def.RemainingArguments);

            Assert.AreEqual("Verbose", def.LongNamedArguments["verbose"].Property.Name);
            Assert.AreEqual("Verbose", def.ShortNamedArguments['v'].Property.Name);
        }

        /// <summary>
        /// Test non corrupt containers.
        /// </summary>
        [Test]
        public void TestNonCorruptContainer5()
        {
            var def = new ContainerDefinition(typeof(ArgumentContainer5));

            Assert.AreEqual(0, def.PositionalArguments.Count);
            Assert.AreEqual(2, def.LongNamedArguments.Count);
            Assert.AreEqual(1, def.ShortNamedArguments.Count);
            Assert.IsNull(def.RemainingArguments);

            Assert.AreEqual("Verbose", def.LongNamedArguments["verbose"].Property.Name);
            Assert.AreEqual("Verbose", def.ShortNamedArguments['v'].Property.Name);
            Assert.AreEqual("Name", def.LongNamedArguments["name"].Property.Name);
        }

        /// <summary>
        /// Test that properties with mutltiple <see cref="ArgumentAttribute"/>s are
        /// detected properly.
        /// </summary>
        [Test]
        public void TestDetectionOfMultipleArgumentAttributes()
        {
            AssertExceptionWithMessage(
                typeof(ContainerDefinitionException),
                Is.StringContaining("more than one"),
                () =>
            {
                new ContainerDefinition(typeof(CorruptArgumentContainer1));
            });
        }

        /// <summary>
        /// Test that problems with the positional indices are detected properly.
        /// </summary>
        [Test]
        public void TestDetectionOfPositionalIndicesProblems()
        {
            AssertExceptionWithMessage(
                typeof(ContainerDefinitionException),
                Is.StringContaining("already in use"),
                () =>
            {
                new ContainerDefinition(typeof(CorruptArgumentContainer2));
            });

            AssertExceptionWithMessage(
                typeof(ContainerDefinitionException),
                Is.StringContaining("is missing"),
                () =>
            {
                new ContainerDefinition(typeof(CorruptArgumentContainer3));
            });

            AssertExceptionWithMessage(
                typeof(ContainerDefinitionException),
                Is.StringContaining("must start at"),
                () =>
            {
                new ContainerDefinition(typeof(CorruptArgumentContainer4));
            });
        }

        /// <summary>
        /// Test that problems with the names detected properly.
        /// </summary>
        [Test]
        public void TestDetectionOfNamingProblems()
        {
            AssertExceptionWithMessage(
                typeof(ContainerDefinitionException),
                Is.StringContaining("Long name").And.StringContaining("already in use"),
                () =>
            {
                new ContainerDefinition(typeof(CorruptArgumentContainer5));
            });

            AssertExceptionWithMessage(
                typeof(ContainerDefinitionException),
                Is.StringContaining("Short name").And.StringContaining("already in use"),
                () =>
            {
                new ContainerDefinition(typeof(CorruptArgumentContainer6));
            });
        }

        /// <summary>
        /// Test that multiple <see cref="RemainingArgumentAttribute"/> annotations
        /// are detected properly. 
        /// </summary>
        [Test]
        public void TestDetectionOfMultipleRemainingArguments()
        {
            AssertExceptionWithMessage(
                typeof(ContainerDefinitionException),
                Is.StringContaining("can only be used once"),
                () =>
            {
                new ContainerDefinition(typeof(CorruptArgumentContainer7));
            });
        }

        /// <summary>
        /// Test that <see cref="RemainingArgumentAttribute"/> annotations on non
        /// <see cref="string[]"/> properties are detected properly. 
        /// </summary>
        [Test]
        public void TestDetectionOfRemainingArgumentsOnNonStringArrays()
        {
            AssertExceptionWithMessage(
                typeof(ContainerDefinitionException),
                Is.StringContaining("must have type System.String[]"),
                () =>
            {
                new ContainerDefinition(typeof(CorruptArgumentContainer8));
            });
        }

        /// <summary>
        /// Test that <see cref="NamedSwitchArgumentAttribute"/> annotations on non
        /// <see cref="bool"/> properties are detected properly. 
        /// </summary>
        [Test]
        public void TestDetectionOfNamedSwitchArgumentsOnNonBooleans()
        {
            AssertExceptionWithMessage(
                typeof(ContainerDefinitionException),
                Is.StringContaining("must have type System.Boolean"),
                () =>
            {
                new ContainerDefinition(typeof(CorruptArgumentContainer9));
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
