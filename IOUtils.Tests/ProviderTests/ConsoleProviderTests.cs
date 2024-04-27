using IOUtils.Input;
using IOUtils.Providers;
using NUnit.Framework;

namespace IOUtils.Tests.ProviderTests;

public class ConsoleProviderTests {
    private const string TestFilePath = "console-provider-tests.txt";
    private const string TestMessage = "Hello, World!";

    [Test]
    public void ConsoleProvider_WriteLine_WritesToConsole() {
        IProvider provider = IProvider.Default;

        using (StreamWriter writer = new(TestFilePath)) {
            Console.SetOut(writer);
            provider.WriteLine(TestMessage);
        }

        Assert.That(File.ReadAllText(TestFilePath), Is.EqualTo($"{TestMessage}{Console.Out.NewLine}"));
    }

    [Test]
    public void ConsoleProvider_ReadLine_ReadsFromConsole() {
        IProvider provider = IProvider.Default;

        File.WriteAllText(TestFilePath, TestMessage);

        using StringReader reader = new(TestMessage);
        Console.SetIn(reader);
        Assert.That(provider.ReadLine(), Is.EqualTo(TestMessage));
    }

    [Test]
    public void ConsoleProvider_LastInput_ReturnsNullIfNoInput() {
        // Create a new ConsoleProvider instance to ensure that the LastInput property is null
        IProvider provider = new ConsoleProvider();

        Assert.That(provider.LastInput, Is.Null);
    }

    [Test]
    public void ConsoleProvider_LastInput_ReturnsLastInput() {
        IProvider provider = IProvider.Default;

        using StringReader reader = new(TestMessage);
        Console.SetIn(reader);

        provider.ReadLine();

        Assert.That(provider.LastInput, Is.EqualTo(TestMessage));
    }

    [Test]
    public void ConsoleProvider_TextInput_GetTextFromConsole() {
        using StreamWriter writer = new(TestFilePath);
        Console.SetOut(writer);
        Console.SetIn(new StringReader(TestMessage));

        string input = TextInput.GetText("Enter some text");

        Assert.That(input, Is.EqualTo(TestMessage));
    }

    [Test]
    public void ConsoleProvider_NumberInput_GetNumberFromConsole() {
        using StreamWriter writer = new(TestFilePath);
        Console.SetOut(writer);
        Console.SetIn(new StringReader("42"));

        int input = NumberInput<int>.GetNumber("Enter a number");

        Assert.That(input, Is.EqualTo(42));
    }

    [Test]
    public void ConsoleProvider_OptionalNumberInput_GetNumberFromConsole() {
        using StreamWriter writer = new(TestFilePath);
        Console.SetOut(writer);
        Console.SetIn(new StringReader("12.2"));

        float? input = NumberInput<float>.GetOptionalNumber("Enter a number");

        Assert.That(input, Is.EqualTo(12.2f));
    }
}