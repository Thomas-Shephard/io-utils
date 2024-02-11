namespace IOUtils.Providers;

public interface IProvider {
    public static readonly IProvider Default = new ConsoleProvider();
    public string? ReadLine();
    public void WriteLine(string? value);
}