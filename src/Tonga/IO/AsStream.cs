

using System;
using System.IO;
using System.Text;

namespace Tonga.IO;

/// <summary>
/// A stream out of other objects.
/// </summary>
public sealed class AsStream(Func<Stream> src) : Stream, IDisposable
{
    /// <summary>
    /// the source
    /// </summary>
    private readonly Lazy<Stream> source = new(src);

    /// <summary>
    /// A stream out of a file Uri.
    /// </summary>
    /// <param name="path">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
    public AsStream(Uri path) : this(new AsConduit(path))
    { }

    /// <summary>
    /// A stream out of a www Url.
    /// </summary>
    /// <param name="url">a url starting with http:// or https://</param>
    public AsStream(Url url) : this(new AsConduit(url))
    { }

    /// <summary>
    /// A stream out of Bytes.
    /// </summary>
    /// <param name="bytes">a <see cref="IBytes"/> object which will be copied to memory</param>
    public AsStream(IBytes bytes) : this(new AsConduit(bytes))
    { }
    /// <summary>
    /// A stream out of a Byte array.
    /// </summary>
    /// <param name="bytes">a <see cref="byte"/> array</param>
    public AsStream(byte[] bytes) : this(new AsConduit(bytes))
    { }

    /// <summary>
    /// A stream out of a Text.
    /// </summary>
    /// <param name="text">some <see cref="IText"/></param>
    public AsStream(IText text) : this(text, Encoding.UTF8)
    { }

    /// <summary>
    /// A stream out of a string.
    /// </summary>
    /// <param name="text">some string</param>
    public AsStream(String text) : this(
        new AsConduit(text))
    { }

    /// <summary>
    /// A stream out of a string.
    /// </summary>
    /// <param name="text">some <see cref="string"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the string</param>
    public AsStream(String text, Encoding enc) : this(
        new AsConduit(text, enc))
    { }

    /// <summary>
    /// A stream out of a <see cref="IText"/> with <see cref="Encoding"/>.
    /// </summary>
    /// <param name="text">some <see cref="IText"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the text</param>
    public AsStream(IText text, Encoding enc) : this(
        new AsConduit(text, enc))
    { }

    /// <summary>
    /// A stream out of a <see cref="StreamReader"/>.
    /// </summary>
    public AsStream(StreamReader rdr) : this(new AsConduit(rdr))
    { }

    /// <summary>
    /// A stream out of a <see cref="StreamReader"/>.
    /// </summary>
    public AsStream(StreamReader rdr, int max = 16 << 10) : this(
        new AsConduit(rdr, Encoding.UTF8, max))
    { }

    /// <summary>
    /// A readable stream out of a <see cref="StreamReader"/>.
    /// </summary>
    public AsStream(StreamReader rdr, Encoding enc, int max = 16 << 10) : this(
        new AsConduit(rdr, enc, max)
    )
    { }

    /// <summary>
    /// A readable stream out of a <see cref="IConduit"/>.
    /// </summary>
    public AsStream(IConduit source) : this(source.Stream)
    { }

    public override int Read(byte[] buf, int offset, int len)
    {
        return this.source.Value.Read(buf, offset, len);
    }

    public override bool CanRead => this.source.Value.CanRead;

    public override bool CanSeek => this.source.Value.CanSeek;

    public override bool CanWrite => this.source.Value.CanWrite;

    public override long Length => this.source.Value.Length;

    public override long Position
    {
        get => this.source.Value.Position;
        set => throw new InvalidOperationException("Setting the position is not supported."); //intended
    }

    public override void Flush() => source.Value.Flush();

    public override long Seek(long offset, SeekOrigin origin) =>
        source.Value.Seek(offset, origin);

    public override void SetLength(long value) =>
        throw new InvalidOperationException("Setting the length is not supported."); //intended

    public override void Write(byte[] buffer, int offset, int count) =>
        throw new InvalidOperationException("Writing is not supported."); //intended

    public void Dispose() =>
        source.Value.Dispose();
}

public static partial class IOSmarts
{
    /// <summary>
    /// A stream out of a file Uri.
    /// </summary>
    /// <param name="path">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
    public static Stream AsStream(this Uri path) => new AsStream(path);

    /// <summary>
    /// A stream out of a www Url.
    /// </summary>
    /// <param name="url">a url starting with http:// or https://</param>
    public static Stream AsStream(Url url) => new AsStream(url);

    /// <summary>
    /// A stream out of Bytes.
    /// </summary>
    /// <param name="bytes">a <see cref="IBytes"/> object which will be copied to memory</param>
    public static Stream AsStream(this IBytes bytes) => new AsStream(bytes);

    /// <summary>
    /// A stream out of a Byte array.
    /// </summary>
    /// <param name="bytes">a <see cref="byte"/> array</param>
    public static Stream AsStream(this byte[] bytes) => AsStream(bytes);

    /// <summary>
    /// A stream out of a Text.
    /// </summary>
    /// <param name="text">some <see cref="IText"/></param>
    public static Stream AsStream(this IText text) => new AsStream(text);

    /// <summary>
    /// A stream out of a string.
    /// </summary>
    /// <param name="text">some string</param>
    public static Stream AsStream(this String text) => new AsStream(text);

    /// <summary>
    /// A stream out of a string.
    /// </summary>
    /// <param name="text">some <see cref="string"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the string</param>
    public static Stream AsStream(this String text, Encoding enc) => new AsStream(text, enc);

    /// <summary>
    /// A stream out of a <see cref="IText"/> with <see cref="Encoding"/>.
    /// </summary>
    /// <param name="text">some <see cref="IText"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the text</param>
    public static Stream AsStream(this IText text, Encoding enc) => new AsStream(text, enc);

    /// <summary>
    /// A stream out of a <see cref="StreamReader"/>.
    /// </summary>
    public static Stream AsStream(this StreamReader rdr) => new AsStream(rdr);

    /// <summary>
    /// A stream out of a <see cref="StreamReader"/>.
    /// </summary>
    public static Stream AsStream(this StreamReader rdr, int max = 16 << 10) => new AsStream(rdr, max);

    /// <summary>
    /// A readable stream out of a <see cref="StreamReader"/>.
    /// </summary>
    public static Stream AsStream(this StreamReader rdr, Encoding enc, int max = 16 << 10) =>
        new AsStream(rdr, enc, max);

    /// <summary>
    /// A readable stream out of a <see cref="IConduit"/>.
    /// </summary>
    public static Stream AsStream(this IConduit source) => new AsStream(source);
}
