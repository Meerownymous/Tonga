

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Tonga.IO
{
    /// <summary>
    /// A  <see cref="StreamReader"/> which copies to a <see cref="StreamWriter"/> while reading.
    /// </summary>
    public sealed class TeeReader : StreamReader, IDisposable
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly StreamReader _source;

        /// <summary>
        /// the destination
        /// </summary>
        private readonly StreamWriter _destination;

        /// <summary>
        /// A  <see cref="StreamReader"/> which copies to a <see cref="StreamWriter"/> while reading.
        /// </summary>
        /// <param name="reader">the reader</param>
        /// <param name="writer">the copy target</param>
        public TeeReader(StreamReader reader, StreamWriter writer) : base(new DeadStream())
        {
            this._source = reader;
            this._destination = writer;
        }

#pragma warning disable CS1591
        public override int Read()
        {
            int c = this._source.Read();
            if (c > -1)
            {
                this._destination.Write((char)c);
            }
            return c;
        }

        public override int ReadBlock(char[] buffer, int index, int count)
        {
            int done = this._source.ReadBlock(buffer, index, count);
            if (done >= 0)
            {
                this._destination.Write(buffer);
            }
            return done;
        }

        public override async Task<string> ReadLineAsync()
        {
            var str = await this._source.ReadLineAsync();
            this._destination.WriteLine(str);
            return str;
        }

        public override async Task<int> ReadBlockAsync(char[] buffer, int index, int count)
        {
            var done = await this._source.ReadBlockAsync(buffer, index, count);
            if (done > 0)
            {
                await this._destination.WriteAsync(buffer);
            }
            return done;
        }

        public override async Task<string> ReadToEndAsync()
        {
            var s = await this._source.ReadToEndAsync();
            await this._destination.WriteAsync(s);
            return s;
        }

        public override int Peek()
        {
            return this._source.Peek();
        }

        public override string ReadToEnd()
        {
            string s = this._source.ReadToEnd();
            this._destination.Write(s);
            return s;
        }

        public override string ReadLine()
        {
            var s = base.ReadLine();
            this._destination.WriteLine(s);
            return s;
        }

        public override async Task<int> ReadAsync(char[] buffer, int index, int count)
        {
            int done = await this._source.ReadAsync(buffer, index, count);
            if (done >= 0)
            {
                await this._destination.WriteAsync(buffer);
            }
            return done;
        }

        public override int Read(char[] cbuf, int offset, int length)
        {
            int done = this._source.Read(cbuf, 0, length);
            if (done >= 0)
            {
                this._destination.Write(cbuf);
            }
            return done;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                _source.Dispose();
            }
            catch (Exception) { }

            try
            {
                _destination.Flush();
                _destination.Dispose();
            }
            catch (Exception) { }
            base.Dispose(disposing);
        }

        public override Encoding CurrentEncoding => this._source.CurrentEncoding;
        public override Stream BaseStream => this._source.BaseStream;

#pragma warning restore MaxPublicMethodCount // a public methods count maximum
#pragma warning restore CS1591
    }
}
