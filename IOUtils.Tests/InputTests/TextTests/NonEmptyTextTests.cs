using IOUtils.Input;
using NUnit.Framework;

namespace IOUtils.Tests.InputTests.TextTests;

public class NonEmptyTextTests {
    private const string Question = "Example question text";

    [TestCase("123", "123")]
    [TestCase("abc", "abc")]
    [TestCase("Hello, World!", "Hello, World!")]
    public void TextInput_NonEmptyText_ValidInput_ReturnsValue(string value, string expected) {
        MockProvider mockProvider = new(value);

        string actual = TextInput.GetNonEmptyText(Question, mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void TextInput_NonEmptyText_InvalidInput_ErrorOutput(string? value) {
        MockProvider mockProvider = new(value);

        Assert.Throws<InvalidOperationException>(() => TextInput.GetNonEmptyText(Question, mockProvider));

        string[] expected = {
            Question,
            "That was not valid, enter some text"
        };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }
}