using IOUtils.Encoders;
using NUnit.Framework;

namespace IOUtils.Tests.EncoderTests;

public class Base2Tests {
    private const string TestEncodedValue = "1010100011001010111001101110100";
    private const string HelloWorldEncodedValue = "1001000011001010110110001101100011011110010110000100000010101110110111101110010011011000110010000100001";

    [Test]
    public void Base2_Encode_Test_ReturnsEncodedString() {
        string actual = Base2Encoder.Encode(TestRawValues.TestBytes);

        Assert.That(actual, Is.EqualTo(TestEncodedValue));
    }

    [Test]
    public void Base2_Decode_Test_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.TestBytes;

        byte[] actual = Base2Encoder.Decode(TestEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base2_Encode_HelloWorld_ReturnsEncodedString() {
        string actual = Base2Encoder.Encode(TestRawValues.HelloWorldBytes);

        Assert.That(actual, Is.EqualTo(HelloWorldEncodedValue));
    }

    [Test]
    public void Base2_Decode_HelloWorld_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.HelloWorldBytes;

        byte[] actual = Base2Encoder.Decode(HelloWorldEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base2_Encode_EmptyArray_ReturnsEmptyString() {
        string actual = Base2Encoder.Encode(TestRawValues.EmptyBytes);

        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void Base2_Decode_EmptyString_ReturnsEmptyArray() {
        byte[] expected = TestRawValues.EmptyBytes;

        byte[] actual = Base2Encoder.Decode(string.Empty);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base2_Decode_InvalidCharacter_ThrowsFormatException() {
        Assert.Throws<FormatException>(() => Base2Encoder.Decode("10X"));
    }
}