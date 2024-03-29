using System.Numerics;
using System.Text;

namespace IOUtils.Utils;

internal static class EncodingUtils {
    internal static string Encode(this byte[] bytes, string charSet, bool isBigEndian) {
        BigInteger dividend = new(bytes, true, isBigEndian);

        StringBuilder stringBuilder = new();

        while (!dividend.IsZero) {
            dividend = BigInteger.DivRem(dividend, charSet.Length, out BigInteger remainder);

            char encodedChar = charSet[(int)remainder];
            stringBuilder.Insert(0, encodedChar);
        }

        return stringBuilder.ToString();
    }

    internal static byte[] Decode(this string encoded, string charSet, bool isBigEndian) {
        BigInteger decodedValue = BigInteger.Zero;

        foreach (char encodedChar in encoded) {
            BigInteger multipliedBase = BigInteger.Multiply(decodedValue, charSet.Length);

            int charIndex = charSet.IndexOf(encodedChar);

            if (charIndex is -1) {
                throw new FormatException($"Character '{encodedChar}' is not in the character set.");
            }

            decodedValue = BigInteger.Add(multipliedBase, charIndex);
        }

        return decodedValue.IsZero
            ? Array.Empty<byte>()
            : decodedValue.ToByteArray(true, isBigEndian);
    }
}