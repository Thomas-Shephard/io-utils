using IOUtils.Data;
using NUnit.Framework;

namespace IOUtils.Tests.DataTests.EncoderTests;

public class InvalidEncoderTests {
    [Test]
    public void CreateEncoder_NoCharacters_ThrowsArgumentException() {
        Assert.That(() => new Encoder(string.Empty), Throws.ArgumentException);
    }

    [Test]
    public void CreateEncoder_OneCharacter_ThrowsArgumentException() {
        Assert.That(() => new Encoder("0"), Throws.ArgumentException);
    }

    [Test]
    public void CreateEncoder_DuplicateCharacters_ThrowsArgumentException() {
        Assert.That(() => new Encoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-_"), Throws.ArgumentException);
    }
}