using System;
using System.Linq;
using System.Text;
using DotArguments;
using DotArguments.Attributes;

namespace DotArgumentsDemo
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
            // create container definition
            ContainerDefinition definition = new ContainerDefinition(typeof(DemoArguments));

            try
            {
                // create object with the populated arguments
                DemoArguments arguments = ArgumentParser<DemoArguments>.Parse(definition, args);

                Console.WriteLine("InputPath: {0}", arguments.InputPath ?? "(null)");
                Console.WriteLine("OutputPath: {0}", arguments.OutputPath ?? "(null)");
                Console.WriteLine("Name: {0}", arguments.Name ?? "(null)");
                Console.WriteLine("Age: {0}", arguments.Age.HasValue ? arguments.Age.Value.ToString() : "(null)");
                Console.WriteLine("Verbose: {0}", arguments.Verbose);

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(string.Format("error: {0}", ex.Message));
                Console.Error.Write(string.Format("usage: {0}", definition.GenerateUsageString("DotArguments.Demo.exe")));

                Environment.Exit(1);
            }
        }
    }
}