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
        [PositionalValueArgument(0)]
        public string InputPath { get; set; }

        [PositionalValueArgument(1, IsOptional = true)]
        public string OutputPath { get; set; }

        [NamedValueArgument("name", 'n', IsOptional = true)]
        public string Name { get; set; }

        [NamedValueArgument("age", 'a', IsOptional = true)]
        public int? Age { get; set; }

        [NamedSwitchArgument("verbose", 'v')]
        public bool Verbose { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // create object with the populated arguments
                DemoArguments arguments = ArgumentParser<DemoArguments>.Parse(args);

                Console.WriteLine("InputPath: {0}", arguments.InputPath ?? "(null)");
                Console.WriteLine("OutputPath: {0}", arguments.OutputPath ?? "(null)");
                Console.WriteLine("Name: {0}", arguments.Name ?? "(null)");
                Console.WriteLine("Age: {0}", arguments.Age.HasValue ? arguments.Age.Value.ToString() : "(null)");
                Console.WriteLine("Verbose: {0}", arguments.Verbose);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }
    }
}
```

Here are some examples, how the application can be invoked and what values would be populated:

```bash
$ DotArgumentsDemo.exe -a 10 --name tom input output
InputPath: input
OutputPath: output
Name: tom
Age: 10
Verbose: False
```

```bash
$ DotArgumentsDemo.exe input --name tom output -a 10
InputPath: input
OutputPath: output
Name: tom
Age: 10
Verbose: False
```

```bash
$ DotArgumentsDemo.exe input --verbose -a 10
InputPath: input
OutputPath: (null)
Name: (null)
Age: 10
Verbose: True
```

```bash
$ DotArgumentsDemo.exe input -v output
InputPath: input
OutputPath: output
Name: (null)
Age: (null)
Verbose: True
```