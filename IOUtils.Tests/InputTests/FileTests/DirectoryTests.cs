using IOUtils.Input;
using NUnit.Framework;

namespace IOUtils.Tests.InputTests.FileTests;

public class DirectoryTests {
    private const string Question = "Example question text";

    [TestCase("./")]
    [TestCase(".")]
    public void FileInput_Directory_ValidInput_ReturnsValue(string input) {
        MockProvider mockProvider = new(input);

        string actual = FileInput.GetDirectoryPath(Question, mockProvider);

        Assert.That(actual, Is.EqualTo(input));
    }

    [TestCase("./non-existent-directory")]
    [TestCase("non-existent-directory")]
    public void FileInput_Directory_InvalidInput_ErrorOutput(string input) {
        MockProvider mockProvider = new(input);

        Assert.Throws<InvalidOperationException>(() => FileInput.GetDirectoryPath(Question, mockProvider));

        string[] expected = {
            Question,
            "That was not valid, the directory does not exist"
        };

        Assert.That(mockProvider.OutputLines, Is.EquivalentTo(expected));
    }
}