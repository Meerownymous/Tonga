

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tonga.Scalar;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
#pragma warning disable CS1591
namespace Tonga.IO
{
    /// <summary>
    /// A <see cref="StreamReader"/> out of other objects.
    /// </summary>
    public sealed class AsReader : StreamReader, IDisposable
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly IScalar<StreamReader> source;

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="char"/> array.
        /// </summary>
        /// <param name="chars">some chars</param>
        public AsReader(params char[] chars) : this(new AsConduit(chars))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="char"/> array.
        /// </summary>
        /// <param name="chars">some chars</param>
        /// <param name="enc">encoding of the chars</param>
        public AsReader(char[] chars, Encoding enc) : this(new AsConduit(chars, enc))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="byte"/> array.
        /// </summary>
        /// <param name="bytes">some bytes</param>
        public AsReader(byte[] bytes) : this(new AsConduit(bytes))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="byte"/> array.
        /// </summary>
        /// <param name="bytes">some bytes</param>
        /// <param name="enc">encoding of the bytes</param>
        public AsReader(byte[] bytes, Encoding enc) : this(new AsConduit(bytes), enc)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="Url"/> array.
        /// </summary>
        /// <param name="url">a www url starting with http:// or https://</param>
        public AsReader(Url url) : this(new AsConduit(url))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="string"/>.
        /// </summary>
        /// <param name="content">a string</param>
        public AsReader(string content) : this(new AsConduit(content))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a file <see cref="Uri"/> array.
        /// </summary>
        /// <param name="uri">a file Uri, create with Path.GetFullPath(absOrRelativePath) or prefix with file:/// </param>
        public AsReader(Uri uri) : this(new AsConduit(uri))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="IBytes"/> object.
        /// </summary>
        /// <param name="bytes">a <see cref="IBytes"/> object</param>
        public AsReader(IBytes bytes) : this(new AsConduit(bytes))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="IText"/> object.
        /// </summary>
        /// <param name="text">some <see cref="IText"/></param>
        public AsReader(IText text) : this(new AsConduit(text))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="IText"/> object.
        /// </summary>
        /// <param name="text">some <see cref="IText"/></param>
        /// <param name="enc">encoding of the text</param>
        public AsReader(IText text, Encoding enc) : this(new AsConduit(text, enc))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="IConduit"/> object.
        /// </summary>
        /// <param name="source">a input</param>
        public AsReader(IConduit source) : this(source, Encoding.UTF8)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="IConduit"/> object.
        /// </summary>
        /// <param name="source">a input</param>
        /// <param name="enc">encoding of the input</param>
        public AsReader(IConduit source, Encoding enc) : this(
            () => new StreamReader(source.Stream(), enc))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">a stream</param>
        public AsReader(Stream stream) : this(stream, Encoding.UTF8)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="Stream"/> object.
        /// </summary>
        /// <param name="stream">a stream</param>
        /// <param name="enc">encoding of the stream</param>
        public AsReader(Stream stream, Encoding enc) : this(new StreamReader(stream, enc))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a reader</param>
        private AsReader(StreamReader rdr) : this(() => rdr)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="Func{TResult}"/> that returns a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="src">func retrieving a reader</param>
        private AsReader(Func<StreamReader> src) : this(AsScalar._(src))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> encapsulated in a <see cref="IScalar{T}"/>.
        /// </summary>
        /// <param name="src">scalar of a reader</param>
        private AsReader(IScalar<StreamReader> src) : base(new DeadConduit().Stream())
        {
            this.source = Scalar.Sticky._(src);
        }

        public override int Read()
        {
            return this.source.Value().Read();
        }

        public override Task<string> ReadToEndAsync()
        {
            return this.source.Value().ReadToEndAsync();
        }

        public override int ReadBlock(char[] buffer, int index, int count)
        {
            return this.source.Value().ReadBlock(buffer, index, count);
        }

        public override Task<int> ReadAsync(char[] buffer, int index, int count)
        {
            return this.source.Value().ReadAsync(buffer, index, count);
        }

        public override int Read(char[] cbuf, int off, int len)
        {
            return this.source.Value().Read(cbuf, off, len);
        }

        public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
        {
            return this.source.Value().ReadBlockAsync(buffer, index, count);
        }

        public override string ReadLine()
        {
            return this.source.Value().ReadLine();
        }

        public override Task<string> ReadLineAsync()
        {
            return this.source.Value().ReadLineAsync();
        }

        public override string ReadToEnd()
        {
            return this.source.Value().ReadToEnd();
        }

        public override int Peek()
        {
            return this.source.Value().Peek();
        }

        protected override void Dispose(bool disposing)
        {
            source.Value().Dispose();
            base.Dispose(disposing);
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
