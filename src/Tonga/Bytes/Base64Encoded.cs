

using System;
using System.Text;
using Tonga.Scalar;

namespace Tonga.Bytes;

/// <summary>
/// Encodes all origin bytes using the Base64 encoding scheme.
/// </summary>
public sealed class Base64Encoded : IBytes
{
    private readonly Func<byte[]> bytes;

    /// <summary>
    /// Encoded origin bytes using the Base64 encoding scheme.
    /// </summary>
    /// <param name="bytes"></param>
    public Base64Encoded(IBytes bytes)
    {
        this.bytes =
            () =>
                Encoding.UTF8.GetBytes(
                    Convert.ToBase64String(
                        bytes.Bytes()
                    )
                );
    }

    /// <summary>
    /// The bytes encoded as Base64
    /// </summary>
    /// <returns></returns>
    public byte[] Bytes() => bytes();
}


public static partial class BytesSmarts
{
    public static IBytes AsBase64Encoded(IBytes bytes) => new Base64Encoded(bytes);
}
