using System;
using System.IO;
using Tonga.Pipe;

namespace Tonga.IO;

/// <summary>
/// Reads all content of a given input and then resets it.
/// </summary>
public sealed class FullRead(bool flush = true, bool close = true) : IPipe<IConduit, IConduit>
{
    public IConduit Yield(IConduit input)
    {
        long size = 0;
        var stream = input.Stream();
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
        return input;
    }
}

public static partial class IOSmarts
{
    public static IPipe<IConduit> FullRead(this IConduit conduit, bool flush = true, bool close = true) =>
        new Func<IConduit>(() => new FullRead(flush, close).Yield(conduit)).AsPipe();
}
