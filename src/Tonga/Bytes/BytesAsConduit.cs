

using System;
using System.IO;

namespace Tonga.Bytes;

/// <summary>
/// Bytes as conduit.
/// </summary>
public sealed class BytesAsConduit(IBytes source) : IConduit
{
    /// <summary>
    /// Bytes as conduit.
    /// </summary>
    /// <param name="text">a text</param>
    public BytesAsConduit(IText text) : this(new AsBytes(text))
    { }

    /// <summary>
    /// Bytes as conduit.
    /// </summary>
    /// <param name="text">a string</param>
    public BytesAsConduit(String text) : this(new AsBytes(text))
    { }

    /// <summary>
    /// Bytes as conduit.
    /// </summary>
    public BytesAsConduit(byte[] bytes) : this(new AsBytes(bytes))
    { }

    /// <summary>
    /// Access the stream.
    /// </summary>
    public Stream Stream() => new MemoryStream(source.Bytes());
}
