using IOUtils.Utils;

namespace IOUtils.Tests.UtilTests.ComparisonTests;

public class LessThanTests : BaseComparisonTests {
    protected override bool ShouldBeTrueWhenGreaterThan => false;
    protected override bool ShouldBeTrueWhenEqualTo => false;
    protected override bool ShouldBeTrueWhenLessThan => true;

    protected override Func<T, T, bool> GetComparer<T>() {
        return ComparisonUtils.LessThan<T>;
    }
}