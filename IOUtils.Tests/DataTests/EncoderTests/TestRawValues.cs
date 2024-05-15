namespace IOUtils.Tests.EncoderTests;

public static class TestRawValues {
    public static readonly byte[] TestBytes = "Test"u8.ToArray();
    public static readonly byte[] HelloWorldBytes = "Hello, World!"u8.ToArray();
    public static readonly byte[] EmptyBytes = [];
    public static readonly byte[] Base64SpecialBytes = "<>"u8.ToArray();
}