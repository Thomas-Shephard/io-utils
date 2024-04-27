using IOUtils.Utils;
using NUnit.Framework;

namespace IOUtils.Tests.UtilTests;

public class NumberTypeTests {
    [TestCase(TypeArgs = new[] { typeof(float) })]
    [TestCase(TypeArgs = new[] { typeof(decimal) })]
    [TestCase(TypeArgs = new[] { typeof(double) })]
    public void NumberTypeUtils_IsFloatingPoint_TrueForFloatingPoint<T>() {
        Assert.That(NumberTypeUtils.IsFloatingPoint<T>(), Is.True);
    }

    [TestCase(TypeArgs = new[] { typeof(int) })]
    [TestCase(TypeArgs = new[] { typeof(uint) })]
    [TestCase(TypeArgs = new[] { typeof(long) })]
    [TestCase(TypeArgs = new[] { typeof(ulong) })]
    [TestCase(TypeArgs = new[] { typeof(short) })]
    public void NumberTypeUtils_IsFloatingPoint_FalseForInteger<T>() {
        Assert.That(NumberTypeUtils.IsFloatingPoint<T>(), Is.False);
    }

    [TestCase(TypeArgs = new[] { typeof(string) })]
    [TestCase(TypeArgs = new[] { typeof(byte) })]
    [TestCase(TypeArgs = new[] { typeof(char) })]
    [TestCase(TypeArgs = new[] { typeof(bool) })]
    [TestCase(TypeArgs = new[] { typeof(string[]) })]
    [TestCase(TypeArgs = new[] { typeof(List<double>) })]
    public void NumberTypeUtils_IsFloatingPoint_FalseForOtherTypes<T>() {
        Assert.That(NumberTypeUtils.IsFloatingPoint<T>(), Is.False);
    }
}