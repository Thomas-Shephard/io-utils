using System.Numerics;
using IOUtils.Utils;
using NUnit.Framework;

namespace IOUtils.Tests.UtilTests.ComparisonTests;

public class WithinRangeTests {
    [TestCase(3)]
    [TestCase(-99L)]
    [TestCase(99.99)]
    [TestCase(int.MaxValue)]
    [TestCase(float.MinValue)]
    public void ComparisonUtils_WithinRange_NoBounds_Valid<T>(T value) where T : IComparable<T>, IMinMaxValue<T> {
        Assert.That(value.WithinRange(T.MinValue, T.MaxValue), Is.True);
    }

    [TestCase(3, 2)]
    [TestCase(99L, -3L)]
    [TestCase(99.99, 99.989)]
    [TestCase(0.001F, 0.0001F)]
    [TestCase(int.MaxValue, int.MinValue)]
    [TestCase(3, 3)]
    [TestCase(-1L, -1L)]
    [TestCase(Math.PI, Math.PI)]
    [TestCase(-5.6D, -5.6D)]
    [TestCase(0.001F, 0.001F)]
    public void ComparisonUtils_WithinRange_MinBoundOnly_Valid<T>(T value, T min) where T : IComparable<T>, IMinMaxValue<T> {
        Assert.That(value.WithinRange(min, T.MaxValue), Is.True);
    }

    [TestCase(3, 4)]
    [TestCase(-1L, 0L)]
    [TestCase(Math.E, Math.PI)]
    [TestCase(-5.6D, -5.5D)]
    [TestCase(0.001F, 0.002F)]
    public void ComparisonUtils_WithinRange_MinBoundOnly_Invalid<T>(T value, T min) where T : IComparable<T>, IMinMaxValue<T> {
        Assert.That(value.WithinRange(min, T.MaxValue), Is.False);
    }

    [TestCase(3, 4)]
    [TestCase(-1L, 0L)]
    [TestCase(Math.E, Math.PI)]
    [TestCase(-5.6D, -5.5D)]
    [TestCase(0.001F, 0.002F)]
    [TestCase(3, 3)]
    [TestCase(-1L, -1L)]
    [TestCase(Math.PI, Math.PI)]
    [TestCase(-5.6D, -5.6D)]
    [TestCase(0.001F, 0.001F)]
    public void ComparisonUtils_WithinRange_MaxBoundOnly_Valid<T>(T value, T max) where T : IComparable<T>, IMinMaxValue<T> {
        Assert.That(value.WithinRange(T.MinValue, max), Is.True);
    }

    [TestCase(3, 2)]
    [TestCase(99L, -3L)]
    [TestCase(99.99, 99.989)]
    [TestCase(0.001F, 0.0001F)]
    [TestCase(int.MaxValue, int.MinValue)]
    public void ComparisonUtils_WithinRange_MaxBoundOnly_Invalid<T>(T value, T max) where T : IComparable<T>, IMinMaxValue<T> {
        Assert.That(value.WithinRange(T.MinValue, max), Is.False);
    }

    [TestCase(2, 1, 3)]
    [TestCase(-3L, -99L, 99L)]
    [TestCase(99.99, 99.989, 100.0)]
    [TestCase(0.01F, 0.001F, 0.01F)]
    [TestCase(Math.PI, Math.PI, Math.PI)]
    public void ComparisonUtils_WithinRange_BothBounds_Valid<T>(T value, T min, T max) where T : IComparable<T>, IMinMaxValue<T> {
        Assert.That(value.WithinRange(min, max), Is.True);
    }

    [TestCase(3, 1, 2)]
    [TestCase(-3L, -99L, -4L)]
    [TestCase(99.989, 99.99, 100.0)]
    [TestCase(0.1F, 0.001F, 0.01F)]
    [TestCase(Math.E, Math.PI, Math.Tau)]
    public void ComparisonUtils_WithinRange_BothBounds_Invalid<T>(T value, T min, T max) where T : IComparable<T>, IMinMaxValue<T> {
        Assert.That(value.WithinRange(min, max), Is.False);
    }

    [TestCase(1, 2, 1)]
    [TestCase(-8L, -4L, -99L)]
    [TestCase(50, 99, -100)]
    [TestCase(0.01F, 0.01F, 0.001F)]
    [TestCase(Math.E, Math.PI, Math.E)]
    public void ComparisonUtils_WithinRange_MinGreaterThanMax_ThrowsException<T>(T value, T min, T max) where T : IComparable<T>, IMinMaxValue<T> {
        Assert.Throws<ArgumentException>(() => value.WithinRange(min, max));
    }
}