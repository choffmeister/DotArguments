using System;
using DotArguments;

namespace DotArguments.Demo
{
    /// <summary>
    /// The main executable class for the demo.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            string executableName = System.AppDomain.CurrentDomain.FriendlyName;

            // create container definition and the parser
            ArgumentDefinition definition = new ArgumentDefinition(typeof(DemoArguments));
            GNUArgumentParser parser = new GNUArgumentParser();

            try
            {
                // create object with the populated arguments
                DemoArguments arguments = parser.Parse<DemoArguments>(definition, args);

                Console.WriteLine("InputPath: {0}", arguments.InputPath ?? "(null)");
                Console.WriteLine("OutputPath: {0}", arguments.OutputPath ?? "(null)");
                Console.WriteLine("Name: {0}", arguments.Name ?? "(null)");
                Console.WriteLine("Age: {0}", arguments.Age.HasValue ? arguments.Age.Value.ToString() : "(null)");
                Console.WriteLine("Verbose: {0}", arguments.Verbose);
                Console.WriteLine("Remaining: [{0}]", string.Join(",", arguments.RemainingArguments));

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                string usageString = parser.GenerateUsageString(definition);

                Console.Error.WriteLine(string.Format("error: {0}", ex.Message));
                Console.Error.Write(string.Format("usage: {0}{1}", executableName, usageString));

                Environment.Exit(1);
            }
        }
    }
}