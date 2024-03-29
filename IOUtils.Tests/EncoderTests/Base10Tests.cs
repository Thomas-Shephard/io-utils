using IOUtils.Encoders;
using NUnit.Framework;

namespace IOUtils.Tests.EncoderTests;

public class Base10Tests {
    private const string TestEncodedValue = "1415934836";
    private const string HelloWorldEncodedValue = "5735816763073854918203775149089";

    [Test]
    public void Base10_Encode_Test_ReturnsEncodedString() {
        string actual = Base10Encoder.Encode(TestRawValues.TestBytes);

        Assert.That(actual, Is.EqualTo(TestEncodedValue));
    }

    [Test]
    public void Base10_Decode_Test_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.TestBytes;

        byte[] actual = Base10Encoder.Decode(TestEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base10_Encode_HelloWorld_ReturnsEncodedString() {
        string actual = Base10Encoder.Encode(TestRawValues.HelloWorldBytes);

        Assert.That(actual, Is.EqualTo(HelloWorldEncodedValue));
    }

    [Test]
    public void Base10_Decode_HelloWorld_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.HelloWorldBytes;

        byte[] actual = Base10Encoder.Decode(HelloWorldEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base10_Encode_EmptyArray_ReturnsEmptyString() {
        string actual = Base10Encoder.Encode(TestRawValues.EmptyBytes);

        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void Base10_Decode_EmptyString_ReturnsEmptyArray() {
        byte[] expected = TestRawValues.EmptyBytes;

        byte[] actual = Base10Encoder.Decode(string.Empty);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base10_Decode_InvalidCharacter_ThrowsFormatException() {
        Assert.Throws<FormatException>(() => Base10Encoder.Decode("1379A"));
    }
}