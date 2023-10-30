

using System;
using System.IO;
using System.Text;
using Tonga.Scalar;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
#pragma warning disable CS1591
#pragma warning disable CS0108

namespace Tonga.IO
{
    /// <summary>
    /// A readable stream out of other objects.
    /// </summary>
    public sealed class InputStreamOf : Stream, IDisposable
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly IScalar<Stream> source;

        /// <summary>
        /// A readable stream out of a file Uri.
        /// </summary>
        /// <param name="path">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
        public InputStreamOf(Uri path) : this(new InputOf(path))
        { }

        /// <summary>
        /// A readable stream out of a www Url.
        /// </summary>
        /// <param name="url">a url starting with http:// or https://</param>
        public InputStreamOf(Url url) : this(new InputOf(url))
        { }

        /// <summary>
        /// A readable stream out of Bytes.
        /// </summary>
        /// <param name="bytes">a <see cref="IBytes"/> object which will be copied to memory</param>
        public InputStreamOf(IBytes bytes) : this(new InputOf(bytes))
        { }
        /// <summary>
        /// A readable stream out of a Byte array.
        /// </summary>
        /// <param name="bytes">a <see cref="byte"/> array</param>
        public InputStreamOf(byte[] bytes) : this(new InputOf(bytes))
        { }

        /// <summary>
        /// A readable stream out of a Text.
        /// </summary>
        /// <param name="text">some <see cref="IText"/></param>
        public InputStreamOf(IText text) : this(text, Encoding.UTF8)
        { }

        /// <summary>
        /// A readable stream out of a string.
        /// </summary>
        /// <param name="text">some string</param>
        public InputStreamOf(String text) : this(
            new InputOf(text))
        { }

        /// <summary>
        /// A readable stream out of a string.
        /// </summary>
        /// <param name="text">some <see cref="string"/></param>
        /// <param name="enc"><see cref="Encoding"/> of the string</param>
        public InputStreamOf(String text, Encoding enc) : this(
            new InputOf(text, enc))
        { }

        /// <summary>
        /// A readable stream out of a <see cref="IText"/> with <see cref="Encoding"/>.
        /// </summary>
        /// <param name="text">some <see cref="IText"/></param>
        /// <param name="enc"><see cref="Encoding"/> of the text</param>
        public InputStreamOf(IText text, Encoding enc) : this(
            new InputOf(text, enc))
        { }

        /// <summary>
        /// A readable stream out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a streamreader</param>
        public InputStreamOf(StreamReader rdr) : this(new InputOf(rdr))
        { }

        /// <summary>
        /// A readable stream out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a streamreader</param>
        /// <param name="max">maximum buffer size</param>
        public InputStreamOf(StreamReader rdr, int max = 16 << 10) : this(
            new InputOf(rdr, Encoding.UTF8, max))
        { }

        /// <summary>
        /// A readable stream out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a streamreader</param>
        /// <param name="enc">encoding of the reader</param>
        /// <param name="max">maximum buffer size</param>
        public InputStreamOf(StreamReader rdr, Encoding enc, int max = 16 << 10) : this(new InputOf(rdr, enc, max))
        { }

        /// <summary>
        /// A readable stream out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">the input</param>
        public InputStreamOf(IInput input) : this(AsScalar._(input.Stream))
        { }

        /// <summary>
        /// A readable stream out of a <see cref="Func"/> that returns a <see cref="Stream"/>.
        /// </summary>
        /// <param name="input">the input</param>
        public InputStreamOf(Func<Stream> input) : this(AsScalar._(input))
        { }

        /// <summary>
        /// A readable stream out of a <see cref="IScalar{T}"/> that encapsulates a <see cref="Stream"/>.
        /// </summary>
        /// <param name="src">the source</param>
        private InputStreamOf(IScalar<Stream> src) : base()
        {
            this.source = src;
        }

        public override int Read(byte[] buf, int offset, int len)
        {
            return this.source.Value().Read(buf, offset, len);
        }

        public override bool CanRead => this.source.Value().CanRead;

        public override bool CanSeek => this.source.Value().CanSeek;

        public override bool CanWrite => this.source.Value().CanWrite;

        public override long Length => this.source.Value().Length;

        public override long Position
        {
            get
            {
                return this.source.Value().Position;
            }
            set
            {
                throw new NotImplementedException(); //intended
            }
        }

        public override void Flush()
        {
            source.Value().Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return source.Value().Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            source.Value().Dispose();
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
