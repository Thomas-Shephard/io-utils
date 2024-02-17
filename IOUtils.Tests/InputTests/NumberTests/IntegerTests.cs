using System.Numerics;
using IOUtils.Input;
using NUnit.Framework;

namespace IOUtils.Tests.InputTests.NumberTests;

public class IntegerTests {
    private const string Question = "Enter a whole number";
    private const string ErrorMessage = "That was not valid, enter a whole number";

    [TestCase("3", 3U)]
    [TestCase("0", 0L)]
    [TestCase("-4", -4)]
    [TestCase("6", 6L)]
    [TestCase("8", 8UL)]
    public void NumberInput_Integer_ValidInput_ReturnsValue<T>(string input, T expected) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        T actual = NumberInput<T>.GetNumber(Question, provider: mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("3.5")]
    [TestCase("abc")]
    [TestCase("-2")]
    [TestCase("3+2")]
    [TestCase("Pi")]
    [TestCase("")]
    [TestCase(null)]
    public void NumberInput_Integer_InvalidInput_ErrorOutput(string input) {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumberInput<ulong>.GetNumber(Question, provider: mockProvider));

        string[] expected = {
            Question,
            ErrorMessage
        };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase("3", 0U, 3U)]
    [TestCase("-3", -3, -3)]
    [TestCase("7", 2UL, 7UL)]
    [TestCase("-342432", -342433L, -342432L)]
    [TestCase("100", 99, 100)]
    public void NumberInput_Integer_MinRange_ValidInput_ReturnsValue<T>(string input, T min, T expected) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        T actual = NumberInput<T>.GetNumber(Question, min, provider: mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("3", 4U)]
    [TestCase("2", 3UL)]
    [TestCase("-2", -1L)]
    [TestCase("-2-5", 0)]
    [TestCase("99", 100L)]
    public void NumberInput_Integer_MinRange_InvalidInput_ErrorOutput<T>(string input, T min) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumberInput<T>.GetNumber(Question, min, provider: mockProvider));

        string[] expected = {
            Question,
            $"{ErrorMessage} greater than or equal to {min}"
        };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase("3", 4, 3)]
    [TestCase("-3", -3L, -3L)]
    [TestCase("4", 5U, 4U)]
    [TestCase("+0", 8UL, 0UL)]
    [TestCase("100", 101U, 100U)]
    public void NumberInput_Integer_MaxRange_ValidInput_ReturnsValue<T>(string input, T max, T expected) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        T actual = NumberInput<T>.GetNumber(Question, max: max, provider: mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("3", -50)]
    [TestCase("2", 1)]
    [TestCase("3", 2UL)]
    [TestCase("-2", -3L)]
    [TestCase("99+1", 99U)]
    public void NumberInput_Integer_MaxRange_InvalidInput_ErrorOutput<T>(string input, T max) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumberInput<T>.GetNumber(Question, max: max, provider: mockProvider));

        string[] expected = {
            Question,
            $"{ErrorMessage} less than or equal to {max}"
        };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase("3", 3, 5, 3)]
    [TestCase("-3", -3L, 3L, -3L)]
    [TestCase("+3", 3UL, 4UL, 3UL)]
    [TestCase("0", -1, 1, 0)]
    [TestCase("100", 99UL, 100UL, 100UL)]
    public void NumberInput_Integer_WithinRange_ValidInput_ReturnsValue<T>(string input, T min, T max, T expected) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        T actual = NumberInput<T>.GetNumber(Question, min, max, mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("3", 4U, 5U)]
    [TestCase("-3", -5L, -4L)]
    [TestCase("-7", -6, -4)]
    [TestCase("6", 7U, 8U)]
    [TestCase("100", 98, 99)]
    public void NumberInput_Integer_WithinRange_InvalidInput_ErrorOutput<T>(string input, T min, T max) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumberInput<T>.GetNumber(Question, min, max, mockProvider));

        string[] expected = {
            Question,
            $"{ErrorMessage} between {min} and {max}"
        };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase("3", 4)]
    [TestCase("-3", -4)]
    [TestCase("abc", 4U)]
    [TestCase("0", 1L)]
    [TestCase("-4", -3L)]
    public void NumberInput_Integer_ExactValue_InvalidInput_ErrorOutput<T>(string input, T minAndMax) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumberInput<T>.GetNumber(Question, minAndMax, minAndMax, mockProvider));

        string[] expected = {
            Question,
            $"{ErrorMessage} of {minAndMax}"
        };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase(5, 3)]
    [TestCase(-3, -5)]
    [TestCase(7UL, 6UL)]
    [TestCase(100L, 99L)]
    [TestCase(0, -1)]
    public void NumberInput_Integer_MinGreaterThanMax_ThrowsException<T>(T min, T max) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        Assert.Throws<ArgumentException>(() => NumberInput<T>.GetNumber(Question, min, max));
    }
}