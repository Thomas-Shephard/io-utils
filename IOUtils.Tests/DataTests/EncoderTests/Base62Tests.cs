using IOUtils.Data;
using NUnit.Framework;

namespace IOUtils.Tests.DataTests.EncoderTests;

public class Base62Tests {
    private const string TestEncodedValue = "1Xp7Ke";
    private const string HelloWorldEncodedValue = "1wJfrzvdbtXUOlUjUf";

    [Test]
    public void Base62_Encode_Test_ReturnsEncodedString() {
        string actual = Encoder.Base62.Encode(TestRawValues.TestBytes);

        Assert.That(actual, Is.EqualTo(TestEncodedValue));
    }

    [Test]
    public void Base62_Decode_Test_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.TestBytes;

        byte[] actual = Encoder.Base62.Decode(TestEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base62_Encode_HelloWorld_ReturnsEncodedString() {
        string actual = Encoder.Base62.Encode(TestRawValues.HelloWorldBytes);

        Assert.That(actual, Is.EqualTo(HelloWorldEncodedValue));
    }

    [Test]
    public void Base62_Decode_HelloWorld_ReturnsDecodedBytes() {
        byte[] expected = TestRawValues.HelloWorldBytes;

        byte[] actual = Encoder.Base62.Decode(HelloWorldEncodedValue);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base62_Encode_EmptyArray_ReturnsEmptyString() {
        string actual = Encoder.Base62.Encode(TestRawValues.EmptyBytes);

        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void Base62_Decode_EmptyString_ReturnsEmptyArray() {
        byte[] expected = TestRawValues.EmptyBytes;

        byte[] actual = Encoder.Base62.Decode(string.Empty);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Base62_Decode_InvalidCharacter_ThrowsFormatException() {
        Assert.Throws<ArgumentException>(() => Encoder.Base62.Decode("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-_"));
    }

    [TestCase("01F23ZXfdmcxc456789")]
    [TestCase("123B4TzxV56789")]
    [TestCase("01234Yasd5678")]
    [TestCase("12345678")]
    [TestCase("012345z67A")]
    public void Base62_RequiresEncoding_Valid_ReturnsFalse(string raw) {
        byte[] bytes = raw.Select(c => (byte)c).ToArray();

        Assert.That(Encoder.Base62.RequiresEncoding(bytes), Is.False);
    }

    [TestCase("!")]
    [TestCase("100-001")]
    [TestCase("9.981")]
    [TestCase("62?")]
    [TestCase("Hello, World!")]
    public void Base62_RequiresEncoding_Invalid_ReturnsTrue(string raw) {
        byte[] bytes = raw.Select(c => (byte)c).ToArray();

        Assert.That(Encoder.Base62.RequiresEncoding(bytes), Is.True);
    }
}