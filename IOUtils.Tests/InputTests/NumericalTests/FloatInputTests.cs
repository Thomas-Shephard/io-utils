using System.Numerics;
using IOUtils.Input;
using NUnit.Framework;

namespace IOUtils.Tests.InputTests.NumericalTests;

public class FloatInputTests {
    private const string Question = "Enter a decimal number";
    private const string ErrorMessage = "That was not valid, enter a decimal number";

    [TestCase("3", 3.0d)]
    [TestCase("3.000", 3.0f)]
    [TestCase("3.5", 3.5f)]
    [TestCase("-2.5", -2.5d)]
    [TestCase("+2.5", 2.5f)]
    public void NumericalInput_Float_ValidInput_ReturnsValue<T>(string input, T expected) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        T actual = NumericalInput<T>.GetInput(Question, provider: mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("3.5.5")]
    [TestCase("abc")]
    [TestCase("+-2.8")]
    [TestCase("3+2")]
    [TestCase("Pi")]
    [TestCase("")]
    [TestCase(null)]
    public void NumericalInput_Float_InvalidInput_ErrorOutput(string input) {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumericalInput<float>.GetInput(Question, provider: mockProvider));

        string[] expected = {
            Question,
            ErrorMessage
        };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase("3", 0f, 3f)]
    [TestCase("-3", -3f, -3f)]
    [TestCase("3.5", 2f, 3.5f)]
    [TestCase("+2.5", 2.5d, 2.5d)]
    [TestCase("100", 99.99d, 100d)]
    public void NumericalInput_Float_MinRange_ValidInput_ReturnsValue<T>(string input, T min, T expected) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        T actual = NumericalInput<T>.GetInput(Question, min, provider: mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("3", 4d)]
    [TestCase("2.5", 3d)]
    [TestCase("3.5", 4.5d)]
    [TestCase("-2-5", 0f)]
    [TestCase("99.99", 100f)]
    public void NumericalInput_Float_MinRange_InvalidInput_ErrorOutput<T>(string input, T min) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumericalInput<T>.GetInput(Question, min, provider: mockProvider));

        string[] expected = {
            Question,
            $"{ErrorMessage} greater than or equal to {min}"
        };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase("3", 4f, 3f)]
    [TestCase("-3", -3d, -3d)]
    [TestCase("3.5", 5f, 3.5f)]
    [TestCase("+2.5", 2.5d, 2.5d)]
    [TestCase("99.99", 100f, 99.99f)]
    public void NumericalInput_Float_MaxRange_ValidInput_ReturnsValue<T>(string input, T max, T expected) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        T actual = NumericalInput<T>.GetInput(Question, max: max, provider: mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("3", 2f)]
    [TestCase("2.5", 2f)]
    [TestCase("3.5", 2.5f)]
    [TestCase("-2.5", -5d)]
    [TestCase("99+999", 99.99d)]
    public void NumericalInput_Float_MaxRange_InvalidInput_ErrorOutput<T>(string input, T max) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumericalInput<T>.GetInput(Question, max: max, provider: mockProvider));

        string[] expected = {
            Question,
            $"{ErrorMessage} less than or equal to {max}"
        };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase("3", 3d, 5d, 3d)]
    [TestCase("-3", -3f, 3f, -3f)]
    [TestCase("3.5", 3.5d, 3.5d, 3.5d)]
    [TestCase("+2.5", 2.5f, 3.5f, 2.5f)]
    [TestCase("99.99", 99.98d, 100d, 99.99d)]
    public void NumericalInput_Float_WithinRange_ValidInput_ReturnsValue<T>(string input, T min, T max, T expected) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        T actual = NumericalInput<T>.GetInput(Question, min, max, mockProvider);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("3", 4f, 5f)]
    [TestCase("-3", -4d, -3.5d)]
    [TestCase("3.5", 4f, 4.5f)]
    [TestCase("+2.5", 3d, 3.5d)]
    [TestCase("99.99", 99.98f, 99.989f)]
    public void NumericalInput_Float_WithinRange_InvalidInput_ErrorOutput<T>(string input, T min, T max) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumericalInput<T>.GetInput(Question, min, max, mockProvider));

        string[] expected = {
            Question,
            $"{ErrorMessage} between {min} and {max}"
        };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase("3", 4d)]
    [TestCase("-3", -4f)]
    [TestCase("3.5", 4.5d)]
    [TestCase("+2.5", 2.7f)]
    [TestCase("99.99", 99.989d)]
    public void NumericalInput_Float_ExactValue_InvalidInput_ErrorOutput<T>(string input, T minAndMax) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => NumericalInput<T>.GetInput(Question, minAndMax, minAndMax, mockProvider));

        string[] expected = {
            Question,
            $"{ErrorMessage} of {minAndMax}"
        };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }

    [TestCase(5f, 3f)]
    [TestCase(-3d, -5d)]
    [TestCase(3.5f, 3.2f)]
    [TestCase(99.99d, 99.989d)]
    [TestCase(0f, -1f)]
    public void NumericalInput_Float_MinGreaterThanMax_ThrowsException<T>(T min, T max) where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
        Assert.Throws<ArgumentException>(() => NumericalInput<T>.GetInput(Question, min, max));
    }
}