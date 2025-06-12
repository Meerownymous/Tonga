

using System;
using System.Diagnostics;
using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// Logged input stream.
    /// </summary>
    public sealed class LoggingOnReadStream(Stream origin, string source, Action<String> log) : Stream
    {
        private long bytes;
        private long time;

        /// <summary>
        /// Logged input stream.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="source"></param>
        public LoggingOnReadStream(Stream input, string source) : this(input, source, msg => Debug.WriteLine(msg))
        { }

        public override bool CanRead => origin.CanRead;
        public override bool CanSeek => origin.CanSeek;
        public override bool CanWrite => origin.CanWrite;
        public override long Length => origin.Length;
        public override long Position { get => origin.Position; set => origin.Position = value; }
        public override void Flush() => origin.Flush();

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
            int byts = origin.Read(buffer, offset, count);
            DateTime end = DateTime.UtcNow;

            long millis = (long)end.Subtract(start).TotalMilliseconds;
            if (byts > 0)
            {
                this.bytes += byts;
                this.time += millis;
            }
            var msg = $"Read {this.bytes} byte(s) from {source} in {this.time}.";
            log.Invoke(msg);

            return byts;
        }

        public override long Seek(long offset, SeekOrigin seekOrigin)
        {
            long skipped = origin.Seek(offset, seekOrigin);
            var msg = $"Skipped {skipped} byte(s) from {source}.";
            log.Invoke(msg);
            return skipped;
        }

        public override void SetLength(long value) => origin.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count) =>
            origin.Write(buffer, offset, count);

        public override void Close() => origin.Close();
    }
}
