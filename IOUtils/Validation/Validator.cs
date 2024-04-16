using System.Diagnostics.CodeAnalysis;

namespace IOUtils.Validation;

public record Validator<T>(Predicate<T> Predicate, string ErrorMessage) {
    public bool Validate(T value, [NotNullWhen(false)] out string? errorMessage) {
        if (!Predicate(value)) {
            errorMessage = ErrorMessage;
            return false;
        }

        errorMessage = null;
        return true;
    }
}