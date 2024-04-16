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
        bool actualValue = PositiveValueValidator.Validate(value, out string? errorMessage);

        Assert.Multiple(() => {
            Assert.That(actualValue, Is.True);
            Assert.That(errorMessage, Is.Null);
        });
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-99)]
    public void Validator_Validate_FalseForInvalidValue(int value) {
        bool actualValue = PositiveValueValidator.Validate(value, out string? errorMessage);

        Assert.Multiple(() => {
            Assert.That(actualValue, Is.False);
            Assert.That(errorMessage, Is.EqualTo(PositiveValueErrorMessage));
        });
    }
}