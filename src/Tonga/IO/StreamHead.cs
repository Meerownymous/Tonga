

using System;
using System.Collections.Generic;
using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// Rhe first N bytes of the original stream.
    /// </summary>
    public sealed class StreamHead(Stream stream, int length) : Stream
    {
        private readonly List<long> processed = [0L];

        public override bool CanRead => stream.CanRead;
        public override bool CanSeek => stream.CanSeek;
        public override bool CanWrite => stream.CanWrite;
        public override long Length => stream.Length;

        public override long Position
        {
            get => stream.Position;
            set => throw new InvalidOperationException("Setting the position is not supported."); //intended
        }
        public override void Flush() => stream.Flush();

        public override int Read(byte[] buf, int offset, int len)
        {
            var result = 0;
            if (this.processed[0] < length)
            {
                var dif = length - this.processed[0];
                this.processed[0] = length;
                result = stream.Read(buf, offset, (int)dif);
            }
            else
            {
                this.processed[0] = 0;
            }
            return result;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long adjusted;
            if (this.processed[0] + offset > length)
            {
                adjusted = length - this.processed[0];
            }
            else
            {
                adjusted = offset;
            }

            long skipped = stream.Seek(adjusted, SeekOrigin.Begin);
            this.processed[0] += skipped;
            return skipped;
        }

        public override void SetLength(long value) =>
            throw new InvalidOperationException("Setting the length is not supported."); //intended

        public override void Write(byte[] buffer, int offset, int count) =>
            throw new InvalidOperationException("Writing is not supported."); //intended
    }
}
