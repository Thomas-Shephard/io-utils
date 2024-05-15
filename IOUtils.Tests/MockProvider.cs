using IOUtils.Providers;

namespace IOUtils.Tests;

internal class MockProvider : IProvider {
    internal MockProvider(params string?[] inputLines) {
        foreach (string? inputLine in inputLines) {
            InputLines.Enqueue(inputLine);
        }
    }

    internal Queue<string?> InputLines { get; } = [];
    internal List<string?> OutputLines { get; } = [];
    public string? LastInput { get; private set; }

    public string? ReadLine() {
        string? input = InputLines.Dequeue();

        LastInput = input;
        return input;
    }

    public void WriteLine(string? value) {
        OutputLines.Add(value);
    }
}