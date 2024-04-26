using IOUtils.Data;
using IOUtils.Tests.EncoderTests;
using NUnit.Framework;

namespace IOUtils.Tests.DataTests.EncoderTests;

public class Base64UriSafeTests {
    private const string TestEncodedValue = "BUZXN0";
    private const string HelloWorldEncodedValue = "BIZWxsbywgV29ybGQh";
    private const string Base64SpecialEncodedValue = "Dw-";

    [Test]
    public void Base64_Encode_Test_ReturnsEncodedString() {
        string actual = Encoder.Base64UriSafe.Encode(TestRawValues.TestBytes, true);

        Assert.That(actual, Is.EqualTo(TestEncodedValue));
    }

    [Test]
    public void Base64_Decode_Test_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.TestBytes;

        byte[] actual = Encoder.Base64UriSafe.Decode(TestEncodedValue, true);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base64_Encode_HelloWorld_ReturnsEncodedString() {
        string actual = Encoder.Base64UriSafe.Encode(TestRawValues.HelloWorldBytes, true);

        Assert.That(actual, Is.EqualTo(HelloWorldEncodedValue));
    }

    [Test]
    public void Base64_Decode_HelloWorld_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.HelloWorldBytes;

        byte[] actual = Encoder.Base64UriSafe.Decode(HelloWorldEncodedValue, true);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base64_Encode_Base64SpecialBytes_ReturnsEncodedString() {
        string actual = Encoder.Base64UriSafe.Encode(TestRawValues.Base64SpecialBytes, true);

        Assert.That(actual, Is.EqualTo(Base64SpecialEncodedValue));
    }

    [Test]
    public void Base64_Decode_Base64SpecialBytes_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.Base64SpecialBytes;

        byte[] actual = Encoder.Base64UriSafe.Decode(Base64SpecialEncodedValue, true);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base64_Encode_EmptyArray_ReturnsEmptyString() {
        string actual = Encoder.Base64UriSafe.Encode(TestRawValues.EmptyBytes, true);

        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void Base64_Decode_EmptyString_ReturnsEmptyArray() {
        byte[] expected = TestRawValues.EmptyBytes;

        byte[] actual = Encoder.Base64UriSafe.Decode(string.Empty, true);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base64_Decode_InvalidCharacter_ThrowsFormatException() {
        Assert.Throws<ArgumentException>(() => Encoder.Base64UriSafe.Decode("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ+/", true));
    }

    [TestCase("01F23ZXfdmcxc456789-")]
    [TestCase("123B4T_zxV56789")]
    [TestCase("01234Yasd5678")]
    [TestCase("12345678")]
    [TestCase("012345z67A")]
    public void Base64_RequiresEncoding_Valid_ReturnsFalse(string raw) {
        byte[] bytes = raw.Select(c => (byte)c).ToArray();

        Assert.That(Encoder.Base64UriSafe.RequiresEncoding(bytes), Is.False);
    }

    [TestCase("!")]
    [TestCase("100/001")]
    [TestCase("9.981")]
    [TestCase("62?")]
    [TestCase("Hello, World!")]
    public void Base64_RequiresEncoding_Invalid_ReturnsTrue(string raw) {
        byte[] bytes = raw.Select(c => (byte)c).ToArray();

        Assert.That(Encoder.Base64UriSafe.RequiresEncoding(bytes), Is.True);
    }
}