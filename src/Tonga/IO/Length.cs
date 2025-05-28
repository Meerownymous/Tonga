using System;
using System.IO;
using Tonga.Scalar;

namespace Tonga.IO;

/// <summary>
/// Length of a stream
/// </summary>
public sealed class Length(Func<Stream> origin) : ScalarEnvelope<Int64>(
    () =>
    {
        long result;
        var stream = origin();
        if(stream.CanSeek)
        {
            result = stream.Length;
            stream.Close();
        }
        else
        {
            long size = 0;
            byte[] buf = new byte[16 << 10];

            int bytesRead;
            while ((bytesRead = stream.Read(buf, 0, buf.Length)) > 0)
            {
                size += bytesRead;
            }
            result = size;
            stream.Close();
        }
        return result;
    }
)
{
    /// <summary>
    /// Length of a stream
    /// </summary>
    public Length(Stream stream) : this(() => stream)
    { }

    /// <summary>
    /// Length of a conduit
    /// </summary>
    public Length(IConduit origin) : this(origin.Stream)
    { }
}

public static partial class IOSmarts
{
    /// <summary>
    /// Length of a stream
    /// </summary>
    public static IScalar<Int64> Length(this Stream stream) => new Length(stream);

    /// <summary>
    /// Length of a conduit
    /// </summary>
    public static IScalar<Int64> Length(this IConduit conduit) => new Length(conduit);

    /// <summary>
    /// Length of a stream
    /// </summary>
    public static IScalar<Int64> Length(this Func<Stream> stream) => new Length(stream);
}
