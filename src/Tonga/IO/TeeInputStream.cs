


using System;
using System.IO;

#pragma warning disable MaxPublicMethodCount
namespace Tonga.IO
{
    /// <summary>
    /// Readable <see cref="Stream"/> that copies input to <see cref="IConduit"/> while reading.
    /// </summary>
    public sealed class TeeInputStream(Stream src, Stream tgt) : Stream
    {
        public int Read()
        {
            var data = (Byte)src.ReadByte();
            tgt.WriteByte(data);
            return data;
        }

        public int Read(byte[] buf) => this.Read(buf, 0, buf.Length);

        public override int Read(byte[] buf, int offset, int len)
        {
            int max = src.Read(buf, offset, len);
            if (max > 0)
            {
                tgt.Write(buf, offset, max);
            }
            return max;
        }

        public long Skip(long num) =>
            src.Seek(num, SeekOrigin.Current);

        public override bool CanRead => src.CanRead;
        public override bool CanSeek => src.CanSeek;
        public override bool CanWrite => src.CanWrite;
        public override long Length => src.Length;
        public override long Position
        {
            get => src.Position;
            set => src.Position = value;
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            try
            {
                src.Flush();
                src.Dispose();
            }
            catch (Exception) { }
            try
            {
                tgt.Flush();
                tgt.Dispose();
            }
            catch (Exception) { }

            base.Dispose(disposing);
        }

        public override void Flush()
        {
            src.Flush();
            tgt.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin) =>
            src.Seek(offset, origin);

        public override void SetLength(long value) =>
            src.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count) =>
            throw new InvalidOperationException("Writing is not supported.");
    }
}
#pragma warning restore CS1591
