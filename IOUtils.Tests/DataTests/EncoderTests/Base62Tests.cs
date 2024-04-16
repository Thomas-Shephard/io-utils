﻿using IOUtils.Data;
using IOUtils.Tests.EncoderTests;
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
}