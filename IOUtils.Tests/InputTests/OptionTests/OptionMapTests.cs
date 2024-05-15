using IOUtils.Input;
using NUnit.Framework;

namespace IOUtils.Tests.InputTests.OptionTests;

public class OptionMapTests {
    private const string Question = "Example question text";

    private const string Option1 = "Value '1'";
    private const string Option2 = "Value '5'";
    private const string Option3 = "Value '10'";

    private const int Value1 = 1;
    private const int Value2 = 5;
    private const int Value3 = 10;

    private static readonly Dictionary<string, int> OptionMap = new() {
        { Option1, Value1 },
        { Option2, Value2 },
        { Option3, Value3 }
    };

    [TestCase("1", Value1)]
    [TestCase("2", Value2)]
    [TestCase("3", Value3)]
    public void OptionInput_OptionMap_ValidInput_ReturnsValue(string input, int expected) {
        MockProvider mockProvider = new(input);

        int actual = OptionInput.GetOption(Question, OptionMap, provider: mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("1", Option1, Value1)]
    [TestCase("2", Option1, Value2)]
    [TestCase("3", Option1, Value3)]
    public void OptionInput_OptionMapWithDefault_ValidInput_ReturnsValue(string input, string defaultValue, int expected) {
        MockProvider mockProvider = new(input);

        int actual = OptionInput.GetOption(Question, OptionMap, defaultValue, mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(Option1, Value1)]
    [TestCase(Option2, Value2)]
    [TestCase(Option3, Value3)]
    public void OptionInput_OptionMapWithDefault_ValidEmptyInput_ReturnsDefaultValue(string defaultValue, int expected) {
        MockProvider mockProvider = new(string.Empty);

        int actual = OptionInput.GetOption(Question, OptionMap, defaultValue, mockProvider);

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

        Assert.Throws<InvalidOperationException>(() => OptionInput.GetOption(Question, OptionMap, provider: mockProvider));

        string expectedQuestion = Question;

        int i = 1;
        foreach (KeyValuePair<string, int> option in OptionMap) {
            expectedQuestion += $"{Environment.NewLine}{i}. {option.Key}";
            i++;
        }

        string expectedErrorMessage = $"That was not valid, enter a whole number between 1 and {OptionMap.Count}";

        string[] expected = [expectedQuestion, expectedErrorMessage];

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [Test]
    public void OptionInput_OptionMap_EmptyOptions_ThrowsException() {
        Assert.Throws<ArgumentException>(() => OptionInput.GetOption(Question, new Dictionary<string, int>()));
    }

    [Test]
    public void OptionInput_OptionMapWithDefault_DefaultOptionNotInOptions_ThrowsException() {
        Assert.Throws<ArgumentException>(() => OptionInput.GetOption(Question, OptionMap, "Hello, World!"));
    }
}