using IOUtils.Utils;

namespace IOUtils.Tests.UtilTests.ComparisonTests;

public class GreaterThanTests : BaseComparisonTests {
    protected override bool ShouldBeTrueWhenGreaterThan => true;
    protected override bool ShouldBeTrueWhenEqualTo => false;
    protected override bool ShouldBeTrueWhenLessThan => false;

    protected override Func<T, T, bool> GetComparer<T>() {
        return ComparisonUtils.GreaterThan<T>;
    }
}