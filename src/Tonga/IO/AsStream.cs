using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tonga.Bytes;

namespace Tonga.IO;

/// <summary>
/// A stream out of other objects.
/// </summary>
public sealed class AsStream(Func<Stream> src) : Stream, IDisposable
{
    /// <summary>
    /// the source
    /// </summary>
    private readonly Lazy<Stream> stream = new(src);

    /// <summary>
    /// A stream out of a file Uri.
    /// </summary>
    /// <param name="path">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
    public AsStream(Uri path) : this(Streamified(() => path))
    { }

    /// <summary>
    /// A stream out of Bytes.
    /// </summary>
    /// <param name="bytes">a <see cref="IBytes"/> object which will be copied to memory</param>
    public AsStream(IBytes bytes) : this(Streamified(bytes.Raw))
    { }

    /// <summary>
    /// A stream out of a Byte array.
    /// </summary>
    /// <param name="bytes">a <see cref="byte"/> array</param>
    public AsStream(byte[] bytes) : this(Streamified(() => bytes))
    { }

    /// <summary>
    /// A stream out of a Byte array.
    /// </summary>
    public AsStream(char[] chars) : this(Streamified(() => chars, Encoding.UTF8))
    { }

    /// <summary>
    /// A stream out of a Byte array.
    /// </summary>
    public AsStream(char[] chars, Encoding encoding) : this(Streamified(() => chars, encoding))
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
    /// <param name="str">some string</param>
    public AsStream(String str) : this(
        new AsBytes(str))
    { }

    /// <summary>
    /// A stream out of a string.
    /// </summary>
    /// <param name="str">some <see cref="string"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the string</param>
    public AsStream(String str, Encoding enc) : this(
        Streamified(() => str, enc)
    )
    { }

    /// <summary>
    /// A stream out of a <see cref="IText"/> with <see cref="Encoding"/>.
    /// </summary>
    /// <param name="text">some <see cref="IText"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the text</param>
    public AsStream(IText text, Encoding enc) : this(
        Streamified(text.Str, enc))
    { }

    /// <summary>
    /// A stream out of a <see cref="StreamReader"/>.
    /// </summary>
    public AsStream(StreamReader rdr) : this(new AsConduit(rdr))
    { }

    /// <summary>
    /// A readable stream out of a <see cref="IConduit"/>.
    /// </summary>
    public AsStream(IConduit source) : this(source.Stream)
    { }

    public override int Read(byte[] buf, int offset, int len) =>
        this.stream.Value.Read(buf, offset, len);

    public override bool CanRead => this.stream.Value.CanRead;

    public override bool CanSeek => this.stream.Value.CanSeek;

    public override bool CanWrite => this.stream.Value.CanWrite;

    public override long Length => this.stream.Value.Length;

    public override long Position
    {
        get => this.stream.Value.Position;
        set => this.stream.Value.Position = value;
    }

    public override void Flush() => stream.Value.Flush();

    public override long Seek(long offset, SeekOrigin origin) =>
        this.stream.Value.Seek(offset, origin);

    public override void SetLength(long value) =>
        this.stream.Value.SetLength(value);

    public override void Write(byte[] buffer, int offset, int count) =>
        this.stream.Value.Write(buffer, offset, count);

    public new void Dispose() =>
        this.stream.Value.Dispose();

    public override void Close() => this.stream.Value.Close();

    private static Func<Stream> Streamified(Func<byte[]> src) => () =>
    {
        var b = src();
        var m = new MemoryStream();
        m.Write(b, 0, b.Length);
        m.Seek(0, SeekOrigin.Begin);
        return m;
    };

    private static Func<Stream> Streamified(Func<string> src, Encoding encoding) => () =>
        new MemoryStream(encoding.GetBytes(src()));

    private static Func<Stream> Streamified(Func<char[]> src, Encoding encoding) => () =>
        new MemoryStream(encoding.GetBytes(src()));

    private static Func<Stream> Streamified(Func<Uri> src) => () =>
    {
        var uri = src();
        if (uri.HostNameType == UriHostNameType.Dns)
            throw new ArgumentException($"Only file resources are allowed, this is not: '{uri.AbsoluteUri}'");
        return new FileStream(Uri.UnescapeDataString(uri.LocalPath), FileMode.OpenOrCreate, FileAccess.ReadWrite);
    };
}

public static partial class IOSmarts
{
    /// <summary>
    /// A stream out of a file Uri.
    /// </summary>
    /// <param name="path">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
    public static Stream AsStream(this Uri path) => new AsStream(path);

    /// <summary>
    /// A stream out of Bytes.
    /// </summary>
    public static Stream AsStream(this IConduit conduit) => new AsStream(conduit);

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
}
