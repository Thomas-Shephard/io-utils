using IOUtils.Input;
using NUnit.Framework;

namespace IOUtils.Tests.InputTests.OptionTests;

public class OptionMapTests {
    private const string Question = "Example question text";

    private static readonly Dictionary<string, int> OptionMap = new() {
        { "Value '1'", 1 },
        { "Value '5'", 5 },
        { "Value '10'", 10 }
    };

    [TestCase("1", 1)]
    [TestCase("2", 5)]
    [TestCase("3", 10)]
    public void OptionInput_OptionMap_ValidInput_ReturnsValue(string input, int expected) {
        MockProvider mockProvider = new(input);

        int actual = OptionInput.GetOption(Question, OptionMap, mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("4")]
    [TestCase("0")]
    [TestCase("1.5")]
    [TestCase("1.0")]
    [TestCase("-1")]
    [TestCase("abc")]
    [TestCase("Yes")]
    [TestCase("No")]
    [TestCase("")]
    public void OptionInput_OptionMap_InvalidInput_ErrorOutput(string input) {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => OptionInput.GetOption(Question, OptionMap, mockProvider));

        string expectedQuestion = Question;

        int i = 1;
        foreach (KeyValuePair<string, int> option in OptionMap) {
            expectedQuestion += $"{Environment.NewLine}{i}. {option.Key}";
            i++;
        }

        string expectedErrorMessage = $"That was not valid, enter a whole number between 1 and {OptionMap.Count}";

        string[] expected = { expectedQuestion, expectedErrorMessage };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [Test]
    public void OptionInput_OptionMap_EmptyOptions_ThrowsException() {
        Assert.Throws<ArgumentException>(() => OptionInput.GetOption(Question, new Dictionary<string, int>()));
    }
}