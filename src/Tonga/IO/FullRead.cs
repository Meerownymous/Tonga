using System;
using System.IO;
using Tonga.Pipe;

namespace Tonga.IO;

/// <summary>
/// Reads all content of a given input and then resets it.
/// </summary>
public sealed class FullRead(Func<Stream> source, bool flush = true, bool close = true) : IPipe<Stream>
{
    public FullRead(IConduit conduit, bool flush = true, bool close = false) : this(conduit.Stream, flush, close)
    { }

    public FullRead(Stream stream, bool flush = true, bool close = false) : this(() => stream, flush, close)
    { }

    public Stream Yield()
    {
        long size = 0;
        var stream = source();
        var memorizedPosition = 0L;
        if (stream.CanSeek)
            memorizedPosition = stream.Position;
        byte[] buf = new byte[16 << 10];

        int bytesRead;
        while ((bytesRead = stream.Read(buf, 0, buf.Length)) > 0)
        {
            size += bytesRead;
        }

        if (stream.CanSeek)
            stream.Seek(memorizedPosition, SeekOrigin.Begin);
        if (flush) stream.Flush();
        if (close) stream.Close();
        return stream;
    }
}

public static partial class IOSmarts
{
    /// <summary>
    /// Reads all content of a given input and then resets it.
    /// </summary>
    public static IPipe<Stream> FullRead(this IConduit conduit, bool flush = true, bool close = true) =>
        new Func<Stream>(() => new FullRead(conduit, flush, close).Yield()).AsPipe();

    /// <summary>
    /// Reads all content of a given input and then resets it.
    /// </summary>
    public static IPipe<Stream> FullRead(this Stream stream, bool flush = true, bool close = true) =>
        new Func<Stream>(() => new FullRead(stream, flush, close).Yield()).AsPipe();
}
