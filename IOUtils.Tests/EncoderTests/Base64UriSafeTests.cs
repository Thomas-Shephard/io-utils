using IOUtils.Encoders;
using NUnit.Framework;

namespace IOUtils.Tests.EncoderTests;

public class Base64UriSafeTests {
    private const string TestEncodedValue = "BUZXN0";
    private const string HelloWorldEncodedValue = "BIZWxsbywgV29ybGQh";
    private const string Base64SpecialEncodedValue = "Dw-";

    [Test]
    public void Base64_Encode_Test_ReturnsEncodedString() {
        string actual = Base64Encoder.Encode(TestRawValues.TestBytes, true);

        Assert.That(actual, Is.EqualTo(TestEncodedValue));
    }

    [Test]
    public void Base64_Decode_Test_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.TestBytes;

        byte[] actual = Base64Encoder.Decode(TestEncodedValue, true);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base64_Encode_HelloWorld_ReturnsEncodedString() {
        string actual = Base64Encoder.Encode(TestRawValues.HelloWorldBytes, true);

        Assert.That(actual, Is.EqualTo(HelloWorldEncodedValue));
    }

    [Test]
    public void Base64_Decode_HelloWorld_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.HelloWorldBytes;

        byte[] actual = Base64Encoder.Decode(HelloWorldEncodedValue, true);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base64_Encode_Base64SpecialBytes_ReturnsEncodedString() {
        string actual = Base64Encoder.Encode(TestRawValues.Base64SpecialBytes, true);

        Assert.That(actual, Is.EqualTo(Base64SpecialEncodedValue));
    }

    [Test]
    public void Base64_Decode_Base64SpecialBytes_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.Base64SpecialBytes;

        byte[] actual = Base64Encoder.Decode(Base64SpecialEncodedValue, true);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base64_Encode_EmptyArray_ReturnsEmptyString() {
        string actual = Base64Encoder.Encode(TestRawValues.EmptyBytes, true);

        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void Base64_Decode_EmptyString_ReturnsEmptyArray() {
        byte[] expected = TestRawValues.EmptyBytes;

        byte[] actual = Base64Encoder.Decode(string.Empty, true);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base64_Decode_InvalidCharacter_ThrowsFormatException() {
        Assert.Throws<FormatException>(() => Base64Encoder.Decode("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ+/", true));
    }
}