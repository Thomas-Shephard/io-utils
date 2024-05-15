using IOUtils.Utils;
using NUnit.Framework;

namespace IOUtils.Tests.UtilTests;

public class NumberTypeTests {
    [TestCase(TypeArgs = [typeof(float)])]
    [TestCase(TypeArgs = [typeof(decimal)])]
    [TestCase(TypeArgs = [typeof(double)])]
    public void NumberTypeUtils_IsFloatingPoint_TrueForFloatingPoint<T>() {
        Assert.That(NumberTypeUtils.IsFloatingPoint<T>(), Is.True);
    }

    [TestCase(TypeArgs = [typeof(int)])]
    [TestCase(TypeArgs = [typeof(uint)])]
    [TestCase(TypeArgs = [typeof(long)])]
    [TestCase(TypeArgs = [typeof(ulong)])]
    [TestCase(TypeArgs = [typeof(short)])]
    public void NumberTypeUtils_IsFloatingPoint_FalseForInteger<T>() {
        Assert.That(NumberTypeUtils.IsFloatingPoint<T>(), Is.False);
    }

    [TestCase(TypeArgs = [typeof(string)])]
    [TestCase(TypeArgs = [typeof(byte)])]
    [TestCase(TypeArgs = [typeof(char)])]
    [TestCase(TypeArgs = [typeof(bool)])]
    [TestCase(TypeArgs = [typeof(string[])])]
    [TestCase(TypeArgs = [typeof(List<double>)])]
    public void NumberTypeUtils_IsFloatingPoint_FalseForOtherTypes<T>() {
        Assert.That(NumberTypeUtils.IsFloatingPoint<T>(), Is.False);
    }
}