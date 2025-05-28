

using System;
using System.IO;
using Tonga.Scalar;
using Tonga.Text;

namespace Tonga.Bytes;

/// <summary>
/// Bytes from Hex String
/// </summary>
public sealed class HexBytes(Func<string> origin) : IBytes
{
    private readonly Lazy<byte[]> bytes = new(() =>
    {
        var hex = origin();
        if ((hex.Length & 1) == 1)
        {
            throw new IOException("Length of hexadecimal text is odd");
        }

        byte[] raw = new byte[hex.Length / 2];
        for (int i = 0; i < raw.Length; i++)
        {
            raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }

        return raw;
    });


    /// <summary>
    /// Bytes from Hex String
    /// </summary>
    /// <param name="origin">The string in Hex format</param>
    public HexBytes(string origin) : this(() => origin)
    { }

    /// <summary>
    /// Bytes from Hex String
    /// </summary>
    /// <param name="origin">The string in Hex format</param>
    public HexBytes(IText origin) : this(origin.Str)
    { }

    public byte[] Bytes() => bytes.Value;
}

public static partial class BytesSmarts
{
    public static IBytes AsHexBytes(this string origin) => new HexBytes(origin);

    public static IBytes AsHexBytes(this IText origin) => new HexBytes(origin);

    public static IBytes AsHexBytes(this Func<string> origin) => new HexBytes(origin);
}
