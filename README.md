# DotArguments

DotArguments is a simple command-line arguments parser for .NET/Mono. The available arguments are defined by simple POCOs with special attributes on its properties.

## Install via NuGet

This package can be found on NuGet:

```
PM> Install-Package DotArguments
```

## Supported arguments

There are four different types of arguments:

* Named values,
* Named switches,
* Positional arguments and
* all remaining arguments.

## Example

You just have to create a POCO with the needed attributes and then use the ArgumentParser to create a populated instance of the class from the arguments array.

```csharp
using System;
using DotArguments;
using DotArguments.Attributes;

namespace DotArgumentsDemo
{
    public class DemoArguments
    {
        [PositionalValueArgument(0, "inputpath")]
        [ArgumentDescription(Short = "the input path")]
        public string InputPath { get; set; }

        [PositionalValueArgument(1, "outputpath", IsOptional = true)]
        [ArgumentDescription(Short = "the output path")]
        public string OutputPath { get; set; }

        [NamedValueArgument("name", 'n', IsOptional = true)]
        [ArgumentDescription(Short = "the name")]
        public string Name { get; set; }

        [NamedValueArgument("age", IsOptional = true)]
        [ArgumentDescription(Short = "the age")]
        public int? Age { get; set; }

        [NamedSwitchArgument("verbose", 'v')]
        [ArgumentDescription(Short = "enable verbose console output")]
        public bool Verbose { get; set; }

        [RemainingArguments]
        public string[] RemainingArguments { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // create container definition
            ArgumentDefinition definition = new ArgumentDefinition(typeof(DemoArguments));

            try
            {
                // create object with the populated arguments
                DemoArguments arguments = definition.Parse<DemoArguments>(args);

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
                Console.Error.WriteLine(string.Format("error: {0}", ex.Message));
                Console.Error.Write(string.Format("usage: {0}", definition.GenerateUsageString()));

                Environment.Exit(1);
            }
        }
    }
}
```

Here are some examples, how the application can be invoked and what values would be populated:

```bash
$ DotArguments.Demo.exe --age 10 --name tom input output
InputPath: input
OutputPath: output
Name: tom
Age: 10
Verbose: False
Remaining: []
```

```bash
$ DotArguments.Demo.exe --name tom output --age 10
InputPath: output
OutputPath: (null)
Name: tom
Age: 10
Verbose: False
Remaining: []
```

```bash
$ DotArguments.Demo.exe input -v output additional1 additional2
InputPath: input
OutputPath: output
Name: (null)
Age: (null)
Verbose: True
Remaining: [additional1,additional2]
```

Are invocation with invalid arguments:

```bash
DotArguments.Demo.exe
error: Mandatory argument at position 0 is missing
usage: DotArguments.Demo.exe [options] inputpath [outputpath] [...]

  inputpath          the input path
  outputpath         the output path

  --age              the age
  -n, --name         the name
  -v, --verbose      enable verbose console output
```
