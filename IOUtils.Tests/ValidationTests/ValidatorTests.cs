using IOUtils.Validation;
using NUnit.Framework;

namespace IOUtils.Tests.ValidationTests;

public class ValidatorTests {
    private const string PositiveValueErrorMessage = "Value must be positive";
    private static readonly Validator<int> PositiveValueValidator = new(value => value > 0, PositiveValueErrorMessage);

    [TestCase(1)]
    [TestCase(3)]
    [TestCase(99)]
    public void Validator_Validate_TrueForValidValue(int value) {
        MockProvider mockProvider = new();

        bool actualValue = PositiveValueValidator.Validate(value, mockProvider);

        Assert.Multiple(() => {
            Assert.That(actualValue, Is.True);
            Assert.That(mockProvider.OutputLines, Is.EquivalentTo(Array.Empty<string>()));
        });
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-99)]
    public void Validator_Validate_FalseForInvalidValue(int value) {
        MockProvider mockProvider = new();

        bool actualValue = PositiveValueValidator.Validate(value, mockProvider);

        Assert.Multiple(() => {
            Assert.That(actualValue, Is.False);
            Assert.That(mockProvider.OutputLines, Is.EquivalentTo(new[] { PositiveValueErrorMessage }));
        });
    }
}