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
}