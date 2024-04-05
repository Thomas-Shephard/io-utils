namespace IOUtils.Providers;

public interface IProvider {
    public static readonly IProvider Default = new ConsoleProvider();
    public string? LastInput { get; }
    public string? ReadLine();
    public void WriteLine(string? value);
}