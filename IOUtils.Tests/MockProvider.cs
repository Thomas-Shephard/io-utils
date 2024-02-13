using IOUtils.Providers;

namespace IOUtils.Tests;

internal class MockProvider : IProvider {
    internal MockProvider(params string?[] inputLines) {
        foreach (string? inputLine in inputLines) {
            InputLines.Enqueue(inputLine);
        }
    }

    internal Queue<string?> InputLines { get; } = new();
    internal List<string?> OutputLines { get; } = new();

    public string? ReadLine() {
        return InputLines.Dequeue();
    }

    public void WriteLine(string? value) {
        OutputLines.Add(value);
    }
}