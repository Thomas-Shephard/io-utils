using System.Text;
using IOUtils.Providers;

namespace IOUtils.Input;

public static class OptionInput {
    public static T GetOption<T>(string question, Dictionary<string, T> options, IProvider? provider = null) {
        string response = GetOption(question, options.Keys.ToArray(), provider);

        return options[response];
    }

    public static string GetOption(string question, string[] options, IProvider? provider = null) {
        int responseIndex = GetOptionIndex(question, options, provider);

        return options[responseIndex];
    }

    public static bool GetYesNoOption(string question, IProvider? provider = null) {
        return GetEitherOrOption(question, "Yes", "No", provider);
    }

    public static bool GetEitherOrOption(string question, string trueOption, string falseOption, IProvider? provider = null) {
        // The index of the first option is 0
        return GetOptionIndex(question, new[] { trueOption, falseOption }, provider) is 0;
    }

    public static int GetOptionIndex(string question, string[] options, IProvider? provider = null) {
        if (options.Length < 1)
            throw new ArgumentException("There must be at least one option to choose from", nameof(options));

        StringBuilder questionStringBuilder = new(question);

        for (int i = 0; i < options.Length; i++)
            questionStringBuilder.Append($"{Environment.NewLine}{i + 1}. {options[i]}");

        // The index of the selected option is 1 less than the input number
        return NumberInput<int>.GetNumber(questionStringBuilder.ToString(), 1, options.Length, provider) - 1;
    }
}