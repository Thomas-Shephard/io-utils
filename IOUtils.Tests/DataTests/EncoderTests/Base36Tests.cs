using IOUtils.Data;
using NUnit.Framework;

namespace IOUtils.Tests.DataTests.EncoderTests;

public class Base36Tests {
    private const string TestEncodedValue = "NF0EB8";
    private const string HelloWorldEncodedValue = "FG3H7VQW7EEN6JWWNZMP";

    [Test]
    public void Base36_Encode_Test_ReturnsEncodedString() {
        string actual = Encoder.Base36.Encode(TestRawValues.TestBytes);

        Assert.That(actual, Is.EqualTo(TestEncodedValue));
    }

    [Test]
    public void Base36_Decode_Test_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.TestBytes;

        byte[] actual = Encoder.Base36.Decode(TestEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base36_Encode_HelloWorld_ReturnsEncodedString() {
        string actual = Encoder.Base36.Encode(TestRawValues.HelloWorldBytes);

        Assert.That(actual, Is.EqualTo(HelloWorldEncodedValue));
    }

    [Test]
    public void Base36_Decode_HelloWorld_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.HelloWorldBytes;

        byte[] actual = Encoder.Base36.Decode(HelloWorldEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base36_Encode_EmptyArray_ReturnsEmptyString() {
        string actual = Encoder.Base36.Encode(TestRawValues.EmptyBytes);

        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void Base36_Decode_EmptyString_ReturnsEmptyArray() {
        byte[] expected = TestRawValues.EmptyBytes;

        byte[] actual = Encoder.Base36.Decode(string.Empty);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base36_Decode_InvalidCharacter_ThrowsFormatException() {
        Assert.Throws<ArgumentException>(() => Encoder.Base36.Decode("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ!"));
    }

    [TestCase("01F23ZX456789")]
    [TestCase("123B4TV56789")]
    [TestCase("01234Y5678")]
    [TestCase("12345678")]
    [TestCase("01234567A")]
    public void Base36_RequiresEncoding_Valid_ReturnsFalse(string raw) {
        byte[] bytes = raw.Select(c => (byte)c).ToArray();

        Assert.That(Encoder.Base36.RequiresEncoding(bytes), Is.False);
    }

    [TestCase("g")]
    [TestCase("100-001")]
    [TestCase("9.981")]
    [TestCase("62?")]
    [TestCase("Hello, World!")]
    public void Base36_RequiresEncoding_Invalid_ReturnsTrue(string raw) {
        byte[] bytes = raw.Select(c => (byte)c).ToArray();

        Assert.That(Encoder.Base36.RequiresEncoding(bytes), Is.True);
    }
}