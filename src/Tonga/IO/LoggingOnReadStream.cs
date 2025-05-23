

using System;
using System.Diagnostics;
using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// Logged input stream.
    /// </summary>
    public sealed class LoggingOnReadStream : Stream
    {

        private readonly Stream origin;
        private readonly string source;
        private readonly Action<string> log;
        private long bytes;
        private long time;

        /// <summary>
        /// Logged input stream.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="source"></param>
        public LoggingOnReadStream(Stream input, string source) : this(input, source, (msg) => Debug.WriteLine(msg))
        { }

        /// <summary>
        /// Logged input stream
        /// </summary>
        /// <param name="input"></param>
        /// <param name="source"></param>
        /// <param name="log"></param>
        public LoggingOnReadStream(Stream input, string source, Action<String> log)
        {
            this.origin = input;
            this.source = source;
            this.log = log;
        }

        public override bool CanRead => this.origin.CanRead;

        public override bool CanSeek => this.origin.CanSeek;

        public override bool CanWrite => this.origin.CanWrite;

        public override long Length => this.origin.Length;

        public override long Position { get => this.origin.Position; set => this.origin.Position = value; }

        public override void Flush()
        {
            this.origin.Flush();
        }

        public override int ReadByte()
        {
            byte[] buf = new byte[1];
            int size;
            if (this.Read(buf, 0, buf.Length) == 0)
            {
                size = 0;
            }
            else
            {
                size = Convert.ToInt32(Convert.ToUInt32(buf[0]));
            }
            return size;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            DateTime start = DateTime.UtcNow;
            int byts = this.origin.Read(buffer, offset, count);
            DateTime end = DateTime.UtcNow;

            long millis = (long)end.Subtract(start).TotalMilliseconds;
            if (byts > 0)
            {
                this.bytes += byts;
                this.time += millis;
            }
            var msg = $"Read {this.bytes} byte(s) from {this.source} in {this.time}.";
            log.Invoke(msg);

            return byts;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long skipped = this.origin.Seek(offset, origin);
            var msg = $"Skipped {skipped} byte(s) from {this.source}.";
            log.Invoke(msg);
            return skipped;
        }

        public override void SetLength(long value)
        {
            this.origin.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.origin.Write(buffer, offset, count);
        }
    }
}
