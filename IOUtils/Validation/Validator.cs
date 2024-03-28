using IOUtils.Providers;

namespace IOUtils.Validation;

public record Validator<T>(Predicate<T> Predicate, string ErrorMessage) {
    public bool Validate(T value, IProvider? provider = null) {
        provider ??= IProvider.Default;

        if (Predicate(value))
            return true;

        provider.WriteLine(ErrorMessage);
        return false;
    }
}