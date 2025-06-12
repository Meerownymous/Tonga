using System;
using System.IO;
using Tonga.Pipe;
using Tonga.Tap;

namespace Tonga.IO;

/// <summary>
/// Reads all content of a given input and then resets it.
/// </summary>
public sealed class FullRead(Func<Stream> source, bool flush = true, bool close = true) : ITap
{
    private readonly Lazy<Stream> stream = new(source);

    public FullRead(IConduit conduit, bool flush = true, bool close = true) : this(conduit.Stream, flush, close)
    { }

    public FullRead(Stream stream, bool flush = true, bool close = true) : this(() => stream, flush, close)
    { }

    public void Trigger()
    {
        long size = 0;
        var memorizedPosition = 0L;
        if (stream.Value.CanSeek)
            memorizedPosition = stream.Value.Position;
        byte[] buf = new byte[16 << 10];

        int bytesRead;
        while ((bytesRead = stream.Value.Read(buf, 0, buf.Length)) > 0)
        {
            size += bytesRead;
        }

        if (!close && stream.Value.CanSeek)
            stream.Value.Seek(memorizedPosition, SeekOrigin.Begin);
        if (flush) stream.Value.Flush();
        if (close) stream.Value.Dispose();
    }
}

public static partial class IOSmarts
{
    /// <summary>
    /// Reads all content of a given input and then resets it.
    /// </summary>
    public static ITap FullRead(this IConduit conduit, bool flush = true, bool close = true) =>
        new FullRead(conduit, flush, close);

    /// <summary>
    /// Reads all content of a given input and then resets it.
    /// </summary>
    public static ITap FullRead(this Stream stream, bool flush = true, bool close = true) =>
        new FullRead(stream, flush, close);
}
