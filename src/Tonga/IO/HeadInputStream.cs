

using System;
using System.Collections.Generic;
using System.IO;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// Input stream that only shows the first N bytes of the original stream.
    /// </summary>
    public sealed class HeadInputStream : Stream
    {
        private readonly Stream origin;
        private readonly long length;
        private readonly IList<long> processed;

        /// <summary>
        /// Input stream that only shows the first N bytes of the original stream.
        /// </summary>
        /// <param name="origin">Stream</param>
        /// <param name="length">Length</param>
        public HeadInputStream(Stream origin, int length)
        {
            this.origin = origin;
            this.length = length;
            this.processed = new List<long>() { 0 };
        }


        public override bool CanRead => this.origin.CanRead;

        public override bool CanSeek => this.origin.CanSeek;

        public override bool CanWrite => this.origin.CanWrite;

        public override long Length => this.origin.Length;

        public override long Position
        {
            get
            {
                return this.origin.Position;
            }
            set
            {
                throw new InvalidOperationException("Setting the position is not supported."); //intended
            }
        }
        public override void Flush()
        {
            origin.Flush();
        }


        public override int Read(byte[] buf, int offset, int len)
        {
            if (this.processed[0] < this.length)
            {
                var dif = this.length - this.processed[0];
                this.processed[0] = this.length;
                return this.origin.Read(buf, offset, (int)(dif));
            }
            else
            {
                this.processed[0] = 0;
                return 0;
            }


        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long adjusted;
            if (this.processed[0] + offset > this.length)
            {
                adjusted = this.length - this.processed[0];
            }
            else
            {
                adjusted = offset;
            }

            long skipped = this.origin.Seek(adjusted, SeekOrigin.Begin);
            this.processed[0] = this.processed[0] + skipped;
            return skipped;
        }

        public override void SetLength(long value)
        {
            throw new InvalidOperationException("Setting the length is not supported."); //intended
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException("Writing is not supported."); //intended
        }
    }
}
