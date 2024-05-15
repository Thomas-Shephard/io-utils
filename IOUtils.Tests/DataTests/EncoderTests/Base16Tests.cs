using IOUtils.Data;
using NUnit.Framework;

namespace IOUtils.Tests.DataTests.EncoderTests;

public class Base16Tests {
    private const string TestEncodedValue = "54657374";
    private const string HelloWorldEncodedValue = "48656C6C6F2C20576F726C6421";

    [Test]
    public void Base16_Encode_Test_ReturnsEncodedString() {
        string actual = Encoder.Base16.Encode(TestRawValues.TestBytes);

        Assert.That(actual, Is.EqualTo(TestEncodedValue));
    }

    [Test]
    public void Base16_Decode_Test_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.TestBytes;

        byte[] actual = Encoder.Base16.Decode(TestEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base16_Encode_HelloWorld_ReturnsEncodedString() {
        string actual = Encoder.Base16.Encode(TestRawValues.HelloWorldBytes);

        Assert.That(actual, Is.EqualTo(HelloWorldEncodedValue));
    }

    [Test]
    public void Base16_Decode_HelloWorld_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.HelloWorldBytes;

        byte[] actual = Encoder.Base16.Decode(HelloWorldEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base16_Encode_EmptyArray_ReturnsEmptyString() {
        string actual = Encoder.Base16.Encode(TestRawValues.EmptyBytes);

        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void Base16_Decode_EmptyString_ReturnsEmptyArray() {
        byte[] expected = TestRawValues.EmptyBytes;

        byte[] actual = Encoder.Base16.Decode(string.Empty);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base16_Decode_InvalidCharacter_ThrowsFormatException() {
        Assert.Throws<ArgumentException>(() => Encoder.Base16.Decode("1379ADG"));
    }

    [TestCase("01F23456789")]
    [TestCase("123B456789")]
    [TestCase("012345678")]
    [TestCase("12345678")]
    [TestCase("01234567A")]
    public void Base16_RequiresEncoding_Valid_ReturnsFalse(string raw) {
        byte[] bytes = raw.Select(c => (byte)c).ToArray();

        Assert.That(Encoder.Base16.RequiresEncoding(bytes), Is.False);
    }

    [TestCase("G")]
    [TestCase("100-001")]
    [TestCase("9.981")]
    [TestCase("62?")]
    [TestCase("Hello, World!")]
    public void Base16_RequiresEncoding_Invalid_ReturnsTrue(string raw) {
        byte[] bytes = raw.Select(c => (byte)c).ToArray();

        Assert.That(Encoder.Base16.RequiresEncoding(bytes), Is.True);
    }
}