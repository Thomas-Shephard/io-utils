using IOUtils.Encoders;
using NUnit.Framework;

namespace IOUtils.Tests.EncoderTests;

public class Base16Tests {
    private const string TestEncodedValue = "54657374";
    private const string HelloWorldEncodedValue = "48656C6C6F2C20576F726C6421";

    [Test]
    public void Base16_Encode_Test_ReturnsEncodedString() {
        string actual = Base16Encoder.Encode(TestRawValues.TestBytes);

        Assert.That(actual, Is.EqualTo(TestEncodedValue));
    }

    [Test]
    public void Base16_Decode_Test_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.TestBytes;

        byte[] actual = Base16Encoder.Decode(TestEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base16_Encode_HelloWorld_ReturnsEncodedString() {
        string actual = Base16Encoder.Encode(TestRawValues.HelloWorldBytes);

        Assert.That(actual, Is.EqualTo(HelloWorldEncodedValue));
    }

    [Test]
    public void Base16_Decode_HelloWorld_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.HelloWorldBytes;

        byte[] actual = Base16Encoder.Decode(HelloWorldEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base16_Encode_EmptyArray_ReturnsEmptyString() {
        string actual = Base16Encoder.Encode(TestRawValues.EmptyBytes);

        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void Base16_Decode_EmptyString_ReturnsEmptyArray() {
        byte[] expected = TestRawValues.EmptyBytes;

        byte[] actual = Base16Encoder.Decode(string.Empty);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base16_Decode_InvalidCharacter_ThrowsFormatException() {
        Assert.Throws<FormatException>(() => Base16Encoder.Decode("1379ADG"));
    }
}