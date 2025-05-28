

using System;
using System.Text;

namespace Tonga.Bytes;

/// <summary>
/// Origin bytes decoded using the Base64 encoding scheme.
/// </summary>
public sealed class Base64Decoded(IBytes bytes) : IBytes
{
    /// <summary>
    /// The
    /// </summary>
    /// <returns></returns>
    public byte[] Bytes()
    {
        var byts = bytes.Bytes();
        string base64String = Encoding.UTF8.GetString(byts, 0, byts.Length);
        return Convert.FromBase64String(base64String);
    }
}

public static partial class BytesSmarts
{
    public static IBytes AsBase64Decoded(IBytes bytes) => new Base64Decoded(bytes);
}
