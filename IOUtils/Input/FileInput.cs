using IOUtils.Providers;
using IOUtils.Validation;

namespace IOUtils.Input;

public static class FileInput {
    private static readonly Validator<string> FilePathValidator = new(File.Exists, "That was not valid, the file does not exist");
    private static readonly Validator<string> DirectoryPathValidator = new(Directory.Exists, "That was not valid, the directory does not exist");

    public static string GetFilePath(string question, IProvider? provider = null) {
        return TextInput.GetText(question, new[] { FilePathValidator }, provider);
    }

    public static string GetDirectoryPath(string question, IProvider? provider = null) {
        return TextInput.GetText(question, new[] { DirectoryPathValidator }, provider);
    }
}