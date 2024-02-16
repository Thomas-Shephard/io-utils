using IOUtils.Utils;
using IOUtils.Validation;
using NUnit.Framework;

namespace IOUtils.Tests.UtilTests;

public class ValidationTests {
    private const string PositiveValueErrorMessage = "Value must be positive";
    private const string Sub100ErrorMessage = "Value must be less than 100";
    private static readonly Validator<int> PositiveValueValidator = new(value => value > 0, PositiveValueErrorMessage);
    private static readonly Validator<int> Sub100Validator = new(value => value < 100, Sub100ErrorMessage);
    private static readonly Validator<int>[] Validators = { PositiveValueValidator, PositiveValueValidator, Sub100Validator };

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(99)]
    [TestCase(100)]
    public void ValidationUtils_Validate_TrueForNullValidators(int value) {
        MockProvider mockProvider = new();

        bool actualValue = ValidationUtils.Validate(null, value, mockProvider);

        Assert.Multiple(() => {
            Assert.That(actualValue, Is.True);
            Assert.That(mockProvider.OutputLines, Is.EquivalentTo(Array.Empty<string>()));
        });
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(99)]
    [TestCase(100)]
    public void ValidationUtils_Validate_TrueForEmptyValidators(int value) {
        MockProvider mockProvider = new();

        bool actualValue = Array.Empty<Validator<int>>().Validate(value, mockProvider);

        Assert.Multiple(() => {
            Assert.That(actualValue, Is.True);
            Assert.That(mockProvider.OutputLines, Is.EquivalentTo(Array.Empty<string>()));
        });
    }

    [TestCase(1)]
    [TestCase(50)]
    [TestCase(99)]
    public void ValidationUtils_Validate_TrueForValidValue(int value) {
        MockProvider mockProvider = new();

        bool actualValue = Validators.Validate(value, mockProvider);

        Assert.Multiple(() => {
            Assert.That(actualValue, Is.True);
            Assert.That(mockProvider.OutputLines, Is.EquivalentTo(Array.Empty<string>()));
        });
    }

    [TestCase(-50)]
    [TestCase(-1)]
    [TestCase(0)]
    public void ValidationUtils_Validate_FalseForNonPositiveValue(int value) {
        MockProvider mockProvider = new();

        bool actualValue = Validators.Validate(value, mockProvider);

        Assert.Multiple(() => {
            Assert.That(actualValue, Is.False);
            Assert.That(mockProvider.OutputLines, Is.EquivalentTo(new[] { PositiveValueErrorMessage }));
        });
    }

    [TestCase(100)]
    [TestCase(101)]
    [TestCase(150)]
    public void ValidationUtils_Validate_FalseForNonSub100Value(int value) {
        MockProvider mockProvider = new();

        bool actualValue = Validators.Validate(value, mockProvider);

        Assert.Multiple(() => {
            Assert.That(actualValue, Is.False);
            Assert.That(mockProvider.OutputLines, Is.EquivalentTo(new[] { Sub100ErrorMessage }));
        });
    }
}