using NUnit.Framework;

namespace IOUtils.Tests.UtilTests.ComparisonTests;

public abstract class BaseComparisonTests {
    protected abstract bool ShouldBeTrueWhenGreaterThan { get; }
    protected abstract bool ShouldBeTrueWhenEqualTo { get; }
    protected abstract bool ShouldBeTrueWhenLessThan { get; }

    [TestCase(3, 2)]
    [TestCase(99L, -3L)]
    [TestCase(99.99, 99.989)]
    [TestCase(0.001F, 0.0001F)]
    [TestCase(int.MaxValue, int.MinValue)]
    public void ComparisonUtils_GreaterThan<T>(T first, T second) where T : IComparable<T> {
        Assert.That(GetComparer<T>()(first, second), Is.EqualTo(ShouldBeTrueWhenGreaterThan));
    }

    [TestCase(3, 3)]
    [TestCase(-1L, -1L)]
    [TestCase(Math.PI, Math.PI)]
    [TestCase(-5.6D, -5.6D)]
    [TestCase(0.001F, 0.001F)]
    public void ComparisonUtils_Equal<T>(T first, T second) where T : IComparable<T> {
        Assert.That(GetComparer<T>()(first, second), Is.EqualTo(ShouldBeTrueWhenEqualTo));
    }

    [TestCase(3, 4)]
    [TestCase(-1L, 0L)]
    [TestCase(Math.E, Math.PI)]
    [TestCase(-5.6D, -5.5D)]
    [TestCase(0.001F, 0.002F)]
    public void ComparisonUtils_LessThan<T>(T first, T second) where T : IComparable<T> {
        Assert.That(GetComparer<T>()(first, second), Is.EqualTo(ShouldBeTrueWhenLessThan));
    }

    protected abstract Func<T, T, bool> GetComparer<T>() where T : IComparable<T>;
}