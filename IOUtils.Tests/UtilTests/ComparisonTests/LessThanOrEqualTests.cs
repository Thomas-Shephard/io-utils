using IOUtils.Utils;

namespace IOUtils.Tests.UtilTests.ComparisonTests;

public class LessThanOrEqualTests : BaseComparisonTests {
    protected override bool ShouldBeTrueWhenGreaterThan => false;
    protected override bool ShouldBeTrueWhenEqualTo => true;
    protected override bool ShouldBeTrueWhenLessThan => true;

    protected override Func<T, T, bool> GetComparer<T>() {
        return ComparisonUtils.LessThanOrEqual<T>;
    }
}