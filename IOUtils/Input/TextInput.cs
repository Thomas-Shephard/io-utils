using IOUtils.Providers;
using IOUtils.Utils;
using IOUtils.Validation;

namespace IOUtils.Input;

public static class TextInput {
    private static readonly Validator<string> NotEmptyValidator = new(value => !string.IsNullOrWhiteSpace(value), "That was not valid, enter some text");

    public static string GetText(string question, Validator<string>[]? validators = null, IProvider? provider = null) {
        provider ??= IProvider.Default;
        provider.WriteLine(question);

        List<string> errorMessages = new();

        string input;
        do {
            if (errorMessages.Count > 0) {
                foreach (string errorMessage in errorMessages) {
                    provider.WriteLine(errorMessage);
                }
            }

            input = provider.ReadLine() ?? "";
        } while (!validators.Validate(input, out errorMessages));

        return input;
    }

    public static string GetNonEmptyText(string question, IProvider? provider = null) {
        return GetText(question, new[] { NotEmptyValidator }, provider);
    }
}