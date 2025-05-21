using System;
using System.IO;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Tonga.IO
{
    /// <summary>
    /// <see cref="Stream"/> which copies to another <see cref="Stream"/> while writing.
    /// </summary>
    public sealed class TeeOnWriteStream : Stream
    {
        /// <summary>
        /// the target
        /// </summary>
        private readonly Stream _target;

        /// <summary>
        /// the copy
        /// </summary>
        private readonly Stream _copy;

        /// <summary>
        /// <see cref="Stream"/> which copies to another <see cref="Stream"/> while writing.
        /// </summary>
        /// <param name="tgt">the target</param>
        /// <param name="cpy">the copy target</param>
        public TeeOnWriteStream(Stream tgt, Stream cpy) : base()
        {
            this._target = tgt;
            this._copy = cpy;
        }

#pragma warning disable CS1591
        public override void Write(byte[] buffer, int offset, int count)
        {
            try
            {
                this._target.Write(buffer, offset, count);
            }
            finally
            {
                this._copy.Write(buffer, offset, count);
            }
        }


        public override void Flush()
        {
            try
            {
                this._target.Flush();
            }
            finally
            {
                this._copy.Flush();
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                this._target.Dispose();
            }
            catch (Exception) { }
            finally
            {
                try
                {
                    this._copy.Dispose();
                }
                catch (Exception) { }
            }
            base.Dispose(disposing);
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => this._target.Length;

        public override long Position { get { return this._target.Position; } set { this._target.Position = value; } }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            try
            {
                this._target.SetLength(value);
            }
            finally
            {
                this._copy.SetLength(value);
            }
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
#pragma warning restore CS1591
