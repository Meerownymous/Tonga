using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tonga.Scalar;

namespace Tonga.IO;

/// <summary>
/// A <see cref="StreamReader"/> out of other objects.
/// </summary>
public sealed class AsStreamReader : StreamReader
{
    /// <summary>
    /// the source
    /// </summary>
    private readonly IScalar<StreamReader> source;

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="char"/> array.
    /// </summary>
    /// <param name="chars">some chars</param>
    public AsStreamReader(params char[] chars) : this(new AsConduit(chars))
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="char"/> array.
    /// </summary>
    /// <param name="chars">some chars</param>
    /// <param name="enc">encoding of the chars</param>
    public AsStreamReader(char[] chars, Encoding enc) : this(new AsConduit(chars, enc))
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="byte"/> array.
    /// </summary>
    /// <param name="bytes">some bytes</param>
    public AsStreamReader(byte[] bytes) : this(new AsConduit(bytes))
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="byte"/> array.
    /// </summary>
    /// <param name="bytes">some bytes</param>
    /// <param name="enc">encoding of the bytes</param>
    public AsStreamReader(byte[] bytes, Encoding enc) : this(new AsConduit(bytes), enc)
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="Url"/> array.
    /// </summary>
    /// <param name="url">a www url starting with http:// or https://</param>
    public AsStreamReader(Url url) : this(new AsConduit(url))
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="string"/>.
    /// </summary>
    /// <param name="content">a string</param>
    public AsStreamReader(string content) : this(new AsConduit(content))
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> out of a file <see cref="Uri"/> array.
    /// </summary>
    /// <param name="uri">a file Uri, create with Path.GetFullPath(absOrRelativePath) or prefix with file:/// </param>
    public AsStreamReader(Uri uri) : this(new AsConduit(uri))
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="IBytes"/> object.
    /// </summary>
    /// <param name="bytes">a <see cref="IBytes"/> object</param>
    public AsStreamReader(IBytes bytes) : this(new AsConduit(bytes))
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="IText"/> object.
    /// </summary>
    /// <param name="text">some <see cref="IText"/></param>
    public AsStreamReader(IText text) : this(new AsConduit(text))
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="IText"/> object.
    /// </summary>
    /// <param name="text">some <see cref="IText"/></param>
    /// <param name="enc">encoding of the text</param>
    public AsStreamReader(IText text, Encoding enc) : this(new AsConduit(text, enc))
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="IConduit"/> object.
    /// </summary>
    /// <param name="source">a input</param>
    public AsStreamReader(IConduit source) : this(source, Encoding.UTF8)
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="IConduit"/> object.
    /// </summary>
    /// <param name="source">a input</param>
    /// <param name="enc">encoding of the input</param>
    public AsStreamReader(IConduit source, Encoding enc) : this(
        () => new StreamReader(source.Stream(), enc))
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="Stream"/>.
    /// </summary>
    /// <param name="stream">a stream</param>
    public AsStreamReader(Stream stream) : this(stream, Encoding.UTF8)
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="Stream"/> object.
    /// </summary>
    /// <param name="stream">a stream</param>
    /// <param name="enc">encoding of the stream</param>
    public AsStreamReader(Stream stream, Encoding enc) : this(new StreamReader(stream, enc))
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a reader</param>
    private AsStreamReader(StreamReader rdr) : this(() => rdr)
    { }

    /// <summary>
    /// A <see cref="StreamReader"/> encapsulated in a <see cref="IScalar{T}"/>.
    /// </summary>
    /// <param name="src">scalar of a reader</param>
    private AsStreamReader(Func<StreamReader> src) : base(new DeadConduit().Stream())
    {
        this.source = new Sticky<StreamReader>(src);
    }

    public override int Read() => this.source.Value().Read();

    public override Task<string> ReadToEndAsync() =>
        this.source.Value().ReadToEndAsync();

    public override int ReadBlock(char[] buffer, int index, int count) =>
        this.source.Value().ReadBlock(buffer, index, count);

    public override Task<int> ReadAsync(char[] buffer, int index, int count) =>
        this.source.Value().ReadAsync(buffer, index, count);

    public override int Read(char[] cbuf, int off, int len) =>
        this.source.Value().Read(cbuf, off, len);

    public override Task<int> ReadBlockAsync(char[] buffer, int index, int count) =>
        this.source.Value().ReadBlockAsync(buffer, index, count);

    public override string ReadLine() =>
        this.source.Value().ReadLine();

    public override Task<string> ReadLineAsync() =>
        this.source.Value().ReadLineAsync();

    public override string ReadToEnd() =>
        this.source.Value().ReadToEnd();

    public override int Peek() =>
        this.source.Value().Peek();

    protected override void Dispose(bool disposing)
    {
        source.Value().Dispose();
        base.Dispose(disposing);
    }
}

public static partial class IOSmarts
{
    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="char"/> array.
    /// </summary>
    /// <param name="chars">some chars</param>
    public static StreamReader AsStreamReader(this char[] chars) => new AsStreamReader(chars);

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="char"/> array.
    /// </summary>
    /// <param name="chars">some chars</param>
    /// <param name="enc">encoding of the chars</param>
    public static StreamReader AsStreamReader(this char[] chars, Encoding enc) => new AsStreamReader(chars, enc);

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="byte"/> array.
    /// </summary>
    /// <param name="bytes">some bytes</param>
    public static StreamReader AsStreamReader(this byte[] bytes) => new AsStreamReader(bytes);

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="byte"/> array.
    /// </summary>
    /// <param name="bytes">some bytes</param>
    /// <param name="enc">encoding of the bytes</param>
    public static StreamReader AsStreamReader(this byte[] bytes, Encoding enc) => new AsStreamReader(bytes, enc);

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="Url"/> array.
    /// </summary>
    /// <param name="url">a www url starting with http:// or https://</param>
    public static StreamReader AsStreamReader(this Url url) => new AsStreamReader(url);

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="string"/>.
    /// </summary>
    /// <param name="content">a string</param>
    public static StreamReader AsStreamReader(this string content) => new AsStreamReader(content);

    /// <summary>
    /// A <see cref="StreamReader"/> out of a file <see cref="Uri"/> array.
    /// </summary>
    /// <param name="uri">a file Uri, create with Path.GetFullPath(absOrRelativePath) or prefix with file:/// </param>
    public static StreamReader AsStreamReader(this Uri uri) => new AsStreamReader(uri);

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="IBytes"/> object.
    /// </summary>
    /// <param name="bytes">a <see cref="IBytes"/> object</param>
    public static StreamReader AsStreamReader(this IBytes bytes) => new AsStreamReader(bytes);

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="IText"/> object.
    /// </summary>
    /// <param name="text">some <see cref="IText"/></param>
    public static StreamReader AsStreamReader(this IText text) => new AsStreamReader(text);

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="IText"/> object.
    /// </summary>
    /// <param name="text">some <see cref="IText"/></param>
    /// <param name="enc">encoding of the text</param>
    public static StreamReader AsStreamReader(this IText text, Encoding enc) => new AsStreamReader(text, enc);

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="IConduit"/> object.
    /// </summary>
    /// <param name="source">a input</param>
    public static StreamReader AsStreamReader(this IConduit source) => new AsStreamReader(source);

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="IConduit"/> object.
    /// </summary>
    /// <param name="source">a input</param>
    /// <param name="enc">encoding of the input</param>
    public static StreamReader AsStreamReader(this IConduit source, Encoding enc) =>
        new AsStreamReader(source, enc);

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="Stream"/>.
    /// </summary>
    /// <param name="stream">a stream</param>
    public static StreamReader AsStreamReader(this Stream stream) => new AsStreamReader(stream);

    /// <summary>
    /// A <see cref="StreamReader"/> out of a <see cref="Stream"/> object.
    /// </summary>
    /// <param name="stream">a stream</param>
    /// <param name="enc">encoding of the stream</param>
    public static StreamReader AsStreamReader(this Stream stream, Encoding enc) => new AsStreamReader(stream, enc);
}
