using IOUtils.Validation;

namespace IOUtils.Utils;

public static class ValidationUtils {
    public static bool Validate<T>(this IEnumerable<Validator<T>>? validators, T value, out List<string> errorMessages) {
        errorMessages = new List<string>();

        if (validators is null)
            return true;

        foreach (Validator<T> validator in validators) {
            if (!validator.Validate(value, out string? errorMessage)) {
                errorMessages.Add(errorMessage);
            }
        }

        return errorMessages.Count is 0;
    }
}