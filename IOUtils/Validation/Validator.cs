using IOUtils.Providers;

namespace IOUtils.Validation;

public record Validator<T>(Func<T, bool> ValidationCheck, string ErrorMessage) {
    public bool Validate(T value, IProvider? provider = null) {
        provider ??= IProvider.Default;

        if (ValidationCheck(value))
            return true;

        provider.WriteLine(ErrorMessage);
        return false;
    }
}