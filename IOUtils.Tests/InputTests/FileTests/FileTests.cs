using IOUtils.Input;
using NUnit.Framework;

namespace IOUtils.Tests.InputTests.FileTests;

public class FileTests {
    private const string Question = "Example question text";

    [TestCase("new-file")]
    [TestCase("./new-file")]
    public void FileInput_File_ValidInput_ReturnsValue(string input) {
        MockProvider mockProvider = new(input);

        string actual;

        using (File.Create(input)) {
            actual = FileInput.GetFilePath(Question, mockProvider);
        }

        File.Delete(input);

        Assert.That(actual, Is.EqualTo(input));
    }

    [TestCase("./non-existent-file")]
    [TestCase("non-existent-file")]
    public void FileInput_File_InvalidInput_ErrorOutput(string input) {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => FileInput.GetFilePath(Question, mockProvider));

        string[] expected = {
            Question,
            "That was not valid, the file does not exist"
        };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }
}