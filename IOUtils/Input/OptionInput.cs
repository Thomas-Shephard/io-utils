using System.Text;
using IOUtils.Providers;

namespace IOUtils.Input;

public static class OptionInput {
    public static string GetOption(string question, string[] options, IProvider? provider = null) {
        int responseIndex = GetOptionIndex(question, options, provider);

        return options[responseIndex];
    }
    
    public static bool GetYesNoOption(string question, IProvider? provider = null) {
        // The index of the "Yes" option is 0
        return GetOptionIndex(question, new[] { "Yes", "No" }, provider) == 0;
    }

    public static int GetOptionIndex(string question, string[] options, IProvider? provider = null) {
        if (options.Length < 1)
            throw new ArgumentException("There must be at least one option to choose from", nameof(options));

        StringBuilder questionStringBuilder = new(question);

        for (int i = 0; i < options.Length; i++)
            questionStringBuilder.Append($"{Environment.NewLine}{i + 1}. {options[i]}");

        // The index of the selected option is 1 less than the numerical input
        return NumericalInput<int>.Get(questionStringBuilder.ToString(), 1, options.Length, provider) - 1;
    }
}