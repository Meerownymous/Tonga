

namespace Tonga.Text;

/// <summary>
/// Hexadecimal representation of Bytes.
/// </summary>
public sealed class AsHex : TextEnvelope
{
    private static readonly char[] HEX_CHARS =
    [
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'
    ];

    /// <summary>
    /// Hexadecimal representation of Bytes.
    /// </summary>
    /// <param name="bytes">bytes</param>
    public AsHex(IBytes bytes) : base(
        () =>
        {
            var rawBytes = bytes.Raw();
            var hex = new char[rawBytes.Length * 2];
            var chr = -1;
            for (int i = 0; i < rawBytes.Length; i++)
            {
                int value = 0xff & rawBytes[i];
                hex[++chr] = HEX_CHARS[value >> 4];
                hex[++chr] = HEX_CHARS[value & 0x0f];
            }
            return new string(hex);
        })
    { }
}

public static partial class TextSmarts
{
    public static IText AsHex(this IBytes bytes) => new AsHex(bytes);
}

