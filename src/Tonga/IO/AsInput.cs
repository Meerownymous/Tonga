

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tonga.Bytes;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// Input out of other things.
    /// </summary>
    public sealed class AsInput : IInput, IDisposable
    {
        /// <summary>
        /// the input
        /// </summary>
        private readonly IScalar<Stream> origin;

        /// <summary>
        /// Input out of a file Uri.
        /// </summary>
        /// <param name="file">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
        public AsInput(Uri file) : this(
            () =>
            {
                if (file.HostNameType == UriHostNameType.Dns)
                {
                    return WebRequest.Create(file).GetResponse().GetResponseStream();
                }
                else
                {
                    return new FileStream(Uri.UnescapeDataString(file.LocalPath), FileMode.Open, FileAccess.Read);
                }
            })
        { }

        /// <summary>
        /// Input out of a file Uri.
        /// </summary>
        /// <param name="file">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
        public AsInput(FileInfo file) : this(AsScalar._(file))
        { }

        /// <summary>
        /// Input out of a scalar of a file Uri.
        /// </summary>
        /// <param name="file">scalar of a uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
        public AsInput(IScalar<FileInfo> file) : this(
            () => new FileStream(Uri.UnescapeDataString(file.Value().FullName), FileMode.Open, FileAccess.Read))
        { }

        /// <summary>
        /// Input out of a Url.
        /// </summary>
        /// <param name="url">a url starting with http:// or https://</param>
        public AsInput(Url url) : this(AsScalar._(url))
        { }

        /// <summary>
        /// Input out of a Url scalar.
        /// </summary>
        /// <param name="url">a url starting with http:// or https://</param>
        public AsInput(IScalar<Url> url) : this(() =>
            {
                var stream = Task.Run(async () =>
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(url.Value().Value());
                        HttpContent content = response.Content;
                        {
                            return await content.ReadAsStreamAsync();
                        }
                    }
                });

                return stream.Result;
            })
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="rdr">a stringreader</param>
        public AsInput(StringReader rdr) : this(new AsBytes(rdr))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="rdr">a streamreader</param>
        public AsInput(StreamReader rdr) : this(new AsBytes(rdr))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="rdr">a streamreader</param>
        /// <param name="enc">encoding of the reader</param>
        public AsInput(StreamReader rdr, Encoding enc) : this(new AsBytes(rdr, enc))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="str">a stream</param>
        /// <param name="enc">encoding of the stream</param>
        public AsInput(Stream str, Encoding enc) : this(new AsBytes(new StreamReader(str), enc))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="rdr">a streamreader</param>
        /// <param name="enc">encoding of the reader</param>
        /// <param name="max">maximum buffer size</param>
        public AsInput(StreamReader rdr, Encoding enc, int max = 16 << 10) : this(new AsBytes(rdr, enc, max))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="builder">a stringbuilder</param>
        public AsInput(StringBuilder builder) : this(builder, Encoding.UTF8)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="builder">a stringbuilder</param>
        /// <param name="enc">encoding of the stringbuilder</param>
        public AsInput(StringBuilder builder, Encoding enc) : this(
            AsScalar._<Stream>(() => new MemoryStream(
                new AsBytes(builder, enc).Bytes())
            )
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="chars">some chars</param>
        public AsInput(params char[] chars) : this(new AsBytes(chars))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="chars">some chars</param>
        /// <param name="enc">encoding of the chars</param>
        public AsInput(char[] chars, Encoding enc) : this(new AsBytes(chars, enc))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="text">some text</param>
        public AsInput(String text) : this(new AsBytes(text))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="text">some <see cref="string"/></param>
        /// <param name="enc"><see cref="Encoding"/> of the string</param>
        public AsInput(String text, Encoding enc) : this(new AsBytes(text, enc))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="text">some <see cref="IText"/></param>
        public AsInput(IText text) : this(new AsBytes(text))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="text">some <see cref="IText"/></param>
        /// <param name="encoding"><see cref="Encoding"/> of the text</param>
        public AsInput(IText text, Encoding encoding) : this(new AsBytes(text, encoding))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="error"><see cref="Exception"/> to serialize</param>
        public AsInput(Exception error) : this(new AsBytes(error))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="bytes">a <see cref="byte"/> array</param>
        public AsInput(byte[] bytes) : this(new AsBytes(bytes))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">a <see cref="IBytes"/> object which will be copied to memory</param>
        public AsInput(IBytes src) : this(
            AsScalar._<Stream>(
                () =>
                {
                    var b = src.Bytes();
                    var m = new MemoryStream();
                    m.Write(b, 0, b.Length);
                    m.Seek(0, SeekOrigin.Begin);
                    return m;
                }
            )
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="stream">a <see cref="Stream"/> as input</param>
        public AsInput(Stream stream) : this(AsScalar._(stream))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">a function retrieving a <see cref="Stream"/> as input</param>
        public AsInput(Func<Stream> fnc) : this(AsScalar._(fnc))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="stream">the input <see cref="Stream"/></param>
        private AsInput(IScalar<Stream> stream)
        {
            this.origin =
                StickyIf._(
                    current => current.CanRead,
                    stream
                );
        }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return this.origin.Value();
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Dispose()
        {
            Stream().Dispose();
        }
    }
}
