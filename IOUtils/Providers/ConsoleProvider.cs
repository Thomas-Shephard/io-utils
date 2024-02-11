namespace IOUtils.Providers;

public class ConsoleProvider : IProvider {
    public string? ReadLine() {
        return Console.ReadLine();
    }

    public void WriteLine(string? value) {
        Console.WriteLine(value);
    }
}