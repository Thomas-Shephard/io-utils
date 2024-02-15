# IOUtils: Input and Output Utility Package for .NET

## Installation

Install the IOUtils NuGet package using the .NET CLI:

```
dotnet add package IOUtils
```

## Features

### Retrieve numerical input from the user

NumericalInput is a generic class that allows for the retrieval of a numerical input from the user. Optionally, a
minimum and maximum value can be specified to restrict the input range.

```csharp
using IOUtils.Input;

int value = NumericalInput<int>.Get("Enter a whole number");
long value = NumericalInput<long>.Get("Enter a non-negative whole number", min: 0);
float value = NumericalInput<float>.Get("Enter a negative number", max: 0);
decimal value = NumericalInput<decimal>.Get("Enter a number between 0 and 100", min: 0, max: 100);
```

### Retrieve option from the user

OptionInput is a class that allows for the retrieval of an option from the user.

```csharp
using IOUtils.Input;

string selectedOption = OptionInput.GetOption("Select an option", new[] { "Option 1", "Option 2", "Option 3" });
int optionIndex = OptionInput.GetOptionIndex("Select an option", new[] { "Option 1", "Option 2", "Option 3" });
bool doSomething = OptionInput.GetYesNoOption("Do you want to do something?");
```

## Contributions

Contributions are welcome! Read the [CONTRIBUTING](CONTRIBUTING.md) guide for information.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) for details.