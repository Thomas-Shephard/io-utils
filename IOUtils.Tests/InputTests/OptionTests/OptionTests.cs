using IOUtils.Input;
using NUnit.Framework;

namespace IOUtils.Tests.InputTests.OptionTests;

public class OptionTests {
    private const string Question = "Example question text";

    [TestCase("1", new[] { "Option 1", "Option 2" }, "Option 1")]
    [TestCase("2", new[] { "Option 1", "Option 2" }, "Option 2")]
    [TestCase("3", new[] { "Option 1", "Option 2", "Option 3" }, "Option 3")]
    [TestCase("4", new[] { "Option 1", "Option 2", "Option 3", "Option 4" }, "Option 4")]
    [TestCase("3", new[] { "Option 1", "Option 2", "Option 3", "Option 4", "Option 5" }, "Option 3")]
    public void OptionInput_Options_ValidInput_ReturnsValue(string input, string[] options, string expected) {
        MockProvider mockProvider = new(input);

        string actual = OptionInput.GetOption(Question, options, mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("6", new[] { "Option 1", "Option 2" })]
    [TestCase("0", new[] { "Option 1", "Option 2" })]
    [TestCase("1.5", new[] { "Option 1", "Option 2" })]
    [TestCase("1.0", new[] { "Option 1", "Option 2", "Option 3" })]
    [TestCase("0", new[] { "Option 1", "Option 2", "Option 3", "Option 4" })]
    [TestCase("-1", new[] { "Option 1", "Option 2", "Option 3" })]
    [TestCase("abc", new[] { "Option 1", "Option 2" })]
    [TestCase("Yes", new[] { "Option 1", "Option 2", "Option 3" })]
    [TestCase("No", new[] { "Option 1", "Option 2" })]
    [TestCase("", new[] { "Option 1", "Option 2", "Option 3", "Option 4" })]
    public void OptionInput_Options_InvalidInput_ErrorOutput(string input, string[] options) {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => OptionInput.GetOption(Question, options, mockProvider));

        string expectedQuestion = Question;

        for (int i = 0; i < options.Length; i++) {
            expectedQuestion += $"{Environment.NewLine}{i + 1}. {options[i]}";
        }

        string expectedErrorMessage = $"That was not valid, enter a whole number between 1 and {options.Length}";

        string[] expected = { expectedQuestion, expectedErrorMessage };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [Test]
    public void OptionInput_Options_EmptyOptions_ThrowsException() {
        Assert.Throws<ArgumentException>(() => OptionInput.GetOption(Question, Array.Empty<string>()));
    }
}