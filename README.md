# IOUtils: Input and Output Utility Package for .NET

[![Build, Test and Publish](https://github.com/Thomas-Shephard/io-utils/actions/workflows/build-test-and-publish.yml/badge.svg)](https://github.com/Thomas-Shephard/io-utils/actions/workflows/build-test-and-publish.yml)

## Installation

Install the IOUtils NuGet package using the .NET CLI:

```
dotnet add package IOUtils
```

## Features

### Input retrieval

#### Retrieve text from the user

TextInput is a class that allows for the retrieval of text from the user.

```csharp
using IOUtils.Input;

string text = TextInput.GetText("Enter some text");
string nonEmptyText = TextInput.GetNonEmptyText("Enter some (non-empty) text");
```

#### Retrieve number from the user

NumberInput is a generic class that allows for the retrieval of a number from the user. Optionally, a
minimum and maximum value can be specified to restrict the input range.

```csharp
using IOUtils.Input;

int value = NumberInput<int>.GetNumber("Enter a whole number");
long value = NumberInput<long>.GetNumber("Enter a non-negative whole number", min: 0);
float value = NumberInput<float>.GetNumber("Enter a negative number", max: 0);
decimal value = NumberInput<decimal>.GetNumber("Enter a number between 0 and 100", min: 0, max: 100);
```

#### Retrieve option from the user

OptionInput is a class that allows for the retrieval of an option from the user.

```csharp
using IOUtils.Input;

string selectedOption = OptionInput.GetOption("Select an option", new[] { "Option 1", "Option 2", "Option 3" });
int optionIndex = OptionInput.GetOptionIndex("Select an option", new[] { "Option 1", "Option 2", "Option 3" });
bool doSomething = OptionInput.GetYesNoOption("Do you want to do something?");
bool tryAgain = OptionInput.GetEitherOrOption("What do you want to do?", "Try again", "Exit");
```

Alternatively, a dictionary can be used to map descriptions to values.

```csharp
using IOUtils.Input;

Dictionary<string, int> options = new() {
    { "Option 1", 1 },
    { "Option 2", 2 },
    { "Option 3", 3 }
};

int selectedOption = OptionInput.GetOption("Select an option", options);

Console.WriteLine($"Value {selectedOption} returned");

Dictionary<string, Action> actions = new() {
    { "Option 1", () => Console.WriteLine("Option 1 selected") },
    { "Option 2", () => Console.WriteLine("Option 2 selected") },
    { "Option 3", () => Console.WriteLine("Option 3 selected") }
};

OptionInput.GetOption("Select an option", actions)();
```

#### Retrieve file path from the user

FileInput is a class that allows for the retrieval of an existing file or directory path from the user.

```csharp
using IOUtils.Input;

string filePath = FileInput.GetFilePath("Enter a file path");
string directoryPath = FileInput.GetDirectoryPath("Enter a directory path");
```

### Encoding

#### Available encodings

The following encoders are available:

| Encoder                  | Character set       |
|--------------------------|---------------------|
| Base2Encoder             | 0-1                 |
| Base10Encoder            | 0-9                 |
| Base16Encoder            | 0-9, A-F            |
| Base36Encoder            | 0-9, A-Z            |
| Base64Encoder            | A-Z, a-z, 0-9, +, / |
| Base64Encoder (URI safe) | A-Z, a-z, 0-9, -, _ |

#### Encode text

Encode a byte array using one of the available encoders. The encoded text is returned as a string. 

```csharp
using IOUtils.Encoders;

byte[] raw = "Hello, World!"u8.ToArray();

// Any encoder listed above can be used here
string encoded = Base36Encoder.Encode(raw);

Console.WriteLine($"Encoded text: {encoded}");
```

#### Decode text

Decode a string encoded using one of the available encoders. The decoded text is returned as a byte array. If the encoded text is not valid, a FormatException is thrown.

```csharp
using IOUtils.Encoders;

string encoded = "FG3H7VQW7EEN6JWWNZMP";

byte[] decoded = Base36Encoder.Decode(encoded);

Console.WriteLine($"Decoded text: {System.Text.Encoding.UTF8.GetString(decoded)}");
```

## Contributions

Contributions are welcome! Read
the [CONTRIBUTING](https://github.com/Thomas-Shephard/io-utils/blob/main/CONTRIBUTING.md) guide for information.

## License

This project is licensed under the MIT License. See
the [LICENSE](https://github.com/Thomas-Shephard/io-utils/blob/main/LICENSE) for details.