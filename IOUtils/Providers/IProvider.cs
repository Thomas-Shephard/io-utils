namespace IOUtils.Providers;

public interface IProvider {
    public string? ReadLine();
    public void WriteLine(string? value);
}