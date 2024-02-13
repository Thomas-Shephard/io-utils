namespace IOUtils.Utils;

internal static class ComparisonUtils {
    internal static bool GreaterThan<T>(this T input, T value) where T : IComparable<T> {
        return input.CompareTo(value) > 0;
    }

    internal static bool GreaterThanOrEqual<T>(this T input, T value) where T : IComparable<T> {
        return input.CompareTo(value) >= 0;
    }

    internal static bool LessThan<T>(this T input, T value) where T : IComparable<T> {
        return !input.GreaterThanOrEqual(value);
    }

    internal static bool LessThanOrEqual<T>(this T input, T value) where T : IComparable<T> {
        return !input.GreaterThan(value);
    }

    internal static bool WithinRange<T>(this T input, T min, T max) where T : IComparable<T> {
        return input.LessThanOrEqual(max) && input.GreaterThanOrEqual(min);
    }
}