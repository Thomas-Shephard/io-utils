using System.Numerics;

namespace IOUtils.Utils;

internal static class NumericalTypeUtils {
    private static readonly Dictionary<string, bool> TypeIsFloatingPoint = new();

    internal static bool IsFloatingPoint<T>() {
        Type type = typeof(T);

        string typeName = type.FullName ?? throw new ArgumentNullException();

        if (TypeIsFloatingPoint.TryGetValue(typeName, out bool isFloatingPoint)) {
            return isFloatingPoint;
        }

        isFloatingPoint = type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IFloatingPoint<>));
        TypeIsFloatingPoint[typeName] = isFloatingPoint;
        return isFloatingPoint;
    }
}