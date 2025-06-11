

using System;
using System.IO;
using System.Text;

namespace Tonga.Bytes;

/// <summary>
/// A <see cref="StringReader"/> as <see cref="IBytes"/>
/// </summary>
public sealed class ReaderAsBytes(Func<StreamReader> rdr, Encoding encoding, int max) : IBytes, IDisposable
{
    /// <summary>
    /// a reader
    /// </summary>
    private readonly Lazy<StreamReader> reader = new(rdr);

    /// <summary>
    /// A <see cref="StringReader"/> as <see cref="IBytes"/>
    /// </summary>
    /// <param name="rdr">the reader</param>
    /// <param name="max">maximum buffer size</param>
    public ReaderAsBytes(StringReader rdr, int max = 16 << 10) : this(() =>
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(rdr.ReadToEnd());
            writer.Flush();
            stream.Position = 0;
            return new StreamReader(stream);
        },
        Encoding.UTF8, max
    )
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> as <see cref="IBytes"/>
    /// </summary>
    /// <param name="rdr">the reader</param>
    public ReaderAsBytes(StreamReader rdr) : this(rdr, Encoding.UTF8)
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> as <see cref="IBytes"/>
    /// </summary>
    /// <param name="rdr">the reader</param>
    /// <param name="enc">encoding of the reader</param>
    /// <param name="max">maximum buffer size</param>
    public ReaderAsBytes(StreamReader rdr, Encoding enc, int max = 16 << 10) : this(
        () => rdr, enc, max
    )
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> returned by a <see cref="Func{TResult}"/>as <see cref="IBytes"/>
    /// </summary>
    /// <param name="rdr">function to retrieve the reader</param>
    /// <param name="enc">encoding of the reader</param>
    /// <param name="max">maximum buffer size</param>
    private ReaderAsBytes(IScalar<StreamReader> rdr, Encoding enc, int max = 16 << 10) : this(
        rdr.Value, enc, max
    )
    { }

    /// <summary>
    /// Get the content as byte array.
    /// </summary>
    /// <returns>content as a byte array.</returns>
    public byte[] Raw()
    {
        var rdr = this.reader.Value;
        var buffer = new char[max];
        var builder = new StringBuilder(max);
        var pos = 0;
        while (rdr.Peek() > -1)
        {
            pos = rdr.Read(buffer, 0, buffer.Length);
            builder.Append(buffer, 0, pos);
        }
        rdr.BaseStream.Position = 0;
        rdr.DiscardBufferedData();
        return encoding.GetBytes(builder.ToString());
    }

    /// <summary>
    /// Clean up.
    /// </summary>
    public void Dispose()
    {
        try
        {
            reader.Value.Dispose();
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
