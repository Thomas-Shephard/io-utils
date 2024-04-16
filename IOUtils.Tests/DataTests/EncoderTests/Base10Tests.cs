using IOUtils.Data;
using IOUtils.Tests.EncoderTests;
using NUnit.Framework;

namespace IOUtils.Tests.DataTests.EncoderTests;

public class Base10Tests {
    private const string TestEncodedValue = "1415934836";
    private const string HelloWorldEncodedValue = "5735816763073854918203775149089";

    [Test]
    public void Base10_Encode_Test_ReturnsEncodedString() {
        string actual = Encoder.Base10.Encode(TestRawValues.TestBytes);

        Assert.That(actual, Is.EqualTo(TestEncodedValue));
    }

    [Test]
    public void Base10_Decode_Test_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.TestBytes;

        byte[] actual = Encoder.Base10.Decode(TestEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base10_Encode_HelloWorld_ReturnsEncodedString() {
        string actual = Encoder.Base10.Encode(TestRawValues.HelloWorldBytes);

        Assert.That(actual, Is.EqualTo(HelloWorldEncodedValue));
    }

    [Test]
    public void Base10_Decode_HelloWorld_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.HelloWorldBytes;

        byte[] actual = Encoder.Base10.Decode(HelloWorldEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base10_Encode_EmptyArray_ReturnsEmptyString() {
        string actual = Encoder.Base10.Encode(TestRawValues.EmptyBytes);

        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void Base10_Decode_EmptyString_ReturnsEmptyArray() {
        byte[] expected = TestRawValues.EmptyBytes;

        byte[] actual = Encoder.Base10.Decode(string.Empty);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base10_Decode_InvalidCharacter_ThrowsFormatException() {
        Assert.Throws<ArgumentException>(() => Encoder.Base10.Decode("1379A"));
    }
}