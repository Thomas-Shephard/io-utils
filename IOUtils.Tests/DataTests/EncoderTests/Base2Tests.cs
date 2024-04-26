using IOUtils.Data;
using IOUtils.Tests.EncoderTests;
using NUnit.Framework;

namespace IOUtils.Tests.DataTests.EncoderTests;

public class Base2Tests {
    private const string TestEncodedValue = "1010100011001010111001101110100";
    private const string HelloWorldEncodedValue = "1001000011001010110110001101100011011110010110000100000010101110110111101110010011011000110010000100001";

    [Test]
    public void Base2_Encode_Test_ReturnsEncodedString() {
        string actual = Encoder.Base2.Encode(TestRawValues.TestBytes);

        Assert.That(actual, Is.EqualTo(TestEncodedValue));
    }

    [Test]
    public void Base2_Decode_Test_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.TestBytes;

        byte[] actual = Encoder.Base2.Decode(TestEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base2_Encode_HelloWorld_ReturnsEncodedString() {
        string actual = Encoder.Base2.Encode(TestRawValues.HelloWorldBytes);

        Assert.That(actual, Is.EqualTo(HelloWorldEncodedValue));
    }

    [Test]
    public void Base2_Decode_HelloWorld_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.HelloWorldBytes;

        byte[] actual = Encoder.Base2.Decode(HelloWorldEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base2_Encode_EmptyArray_ReturnsEmptyString() {
        string actual = Encoder.Base2.Encode(TestRawValues.EmptyBytes);

        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void Base2_Decode_EmptyString_ReturnsEmptyArray() {
        byte[] expected = TestRawValues.EmptyBytes;

        byte[] actual = Encoder.Base2.Decode(string.Empty);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base2_Decode_InvalidCharacter_ThrowsFormatException() {
        Assert.Throws<ArgumentException>(() => Encoder.Base2.Decode("10X"));
    }

    [TestCase("1")]
    [TestCase("0")]
    [TestCase("01")]
    [TestCase("10")]
    [TestCase("1010100011001010111001101110100")]
    public void Base2_RequiresEncoding_Valid_ReturnsFalse(string raw) {
        byte[] bytes = raw.Select(c => (byte)c).ToArray();

        Assert.That(Encoder.Base2.RequiresEncoding(bytes), Is.False);
    }

    [TestCase("2")]
    [TestCase("X")]
    [TestCase("1010100011001010111001101110100X")]
    [TestCase("1 0")]
    [TestCase("1-0")]
    public void Base2_RequiresEncoding_Invalid_ReturnsTrue(string raw) {
        byte[] bytes = raw.Select(c => (byte)c).ToArray();

        Assert.That(Encoder.Base2.RequiresEncoding(bytes), Is.True);
    }
}