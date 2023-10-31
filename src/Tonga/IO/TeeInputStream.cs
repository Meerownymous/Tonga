


using System;
using System.IO;

#pragma warning disable MaxPublicMethodCount
namespace Tonga.IO
{
    /// <summary>
    /// Readable <see cref="Stream"/> that copies input to <see cref="IOutput"/> while reading.
    /// </summary>
    public sealed class TeeInputStream : Stream
    {
        /// <summary>
        /// input
        /// </summary>
        private readonly Stream input;

        /// <summary>
        /// destination
        /// </summary>
        private readonly Stream output;

        /// <summary>
        /// Readable <see cref="Stream"/> that copies input to <see cref="IOutput"/> while reading.
        /// </summary>
        /// <param name="src">the source</param>
        /// <param name="tgt">the destination</param>
        public TeeInputStream(Stream src, Stream tgt) : base()
        {
            this.input = src;
            this.output = tgt;
        }

        public int Read()
        {
            var data = (Byte)this.input.ReadByte();
            if (data >= 0)
            {
                this.output.WriteByte(data);
            }
            return data;
        }

        public int Read(byte[] buf)
        {
            return this.Read(buf, 0, buf.Length);
        }

        public override int Read(byte[] buf, int offset, int len)
        {
            int max = this.input.Read(buf, offset, len);
            if (max > 0)
            {
                this.output.Write(buf, offset, max);
            }
            return max;
        }

        public long Skip(long num)
        {
            return this.input.Seek(num, SeekOrigin.Current);
        }

        public override bool CanRead => input.CanRead;

        public override bool CanSeek => input.CanSeek;

        public override bool CanWrite => input.CanWrite;

        public override long Length => input.Length;

        public override long Position
        {
            get
            {
                return input.Position;
            }
            set
            {
                input.Position = value;
            }
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            try
            {
                this.input.Flush();
                this.input.Dispose();
            }
            catch (Exception) { }
            try
            {
                this.output.Flush();
                this.output.Dispose();
            }
            catch (Exception) { }

            base.Dispose(disposing);
        }

        public override void Flush()
        {
            this.input.Flush();
            this.output.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return input.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            input.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException("Writing is not supported.");
        }
    }
}
#pragma warning restore CS1591
