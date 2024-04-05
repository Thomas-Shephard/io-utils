using System.Numerics;
using IOUtils.Providers;
using IOUtils.Utils;

namespace IOUtils.Input;

public static class NumberInput<T> where T : struct, INumberBase<T>, IComparable<T>, IMinMaxValue<T> {
    public static T GetNumber(string question, T? min = null, T? max = null, IProvider? provider = null) {
        ValidateMinMax(min, max);

        provider ??= IProvider.Default;
        provider.WriteLine(question);

        string? errorMessage = null;
        T input;
        // Keep asking for input until the input is valid and within the specified range
        while (!(T.TryParse(provider.ReadLine(), null, out input) && input.WithinRange(min ?? T.MinValue, max ?? T.MaxValue))) {
            // If the input is not valid, display an error message
            errorMessage ??= GetErrorMessage(min, max, false);
            provider.WriteLine(errorMessage);
        }

        return input;
    }

    public static T? GetOptionalNumber(string question, T? min = null, T? max = null, IProvider? provider = null) {
        ValidateMinMax(min, max);

        provider ??= IProvider.Default;
        provider.WriteLine(question);

        string? errorMessage = null;
        T input;
        // Keep asking for input until the input is valid and within the specified range or the input is empty
        while (!(T.TryParse(provider.ReadLine(), null, out input) && input.WithinRange(min ?? T.MinValue, max ?? T.MaxValue))) {
            // If the input is empty, return null
            if (string.IsNullOrWhiteSpace(provider.LastInput))
                return null;

            // If the input is not valid, display an error message
            errorMessage ??= GetErrorMessage(min, max, true);
            provider.WriteLine(errorMessage);
        }

        return input;
    }

    private static void ValidateMinMax(T? min, T? max) {
        if (min is not null && max is not null && min.Value.GreaterThan(max.Value))
            throw new ArgumentException("The minimum value cannot be greater than the maximum value");
    }

    private static string GetErrorMessage(T? min, T? max, bool allowEmptyResponse) {
        string typeDescription = NumberTypeUtils.IsFloatingPoint<T>() ? "decimal number" : "whole number";

        // Create an error message based on the provided min and max values
        string numberErrorMessage = $"That was not valid, enter a {typeDescription}" + (min, max) switch {
            (null, null)                              => string.Empty,
            (null, not null)                          => $" less than or equal to {max}",
            (not null, null)                          => $" greater than or equal to {min}",
            (not null, not null) when min.Equals(max) => $" of {min}",
            (not null, not null)                      => $" between {min} and {max}"
        };

        // If the input can be empty, add a message to the error message
        return $"{numberErrorMessage}{(allowEmptyResponse ? " or press enter to skip" : string.Empty)}";
    }
}