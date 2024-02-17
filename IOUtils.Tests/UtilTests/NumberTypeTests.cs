using IOUtils.Utils;
using NUnit.Framework;

namespace IOUtils.Tests.UtilTests;

public class NumberTypeTests {
    [TestCase(3.0d)]
    [TestCase(99.99)]
    [TestCase(1.0F)]
    [TestCase(Math.PI)]
    [TestCase(0.001F)]
    public void NumberTypeUtils_IsFloatingPoint_TrueForFloatingPoint<T>(T value) {
        Assert.That(NumberTypeUtils.IsFloatingPoint<T>(), Is.True);
    }

    [TestCase(1)]
    [TestCase(99L)]
    [TestCase(3U)]
    [TestCase(0)]
    [TestCase(int.MaxValue)]
    public void NumberTypeUtils_IsFloatingPoint_FalseForInteger<T>(T value) {
        Assert.That(NumberTypeUtils.IsFloatingPoint<T>(), Is.False);
    }
}