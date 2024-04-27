using System.Numerics;

namespace IOUtils.Utils;

internal static class NumberTypeUtils {
    private static readonly Dictionary<Type, bool> TypeIsFloatingPoint = new();

    internal static bool IsFloatingPoint<T>() {
        Type type = typeof(T);

        if (TypeIsFloatingPoint.TryGetValue(type, out bool isFloatingPoint)) {
            return isFloatingPoint;
        }

        isFloatingPoint = type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IFloatingPoint<>));
        TypeIsFloatingPoint[type] = isFloatingPoint;
        return isFloatingPoint;
    }
}