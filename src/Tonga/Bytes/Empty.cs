

namespace Tonga.Bytes;

/// <summary>
/// Bytes without data.
/// </summary>
public sealed class Empty : IBytes
{
    /// <summary>
    /// Bytes without data.
    /// </summary>
    public Empty()
    { }

    /// <summary>
    /// Get the content as byte array.
    /// </summary>
    /// <returns>content as byte array</returns>
    public byte[] Bytes() => [];
}

