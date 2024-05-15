using System.Numerics;
using IOUtils.Input;
using NUnit.Framework;

namespace IOUtils.Tests.InputTests.NumberTests;

public class OptionalFloatTests {
    private const string Question = "Enter a decimal number";
    private const string ErrorMessageType = "That was not valid, enter a decimal number";
    private const string ErrorMessageSkip = "or press enter to skip";

    [TestCase("3", 3.0d)]
    [TestCase("3.000", 3.0f)]
    [TestCase("-2.5", -2.5d)]
    [TestCase("+2.5", 2.5f)]
    public void NumberInput_Float_ValidInput_ReturnsValue<T>(string input, T expected) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        T? actual = NumberInput<T>.GetOptionalNumber(Question, provider: mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void NumberInput_Float_ValidEmptyInput_ReturnsNull() {
        MockProvider mockProvider = new(string.Empty);

        float? actual = NumberInput<float>.GetOptionalNumber(Question, provider: mockProvider);

        Assert.That(actual, Is.Null);
    }

    [TestCase("3.5.5")]
    [TestCase("abc")]
    [TestCase("+-2.8")]
    [TestCase("3+2")]
    [TestCase("Pi")]
    public void NumberInput_Float_InvalidInput_ErrorOutput(string input) {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumberInput<float>.GetOptionalNumber(Question, provider: mockProvider));

        string[] expected = [
            Question,
            $"{ErrorMessageType} {ErrorMessageSkip}"
        ];

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase("3", 0f, 3f)]
    [TestCase("-3", -3f, -3f)]
    [TestCase("3.5", 2f, 3.5f)]
    [TestCase("+2.5", 2.5d, 2.5d)]
    [TestCase("100", 99.99d, 100d)]
    public void NumberInput_Float_MinRange_ValidInput_ReturnsValue<T>(string input, T min, T expected) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        T? actual = NumberInput<T>.GetOptionalNumber(Question, min, provider: mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(0f)]
    [TestCase(-3f)]
    [TestCase(2f)]
    [TestCase(2.5d)]
    [TestCase(99.99d)]
    public void NumberInput_Float_MinRange_ValidEmptyInput_ReturnsNull<T>(T min) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(string.Empty);

        T? actual = NumberInput<T>.GetOptionalNumber(Question, min, provider: mockProvider);

        Assert.That(actual, Is.Null);
    }

    [TestCase("3", 4d)]
    [TestCase("2.5", 3d)]
    [TestCase("3.5", 4.5d)]
    [TestCase("-2-5", 0f)]
    [TestCase("99.99", 100f)]
    public void NumberInput_Float_MinRange_InvalidInput_ErrorOutput<T>(string input, T min) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumberInput<T>.GetOptionalNumber(Question, min, provider: mockProvider));

        string[] expected = [
            Question,
            $"{ErrorMessageType} greater than or equal to {min} {ErrorMessageSkip}"
        ];

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase("3", 4f, 3f)]
    [TestCase("-3", -3d, -3d)]
    [TestCase("3.5", 5f, 3.5f)]
    [TestCase("+2.5", 2.5d, 2.5d)]
    [TestCase("99.99", 100f, 99.99f)]
    public void NumberInput_Float_MaxRange_ValidInput_ReturnsValue<T>(string input, T max, T expected) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        T? actual = NumberInput<T>.GetOptionalNumber(Question, max: max, provider: mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }


    [TestCase(4f)]
    [TestCase(-3d)]
    [TestCase(5f)]
    [TestCase(2.5d)]
    [TestCase(100f)]
    public void NumberInput_Float_MaxRange_ValidEmptyInput_ReturnsNull<T>(T max) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(string.Empty);

        T? actual = NumberInput<T>.GetOptionalNumber(Question, max: max, provider: mockProvider);

        Assert.That(actual, Is.Null);
    }

    [TestCase("3", 2f)]
    [TestCase("2.5", 2f)]
    [TestCase("3.5", 2.5f)]
    [TestCase("-2.5", -5d)]
    [TestCase("99+999", 99.99d)]
    public void NumberInput_Float_MaxRange_InvalidInput_ErrorOutput<T>(string input, T max) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumberInput<T>.GetOptionalNumber(Question, max: max, provider: mockProvider));

        string[] expected = [
            Question,
            $"{ErrorMessageType} less than or equal to {max} {ErrorMessageSkip}"
        ];

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase("3", 3d, 5d, 3d)]
    [TestCase("-3", -3f, 3f, -3f)]
    [TestCase("3.5", 3.5d, 3.5d, 3.5d)]
    [TestCase("+2.5", 2.5f, 3.5f, 2.5f)]
    [TestCase("99.99", 99.98d, 100d, 99.99d)]
    public void NumberInput_Float_WithinRange_ValidInput_ReturnsValue<T>(string input, T min, T max, T expected) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        T? actual = NumberInput<T>.GetOptionalNumber(Question, min, max, mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(3d, 5d)]
    [TestCase(-3f, 3f)]
    [TestCase(3.5d, 3.5d)]
    [TestCase(2.5f, 3.5f)]
    [TestCase(99.98d, 100d)]
    [TestCase(3.5d, 3.5d)]
    public void NumberInput_Float_WithinRange_ValidEmptyInput_ReturnsNull<T>(T min, T max) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(string.Empty);

        T? actual = NumberInput<T>.GetOptionalNumber(Question, min, max, mockProvider);

        Assert.That(actual, Is.Null);
    }

    [TestCase("3", 4f, 5f)]
    [TestCase("-3", -4d, -3.5d)]
    [TestCase("3.5", 4f, 4.5f)]
    [TestCase("+2.5", 3d, 3.5d)]
    [TestCase("99.99", 99.98f, 99.989f)]
    public void NumberInput_Float_WithinRange_InvalidInput_ErrorOutput<T>(string input, T min, T max) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumberInput<T>.GetOptionalNumber(Question, min, max, mockProvider));

        string[] expected = [
            Question,
            $"{ErrorMessageType} between {min} and {max} {ErrorMessageSkip}"
        ];

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase("3", 4d)]
    [TestCase("-3", -4f)]
    [TestCase("3.5", 4.5d)]
    [TestCase("+2.5", 2.7f)]
    [TestCase("99.99", 99.989d)]
    public void NumberInput_Float_ExactValue_InvalidInput_ErrorOutput<T>(string input, T minAndMax) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumberInput<T>.GetOptionalNumber(Question, minAndMax, minAndMax, mockProvider));

        string[] expected = [
            Question,
            $"{ErrorMessageType} of {minAndMax} {ErrorMessageSkip}"
        ];

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase(5f, 3f)]
    [TestCase(-3d, -5d)]
    [TestCase(3.5f, 3.2f)]
    [TestCase(99.99d, 99.989d)]
    [TestCase(0f, -1f)]
    public void NumberInput_Float_MinGreaterThanMax_ThrowsException<T>(T min, T max) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        Assert.Throws<ArgumentException>(() => NumberInput<T>.GetOptionalNumber(Question, min, max));
    }
}