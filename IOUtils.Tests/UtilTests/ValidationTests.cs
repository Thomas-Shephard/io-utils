using IOUtils.Utils;
using IOUtils.Validation;
using NUnit.Framework;

namespace IOUtils.Tests.UtilTests;

public class ValidationTests {
    private const string PositiveValueErrorMessage = "Value must be positive";
    private const string Sub100ErrorMessage = "Value must be less than 100";
    private static readonly Validator<int> PositiveValueValidator = new(value => value > 0, PositiveValueErrorMessage);
    private static readonly Validator<int> Sub100Validator = new(value => value < 100, Sub100ErrorMessage);
    private static readonly Validator<int>[] Validators = [PositiveValueValidator, Sub100Validator];

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(99)]
    [TestCase(100)]
    public void ValidationUtils_Validate_TrueForNullValidators(int value) {
        bool actualValue = ValidationUtils.Validate(null, value, out List<string>? errorMessages);

        Assert.Multiple(() => {
            Assert.That(actualValue, Is.True);
            Assert.That(errorMessages, Is.Empty);
        });
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(99)]
    [TestCase(100)]
    public void ValidationUtils_Validate_TrueForEmptyValidators(int value) {
        bool actualValue = Array.Empty<Validator<int>>().Validate(value, out List<string>? errorMessages);

        Assert.Multiple(() => {
            Assert.That(actualValue, Is.True);
            Assert.That(errorMessages, Is.Empty);
        });
    }

    [TestCase(1)]
    [TestCase(50)]
    [TestCase(99)]
    public void ValidationUtils_Validate_TrueForValidValue(int value) {
        bool actualValue = Validators.Validate(value, out List<string>? errorMessages);

        Assert.Multiple(() => {
            Assert.That(actualValue, Is.True);
            Assert.That(errorMessages, Is.Empty);
        });
    }

    [TestCase(-50)]
    [TestCase(-1)]
    [TestCase(0)]
    public void ValidationUtils_Validate_FalseForNonPositiveValue(int value) {
        bool actualValue = Validators.Validate(value, out List<string>? errorMessages);

        Assert.Multiple(() => {
            Assert.That(actualValue, Is.False);
            Assert.That(errorMessages, Is.EquivalentTo(new[] { PositiveValueErrorMessage }));
        });
    }

    [TestCase(100)]
    [TestCase(101)]
    [TestCase(150)]
    public void ValidationUtils_Validate_FalseForNonSub100Value(int value) {
        bool actualValue = Validators.Validate(value, out List<string>? errorMessages);

        Assert.Multiple(() => {
            Assert.That(actualValue, Is.False);
            Assert.That(errorMessages, Is.EquivalentTo(new[] { Sub100ErrorMessage }));
        });
    }
}