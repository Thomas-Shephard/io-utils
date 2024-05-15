using IOUtils.Input;
using NUnit.Framework;

namespace IOUtils.Tests.InputTests.OptionTests;

public class YesNoOptionTests {
    private const string Question = "Example question text";

    [TestCase("1", true)]
    [TestCase("2", false)]
    public void OptionInput_YesNo_ValidInput_ReturnsValue(string input, bool expected) {
        MockProvider mockProvider = new(input);

        bool actual = OptionInput.GetYesNoOption(Question, provider: mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("1", true, true)]
    [TestCase("2", true, false)]
    [TestCase("1", false, true)]
    [TestCase("2", false, false)]
    public void OptionInput_YesNoWithDefault_ValidInput_ReturnsValue(string input, bool defaultValue, bool expected) {
        MockProvider mockProvider = new(input);

        bool actual = OptionInput.GetYesNoOption(Question, defaultValue, mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(true, true)]
    [TestCase(false, false)]
    public void OptionInput_YesNoWithDefault_ValidEmptyInput_ReturnsDefaultValue(bool defaultValue, bool expected) {
        MockProvider mockProvider = new(string.Empty);

        bool actual = OptionInput.GetYesNoOption(Question, defaultValue, mockProvider);

        Assert.That(actual, Is.EqualTo(defaultValue));
    }

    [TestCase("3")]
    [TestCase("1.5")]
    [TestCase("1.0")]
    [TestCase("0")]
    [TestCase("-1")]
    [TestCase("abc")]
    [TestCase("Yes")]
    [TestCase("No")]
    [TestCase("")]
    [TestCase(null)]
    public void OptionInput_YesNo_InvalidInput_ErrorOutput(string? input) {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => OptionInput.GetYesNoOption(Question, provider: mockProvider));

        string[] expected = [
            $"""
             {Question}
             1. Yes
             2. No
             """,
            "That was not valid, enter a whole number between 1 and 2"
        ];

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }
}