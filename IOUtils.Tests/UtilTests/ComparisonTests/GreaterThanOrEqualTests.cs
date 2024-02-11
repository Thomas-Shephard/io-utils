using IOUtils.Utils;

namespace IOUtils.Tests.UtilTests.ComparisonTests;

public class GreaterThanOrEqualTests : BaseComparisonTests {
    protected override bool ShouldBeTrueWhenGreaterThan => true;
    protected override bool ShouldBeTrueWhenEqualTo => true;
    protected override bool ShouldBeTrueWhenLessThan => false;

    protected override Func<T, T, bool> GetComparer<T>() {
        return ComparisonUtils.GreaterThanOrEqual<T>;
    }
}