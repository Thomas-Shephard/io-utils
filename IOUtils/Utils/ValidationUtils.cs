using IOUtils.Providers;
using IOUtils.Validation;

namespace IOUtils.Utils;

public static class ValidationUtils {
    public static bool Validate<T>(this IEnumerable<Validator<T>>? validators, T value, IProvider? provider = null) {
        return validators?.All(validator => validator.Validate(value, provider)) ?? true;
    }
}