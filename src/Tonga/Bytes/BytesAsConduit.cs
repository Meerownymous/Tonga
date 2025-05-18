

using System;
using System.IO;

namespace Tonga.Bytes;

/// <summary>
/// Bytes as input.
/// </summary>
public sealed class BytesAsConduit(IBytes source) : IConduit
{
    /// <summary>
    /// Bytes as input.
    /// </summary>
    /// <param name="text">a text</param>
    public BytesAsConduit(IText text) : this(new AsBytes(text))
    { }

    /// <summary>
    /// Bytes as input.
    /// </summary>
    /// <param name="text">a string</param>
    public BytesAsConduit(String text) : this(new AsBytes(text))
    { }

    /// <summary>
    /// Bytes as input.
    /// </summary>
    /// <param name="bytes">byte array</param>
    public BytesAsConduit(byte[] bytes) : this(new AsBytes(bytes))
    { }

    /// <summary>
    /// Get the stream.
    /// </summary>
    public Stream Stream() => new MemoryStream(source.Bytes());
}
