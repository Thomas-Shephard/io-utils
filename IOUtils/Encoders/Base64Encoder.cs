using IOUtils.Utils;

namespace IOUtils.Encoders;

public static class Base64Encoder {
    private const string CharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
    private const string UriSafeCharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

    public static string Encode(byte[] raw, bool uriSafe = false, bool isBigEndian = true) {
        return raw.Encode(uriSafe ? UriSafeCharSet : CharSet, isBigEndian);
    }

    public static byte[] Decode(string encoded, bool uriSafe = false, bool isBigEndian = true) {
        return encoded.Decode(uriSafe ? UriSafeCharSet : CharSet, isBigEndian);
    }
}