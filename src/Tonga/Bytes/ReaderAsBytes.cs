

using System;
using System.IO;
using System.Text;
using System.Text.Unicode;

namespace Tonga.Bytes;

/// <summary>
/// A <see cref="StringReader"/> as <see cref="IBytes"/>
/// </summary>
public sealed class ReaderAsBytes : IBytes, IDisposable
{
    private readonly IDisposable disposable;

    /// <summary>
    /// a reader
    /// </summary>
    private readonly Lazy<byte[]> bytes;

    /// <summary>
    /// A <see cref="StringReader"/> as <see cref="IBytes"/>
    /// </summary>
    /// <param name="rdr">the reader</param>
    /// <param name="max">maximum buffer size</param>
    public ReaderAsBytes(StringReader rdr) : this(rdr, Encoding.UTF8)
    { }

    /// <summary>
    /// A <see cref="StringReader"/> as <see cref="IBytes"/>
    /// </summary>
    public ReaderAsBytes(StringReader rdr, Encoding encoding) : this(() =>
        encoding.GetBytes(rdr.ReadToEnd()),
        rdr
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
        () =>
        {
            var buffer = new char[max];
            var builder = new StringBuilder(max);
            while (rdr.Peek() > -1)
            {
                var pos = rdr.Read(buffer, 0, buffer.Length);
                builder.Append(buffer, 0, pos);
            }
            rdr.BaseStream.Position = 0;
            rdr.DiscardBufferedData();
            return enc.GetBytes(builder.ToString());
        },
        rdr
    )
    { }

    private ReaderAsBytes(Func<byte[]> bytes, IDisposable disposable)
    {
        this.disposable = disposable;
        this.bytes = new Lazy<byte[]>(bytes);
    }

    /// <summary>
    /// Get the content as byte array.
    /// </summary>
    /// <returns>content as a byte array.</returns>
    public byte[] Raw() => this.bytes.Value;

    public void Dispose()
    {
        this.disposable.Dispose();
    }
}
