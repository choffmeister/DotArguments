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
            var def = new ArgumentDefinition(typeof(ArgumentContainer1));

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
            var def = new ArgumentDefinition(typeof(ArgumentContainer2));

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
            var def = new ArgumentDefinition(typeof(ArgumentContainer3));

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
            var def = new ArgumentDefinition(typeof(ArgumentContainer4));

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
            var def = new ArgumentDefinition(typeof(ArgumentContainer5));

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
                typeof(ArgumentDefinitionException),
                Is.StringContaining("more than one"),
                () =>
            {
                new ArgumentDefinition(typeof(CorruptArgumentContainer1));
            });
        }

        /// <summary>
        /// Test that problems with the positional indices are detected properly.
        /// </summary>
        [Test]
        public void TestDetectionOfPositionalIndicesProblems()
        {
            AssertExceptionWithMessage(
                typeof(ArgumentDefinitionException),
                Is.StringContaining("already in use"),
                () =>
            {
                new ArgumentDefinition(typeof(CorruptArgumentContainer2));
            });

            AssertExceptionWithMessage(
                typeof(ArgumentDefinitionException),
                Is.StringContaining("is missing"),
                () =>
            {
                new ArgumentDefinition(typeof(CorruptArgumentContainer3));
            });

            AssertExceptionWithMessage(
                typeof(ArgumentDefinitionException),
                Is.StringContaining("must start at"),
                () =>
            {
                new ArgumentDefinition(typeof(CorruptArgumentContainer4));
            });
        }

        /// <summary>
        /// Test that problems with the names detected properly.
        /// </summary>
        [Test]
        public void TestDetectionOfNamingProblems()
        {
            AssertExceptionWithMessage(
                typeof(ArgumentDefinitionException),
                Is.StringContaining("Long name").And.StringContaining("already in use"),
                () =>
            {
                new ArgumentDefinition(typeof(CorruptArgumentContainer5));
            });

            AssertExceptionWithMessage(
                typeof(ArgumentDefinitionException),
                Is.StringContaining("Short name").And.StringContaining("already in use"),
                () =>
            {
                new ArgumentDefinition(typeof(CorruptArgumentContainer6));
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
                typeof(ArgumentDefinitionException),
                Is.StringContaining("can only be used once"),
                () =>
            {
                new ArgumentDefinition(typeof(CorruptArgumentContainer7));
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
                typeof(ArgumentDefinitionException),
                Is.StringContaining("must have type System.String[]"),
                () =>
            {
                new ArgumentDefinition(typeof(CorruptArgumentContainer8));
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
                typeof(ArgumentDefinitionException),
                Is.StringContaining("must have type System.Boolean"),
                () =>
            {
                new ArgumentDefinition(typeof(CorruptArgumentContainer9));
            });
        }

        /// <summary>
        /// Test that invalid long names are detected properly.
        /// </summary>
        [Test]
        public void TestDetectionOfInvalidLongNames()
        {
            AssertExceptionWithMessage(
                typeof(ArgumentDefinitionException),
                Is.StringContaining("at least 2 characters"),
                () =>
            {
                new ArgumentDefinition(typeof(CorruptArgumentContainer10));
            });

            AssertExceptionWithMessage(
                typeof(ArgumentDefinitionException),
                Is.StringContaining("match regex"),
                () =>
            {
                new ArgumentDefinition(typeof(CorruptArgumentContainer11));
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
