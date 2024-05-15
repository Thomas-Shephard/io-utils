using System.Numerics;
using System.Text;

namespace IOUtils.Data;

public class Encoder {
    public static readonly Encoder Base2 = new("01");
    public static readonly Encoder Base10 = new("0123456789");
    public static readonly Encoder Base16 = new("0123456789ABCDEF");
    public static readonly Encoder Base36 = new("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
    public static readonly Encoder Base62 = new("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
    public static readonly Encoder Base64 = new("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/");
    public static readonly Encoder Base64UriSafe = new("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_");

    public Encoder(char[] characters) {
        if (characters.Length < 2)
            throw new ArgumentException("The encoder must have at least two characters", nameof(characters));

        if (characters.Length != characters.Distinct().Count())
            throw new ArgumentException("The encoder must not have duplicate characters", nameof(characters));

        Characters = characters;
    }

    public Encoder(string characters) : this(characters.ToCharArray()) { }

    public char[] Characters { get; }

    public string Encode(byte[] bytes, bool isBigEndian = true) {
        BigInteger dividend = new(bytes, true, isBigEndian);

        StringBuilder encodedStringBuilder = new();

        while (!dividend.IsZero) {
            dividend = BigInteger.DivRem(dividend, Characters.Length, out BigInteger remainder);

            char encodedChar = Characters[(int)remainder];
            encodedStringBuilder.Insert(0, encodedChar);
        }

        return encodedStringBuilder.ToString();
    }

    public byte[] Decode(string encoded, bool isBigEndian = true) {
        BigInteger decodedValue = BigInteger.Zero;

        foreach (int charIndex in encoded.Select(encodedChar => Array.IndexOf(Characters, encodedChar))) {
            if (charIndex is -1) {
                throw new ArgumentException("The encoding contains unknown characters", nameof(encoded));
            }

            decodedValue = BigInteger.Add(BigInteger.Multiply(decodedValue, Characters.Length), charIndex);
        }

        return decodedValue.IsZero
            ? []
            : decodedValue.ToByteArray(true, isBigEndian);
    }

    public bool RequiresEncoding(IEnumerable<byte> bytes) {
        return bytes.Any(b => Array.IndexOf(Characters, (char)b) is -1);
    }
}