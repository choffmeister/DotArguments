using DotArguments.Tests.TestCommands;
using NUnit.Framework;

namespace DotArguments.Tests
{
    /// <summary>
    /// Command manager test.
    /// </summary>
    [TestFixture]
    public class CommandManagerTest
    {
        /// <summary>
        /// Tests case 1.
        /// </summary>
        [Test]
        public void TestExceptionOnMissingCommandName()
        {
            CommandManager commandManager = new CommandManager();
            commandManager.RegisterCommand(typeof(Command1));
            commandManager.RegisterCommand(typeof(Command2));

            AssertHelper.AssertExceptionWithMessage(
                typeof(CommandManagerException),
                Is.StringContaining("is missing"),
                () =>
            {
                commandManager.Execute(new string[] { });
            });
        }

        /// <summary>
        /// Tests case 2.
        /// </summary>
        [Test]
        public void TestExceptionOnUnknownCommandName()
        {
            CommandManager commandManager = new CommandManager();
            commandManager.RegisterCommand(typeof(Command1));
            commandManager.RegisterCommand(typeof(Command2));

            AssertHelper.AssertExceptionWithMessage(
                typeof(CommandManagerException),
                Is.StringContaining("is unknown"),
                () =>
                {
                commandManager.Execute(new string[] { "unknown" });
            });
        }

        /// <summary>
        /// Tests case 3.
        /// </summary>
        [Test]
        public void TestCorrectDelegationToCommands()
        {
            CommandManager commandManager = new CommandManager();
            commandManager.RegisterCommand(typeof(Command1));
            commandManager.RegisterCommand(typeof(Command2));

            AssertHelper.AssertWithConsoleOutput(
                Is.StringContaining("Name: Tom").And.StringContaining("Age: (null)"),
                () =>
            {
                Assert.AreEqual(0, commandManager.Execute(new string[] { "cmd1", "Tom" }));
            });

            AssertHelper.AssertWithConsoleOutput(
                Is.StringContaining("Name: Tom").And.StringContaining("Age: 12"),
            () =>
                {
                Assert.AreEqual(0, commandManager.Execute(new string[] { "cmd1", "Tom", "--age", "12" }));
            });

            AssertHelper.AssertWithConsoleOutput(
                Is.StringContaining("Path: myfile"),
            () =>
                {
                Assert.AreEqual(0, commandManager.Execute(new string[] { "cmd2", "myfile" }));
            });
        }
    }
}
