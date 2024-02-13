using System.Numerics;
using IOUtils.Providers;
using IOUtils.Utils;

namespace IOUtils.Input;

public static class NumericalInput<T> where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
    public static T Get(string question, T? min = null, T? max = null, IProvider? provider = null) {
        if (min is not null && max is not null && min.Value.GreaterThan(max.Value))
            throw new ArgumentException("The minimum value cannot be greater than the maximum value");

        provider ??= IProvider.Default;
        provider.WriteLine(question);

        string? errorMessage = null;
        T input;
        // Keep asking for input until the input is valid and within the specified range
        while (!(T.TryParse(provider.ReadLine(), null, out input) && input.WithinRange(min ?? T.MinValue, max ?? T.MaxValue))) {
            // If the input is not valid, display an error message
            errorMessage ??= GetErrorMessage(min, max);
            provider.WriteLine(errorMessage);
        }

        return input;
    }

    private static string GetErrorMessage(T? min, T? max) {
        string typeDescription = NumericalTypeUtils.IsFloatingPoint<T>() ? "decimal number" : "whole number";

        // Display the error message based on the provided min and max values
        return $"That was not valid, enter a {typeDescription}" + (min, max) switch {
            (null, null)                              => string.Empty,
            (null, not null)                          => $" less than or equal to {max}",
            (not null, null)                          => $" greater than or equal to {min}",
            (not null, not null) when min.Equals(max) => $" of {min}",
            (not null, not null)                      => $" between {min} and {max}"
        };
    }
}