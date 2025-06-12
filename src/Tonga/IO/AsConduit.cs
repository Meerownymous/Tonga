

using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tonga.Bytes;

namespace Tonga.IO;

/// <summary>
/// Input out of other things.
/// </summary>
public sealed class AsConduit : IConduit, IDisposable
{
    /// <summary>
    /// the input
    /// </summary>
    private readonly Lazy<Stream> origin;

    /// <summary>
    /// Input out of a file Uri.
    /// </summary>
    /// <param name="uri">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
    public AsConduit(Uri uri) : this(
        () =>
        {
            Stream result;
            if (uri.HostNameType == UriHostNameType.Dns)
            {
                var stream = Task.Run(async () =>
                {
                    using HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync(uri.AbsoluteUri);
                    HttpContent content = response.Content;
                    {
                        return await content.ReadAsStreamAsync();
                    }
                });
                result = stream.Result;
            }
            else
                result = new FileStream(Uri.UnescapeDataString(uri.LocalPath), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            return result;
        })
    { }

    /// <summary>
    /// Input out of a file Uri.
    /// </summary>
    /// <param name="file">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
    public AsConduit(FileInfo file) : this(() => file)
    { }

    /// <summary>
    /// Input out of a scalar of a file Uri.
    /// </summary>
    /// <param name="file">scalar of a uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
    public AsConduit(Func<FileInfo> file) : this(
        () => new FileStream(Uri.UnescapeDataString(file().FullName), FileMode.Open, FileAccess.ReadWrite))
    { }

    /// <summary>
    /// Input out of a Url.
    /// </summary>
    /// <param name="url">a url starting with http:// or https://</param>
    public AsConduit(Url url) : this(() => url)
    { }

    /// <summary>
    /// Input out of a Url scalar.
    /// </summary>
    /// <param name="url">a url starting with http:// or https://</param>
    public AsConduit(Func<Url> url) : this(() =>
        {
            var stream = Task.Run(async () =>
            {
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url().Value());
                HttpContent content = response.Content;
                {
                    return await content.ReadAsStreamAsync();
                }
            });
            return stream.Result;
        })
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="rdr">a streamreader</param>
    public AsConduit(StreamReader rdr) : this(new AsBytes(rdr))
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="rdr">a streamreader</param>
    /// <param name="enc">encoding of the reader</param>
    public AsConduit(StreamReader rdr, Encoding enc) : this(new AsBytes(rdr, enc))
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="str">a stream</param>
    /// <param name="enc">encoding of the stream</param>
    public AsConduit(Stream str, Encoding enc) : this(new AsBytes(new StreamReader(str), enc))
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="rdr">a streamreader</param>
    /// <param name="enc">encoding of the reader</param>
    /// <param name="max">maximum buffer size</param>
    public AsConduit(StreamReader rdr, Encoding enc, int max = 16 << 10) : this(new AsBytes(rdr, enc, max))
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="builder">a stringbuilder</param>
    public AsConduit(StringBuilder builder) : this(builder, Encoding.UTF8)
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="builder">a stringbuilder</param>
    /// <param name="enc">encoding of the stringbuilder</param>
    public AsConduit(StringBuilder builder, Encoding enc) : this(() =>
        new MemoryStream(
            new AsBytes(builder, enc).Raw()
        )
    )
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="chars">some chars</param>
    public AsConduit(params char[] chars) : this(new AsBytes(chars))
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="chars">some chars</param>
    /// <param name="enc">encoding of the chars</param>
    public AsConduit(char[] chars, Encoding enc) : this(new AsBytes(chars, enc))
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="text">some text</param>
    public AsConduit(String text) : this(new AsBytes(text))
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="text">some <see cref="string"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the string</param>
    public AsConduit(String text, Encoding enc) : this(new AsBytes(text, enc))
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="text">some <see cref="IText"/></param>
    public AsConduit(IText text) : this(new AsBytes(text))
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="text">some <see cref="IText"/></param>
    /// <param name="encoding"><see cref="Encoding"/> of the text</param>
    public AsConduit(IText text, Encoding encoding) : this(new AsBytes(text, encoding))
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="error"><see cref="Exception"/> to serialize</param>
    public AsConduit(Exception error) : this(new AsBytes(error))
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="bytes">a <see cref="byte"/> array</param>
    public AsConduit(byte[] bytes) : this(new AsBytes(bytes))
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">a <see cref="IBytes"/> object which will be copied to memory</param>
    public AsConduit(IBytes src) : this(
        () =>
        {
            var b = src.Raw();
            var m = new MemoryStream();
            m.Write(b, 0, b.Length);
            m.Seek(0, SeekOrigin.Begin);
            return m;
        }
    )
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="stream">a <see cref="Stream"/> as input</param>
    public AsConduit(Stream stream) : this(() => stream)
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="stream">the input <see cref="Stream"/></param>
    public AsConduit(Func<Stream> stream)
    {
        this.origin = new Lazy<Stream>(stream);
    }

    /// <summary>
    /// Get the stream.
    /// </summary>
    /// <returns>the stream</returns>
    public Stream Stream() => this.origin.Value;

    /// <summary>
    /// Clean up.
    /// </summary>
    public void Dispose() => this.origin.Value.Dispose();
}

public static partial class IOSmarts
{
    /// <summary>
    /// Input out of a file Uri.
    /// </summary>
    /// <param name="uri">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
    public static IConduit AsConduit(this Uri uri) => new AsConduit(uri);
    /// <summary>
    /// Input out of a file Uri.
    /// </summary>
    /// <param name="file">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
    public static IConduit AsConduit(this FileInfo file) => new AsConduit(file);

    /// <summary>
    /// Input out of a scalar of a file Uri.
    /// </summary>
    /// <param name="file">scalar of a uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
    public static IConduit AsConduit(this Func<FileInfo> file) => new AsConduit(file);

    /// <summary>
    /// Input out of a Url.
    /// </summary>
    /// <param name="url">a url starting with http:// or https://</param>
    public static IConduit AsConduit(this Url url) => new AsConduit(url);

    /// <summary>
    /// Input out of a Url scalar.
    /// </summary>
    /// <param name="url">a url starting with http:// or https://</param>
    public static IConduit AsConduit(this Func<Url> url) => new AsConduit(url);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="rdr">a streamreader</param>
    public static IConduit AsConduit(this StreamReader rdr) => new AsConduit(rdr);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="rdr">a streamreader</param>
    /// <param name="enc">encoding of the reader</param>
    public static IConduit AsConduit(this StreamReader rdr, Encoding enc) => new AsConduit(rdr, enc);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="str">a stream</param>
    /// <param name="enc">encoding of the stream</param>
    public static IConduit AsConduit(this Stream str, Encoding enc) =>
        new AsConduit(str, enc);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="rdr">a streamreader</param>
    /// <param name="enc">encoding of the reader</param>
    /// <param name="max">maximum buffer size</param>
    public static IConduit AsConduit(this StreamReader rdr, Encoding enc, int max = 16 << 10) =>
        new AsConduit(rdr, enc, max);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="builder">a stringbuilder</param>
    public static IConduit AsConduit(this StringBuilder builder) => new AsConduit(builder);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="builder">a stringbuilder</param>
    /// <param name="enc">encoding of the stringbuilder</param>
    public static IConduit AsConduit(this StringBuilder builder, Encoding enc) => new AsConduit(builder, enc);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="chars">some chars</param>
    public static IConduit AsConduit(this char[] chars) => new AsConduit(chars);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="chars">some chars</param>
    /// <param name="enc">encoding of the chars</param>
    public static IConduit AsConduit(this char[] chars, Encoding enc) => new AsConduit(chars, enc);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="text">some text</param>
    public static IConduit AsConduit(this String text) => new AsConduit(text);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="text">some <see cref="string"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the string</param>
    public static IConduit AsConduit(this String text, Encoding enc) => new AsConduit(text, enc);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="text">some <see cref="IText"/></param>
    public static IConduit AsConduit(this IText text) => new AsConduit(text);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="text">some <see cref="IText"/></param>
    /// <param name="encoding"><see cref="Encoding"/> of the text</param>
    public static IConduit AsConduit(this IText text, Encoding encoding) => new AsConduit(text, encoding);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="error"><see cref="Exception"/> to serialize</param>
    public static IConduit AsConduit(this Exception error) => new AsConduit(error);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="bytes">a <see cref="byte"/> array</param>
    public static IConduit AsConduit(this byte[] bytes) => new AsConduit(bytes);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">a <see cref="IBytes"/> object which will be copied to memory</param>
    public static IConduit AsConduit(this IBytes src) => new AsConduit(src);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="stream">a <see cref="Stream"/> as input</param>
    public static IConduit AsConduit(this Stream stream) => new AsConduit(stream);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="stream">the input <see cref="Stream"/></param>
    public static IConduit AsConduit(this Func<Stream> stream) => new AsConduit(stream);
}
