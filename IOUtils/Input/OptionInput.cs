using System.Text;
using IOUtils.Providers;

namespace IOUtils.Input;

public static class OptionInput {
    public static T GetOption<T>(string question, Dictionary<string, T> options, string? defaultOption = null, IProvider? provider = null) {
        string response = GetOption(question, options.Keys.ToArray(), defaultOption, provider);

        return options[response];
    }

    public static string GetOption(string question, string[] options, string? defaultOption = null, IProvider? provider = null) {
        int responseIndex = GetOptionIndex(question, options, defaultOption, provider);

        return options[responseIndex];
    }

    public static bool GetYesNoOption(string question, bool? defaultOption = null, IProvider? provider = null) {
        return GetEitherOrOption(question, "Yes", "No", defaultOption, provider);
    }

    public static bool GetEitherOrOption(string question, string trueOption, string falseOption, bool? defaultOption = null, IProvider? provider = null) {
        string? defaultOptionString = defaultOption switch {
            true  => trueOption,
            false => falseOption,
            null  => null
        };

        // The index of the first option is 0
        return GetOptionIndex(question, new[] { trueOption, falseOption }, defaultOptionString, provider) is 0;
    }

    // If the default option is provided multiple times, the first occurrence will be the default
    // If the default option is not in the provided options, an ArgumentException will be thrown
    public static int GetOptionIndex(string question, string[] options, string? defaultOption = null, IProvider? provider = null) {
        if (options.Length < 1)
            throw new ArgumentException("There must be at least one option to choose from", nameof(options));

        int? defaultOptionIndex = defaultOption is not null
            ? Array.IndexOf(options, defaultOption)
            : null;

        if (defaultOptionIndex is -1)
            throw new ArgumentException("The default option is not in the provided options", nameof(defaultOption));

        StringBuilder questionStringBuilder = new(question);

        if (defaultOption is not null)
            questionStringBuilder.Append(" (Press enter to use default option)");

        for (int i = 0; i < options.Length; i++) {
            questionStringBuilder.Append($"{Environment.NewLine}{i + 1}. {options[i]}");

            if (i == defaultOptionIndex)
                questionStringBuilder.Append(" (Default)");
        }

        // If the default option is null, the selected option is always 1 less than the input number
        if (defaultOptionIndex is null)
            return NumberInput<int>.GetNumber(questionStringBuilder.ToString(), 1, options.Length, provider) - 1;

        // The index of the selected option is 1 less than the input number
        int? selectedOptionIndex = NumberInput<int>.GetOptionalNumber(questionStringBuilder.ToString(), 1, options.Length, provider) - 1;
        // If the selected option is null, use the default option
        return selectedOptionIndex ?? defaultOptionIndex.Value;
    }
}