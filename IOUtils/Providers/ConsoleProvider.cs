namespace IOUtils.Providers;

public class ConsoleProvider : IProvider {
    public string? LastInput { get; private set; }

    public string? ReadLine() {
        string? input = Console.ReadLine();

        LastInput = input;
        return input;
    }

    public void WriteLine(string? value) {
        Console.WriteLine(value);
    }
}