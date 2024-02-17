using IOUtils.Input;
using NUnit.Framework;

namespace IOUtils.Tests.InputTests.TextTests;

public class TextTests {
    private const string Question = "Example question text";

    [TestCase("123", "123")]
    [TestCase("abc", "abc")]
    [TestCase("Hello, World!", "Hello, World!")]
    [TestCase(null, "")]
    [TestCase("", "")]
    [TestCase(" ", " ")]
    public void TextInput_Text_ValidInput_ReturnsValue(string value, string expected) {
        MockProvider mockProvider = new(value);

        string actual = TextInput.GetText(Question, provider: mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }
}