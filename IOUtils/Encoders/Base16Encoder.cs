using IOUtils.Utils;

namespace IOUtils.Encoders;

public static class Base16Encoder {
    private const string CharSet = "0123456789ABCDEF";

    public static string Encode(byte[] raw, bool isBigEndian = true) {
        return raw.Encode(CharSet, isBigEndian);
    }

    public static byte[] Decode(string encoded, bool isBigEndian = true) {
        return encoded.Decode(CharSet, isBigEndian);
    }
}