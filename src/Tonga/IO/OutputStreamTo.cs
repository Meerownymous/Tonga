

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
    /// A writable <see cref="Stream"/> out of other objects.
    /// </summary>
    public sealed class OutputStreamTo : Stream
    {
        /// <summary>
        /// the target
        /// </summary>
        private readonly IScalar<Stream> target;

        /// <summary>
        /// A writable <see cref="Stream"/> of a file path.
        /// </summary>
        /// <param name="path"></param>
        public OutputStreamTo(string path) : this(new OutputTo(path))
        { }

        /// <summary>
        /// A writable <see cref="Stream"/> out of a StreamWriter.
        /// </summary>
        /// <param name="wtr">a writer</param>
        public OutputStreamTo(StreamWriter wtr) : this(wtr, Encoding.UTF8)
        { }

        /// <summary>
        /// A writable <see cref="Stream"/> out of a StreamWriter.
        /// </summary>
        /// <param name="wtr">a writer</param>
        /// <param name="enc">encoding of the writer</param>        
        public OutputStreamTo(StreamWriter wtr, Encoding enc) : this(new OutputTo(wtr, enc))
        { }

        /// <summary>
        /// A writable <see cref="Stream"/> out of a <see cref="IOutput"/>.
        /// </summary>
        /// <param name="output">an output</param>
        public OutputStreamTo(IOutput output) : this(AsScalar._(() => output.Stream()))
        { }

        /// <summary>
        /// A writable <see cref="Stream"/> out of a <see cref="IScalar{Stream}"/> objects.
        /// </summary>
        /// <param name="tgt">the target</param>
        private OutputStreamTo(IScalar<Stream> tgt) : base()
        {
            this.target = Sticky._(tgt);
        }

        public async new void WriteAsync(byte[] buffer, int offset, int length)
        {
            await this.target.Value().WriteAsync(buffer, offset, length);
        }

        public override void WriteByte(byte b)
        {
            this.target.Value().WriteByte(b);
        }

        public override void Write(byte[] buffer, int offset, int length)
        {
            this.target.Value().Write(buffer, offset, length);
        }

        public void Dispose()
        {
            ((IDisposable)this.target.Value()).Dispose();
        }

        public override void Flush()
        {
            this.target.Value().Flush();
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => target.Value().Length;

        public override long Position { get { return target.Value().Position; } set { target.Value().Position = value; } }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.target.Value().Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.target.Value().SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException(); //intended
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
