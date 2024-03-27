# IOUtils: Input and Output Utility Package for .NET

## Installation

Install the IOUtils NuGet package using the .NET CLI:

```
dotnet add package IOUtils
```

## Features

### Retrieve text from the user

TextInput is a class that allows for the retrieval of text from the user.

```csharp
using IOUtils.Input;

string text = TextInput.GetText("Enter some text");
string nonEmptyText = TextInput.GetNonEmptyText("Enter some (non-empty) text");
```

### Retrieve number from the user

NumberInput is a generic class that allows for the retrieval of a number from the user. Optionally, a
minimum and maximum value can be specified to restrict the input range.

```csharp
using IOUtils.Input;

int value = NumberInput<int>.GetNumber("Enter a whole number");
long value = NumberInput<long>.GetNumber("Enter a non-negative whole number", min: 0);
float value = NumberInput<float>.GetNumber("Enter a negative number", max: 0);
decimal value = NumberInput<decimal>.GetNumber("Enter a number between 0 and 100", min: 0, max: 100);
```

### Retrieve option from the user

OptionInput is a class that allows for the retrieval of an option from the user.

```csharp
using IOUtils.Input;

string selectedOption = OptionInput.GetOption("Select an option", new[] { "Option 1", "Option 2", "Option 3" });
int optionIndex = OptionInput.GetOptionIndex("Select an option", new[] { "Option 1", "Option 2", "Option 3" });
bool doSomething = OptionInput.GetYesNoOption("Do you want to do something?");
```

### Retrieve file path from the user

FileInput is a class that allows for the retrieval of an existing file or directory path from the user.

```csharp
using IOUtils.Input;

string filePath = FileInput.GetFilePath("Enter a file path");
string directoryPath = FileInput.GetDirectoryPath("Enter a directory path");
```

## Contributions

Contributions are welcome! Read
the [CONTRIBUTING](https://github.com/Thomas-Shephard/io-utils/blob/main/CONTRIBUTING.md) guide for information.

## License

This project is licensed under the MIT License. See
the [LICENSE](https://github.com/Thomas-Shephard/io-utils/blob/main/LICENSE) for details.