

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tonga.Scalar;

#pragma warning disable MaxVariablesCount // Four fields maximum
#pragma warning disable Immutability // Fields are readonly or constant
#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Tonga.IO
{
    /// <summary>
    /// <see cref="StreamWriter"/> as a writable <see cref="Stream"/>.
    /// </summary>
    public sealed class WriterAsOutputStream : Stream, IDisposable
    {
        /// <summary>
        /// the writer
        /// </summary>
        private readonly StreamWriter writer;

        /// <summary>
        /// encoding of the writer
        /// </summary>
        private readonly IScalar<Decoder> decoder;

        /// <summary>
        /// <see cref="StreamWriter"/> as a writable <see cref="Stream"/>.
        /// </summary>
        public WriterAsOutputStream(StreamWriter writer) : this(writer, Encoding.UTF8)
        { }

        /// <summary>
        /// <see cref="StreamWriter"/> as a writable <see cref="Stream"/>.
        /// </summary>
        public WriterAsOutputStream(StreamWriter writer, string encoding) : this(writer, Encoding.GetEncoding(encoding))
        { }

        /// <summary>
        /// <see cref="StreamWriter"/> as a writable <see cref="Stream"/>.
        /// </summary>
        public WriterAsOutputStream(StreamWriter writer, Encoding encoding) : this(
                writer,
                AsScalar._(() =>
                {
                    var decoder = encoding.GetDecoder();
                    decoder.Fallback = DecoderFallback.ExceptionFallback;
                    return decoder;
                })
            )
        { }

        /// <summary>
        /// <see cref="StreamWriter"/> as a writable <see cref="Stream"/>.
        /// </summary>
        public WriterAsOutputStream(StreamWriter writer, IScalar<Decoder> decoder) : base()
        {
            this.writer = writer;
            this.decoder = decoder;
        }

        /// <summary>
        /// <see cref="StreamWriter"/> as a writable <see cref="Stream"/>.
        /// </summary>
        public void Write(int data)
        {
            this.Write(new byte[] { (byte)data }, 0, 1);
        }

        public void Write(byte[] buffer)
        {
            this.Write(buffer, 0, buffer.Length);
        }

        public override void Write(byte[] buffer, int offset, int length)
        {
            int left = length;
            int start = offset;
            while (left > 0)
            {
                int taken = this.Next(buffer, start, left);
                start += taken;
                left -= taken;
            }
        }

        public override async Task WriteAsync(byte[] buffer, int offset, int length, CancellationToken cancellationToken)
        {
            int left = length;
            int start = offset;
            while (left > 0)
            {
                int taken = await this.NextAsync(buffer, start, left);
                start += taken;
                left -= taken;
            }
        }

        private int Next(byte[] buffer, int offset, int length)
        {
            var charCount = this.decoder.Value().GetCharCount(buffer, 0, length);
            char[] chars = new char[charCount];
            this.decoder.Value().GetChars(buffer, 0, charCount, chars, 0);

            long max = Math.Min((long)length, charCount);


            this.writer.Write(chars);
            this.writer.Flush();

            return (int)Math.Min((long)length, charCount);
        }

        private async Task<int> NextAsync(byte[] buffer, int offset, int length)
        {
            var charCount = this.decoder.Value().GetCharCount(buffer, 0, length);
            char[] chars = new char[charCount];
            this.decoder.Value().GetChars(buffer, 0, charCount, chars, 0);

            long max = Math.Min((long)length, charCount);


            await this.writer.WriteAsync(chars);
            await this.writer.FlushAsync();

            return (int)Math.Min((long)length, charCount);
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length
        {
            get => throw new NotImplementedException();
        }

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



        public override void Flush()
        {
            this.writer.Flush();
        }

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
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                this.writer.Flush();
            }
            catch (Exception) { }

            try
            {
                this.writer.Dispose();
            }
            catch (Exception) { }
            base.Dispose(disposing);
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
#pragma warning restore Immutability // Fields are readonly or constant
#pragma warning restore MaxVariablesCount // Four fields maximum
