using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Tonga.IO;

    /// <summary>
    /// A  <see cref="StreamReader"/> which copies to a <see cref="StreamWriter"/> while reading.
    /// </summary>
    public sealed class TeeStreamReader(StreamReader source, StreamWriter destination) : StreamReader(new DeadStream())
    {
        public override int Read()
        {
            int c = source.Read();
            if (c > -1)
            {
                destination.Write((char)c);
            }
            return c;
        }

        public override int ReadBlock(char[] buffer, int index, int count)
        {
            int done = source.ReadBlock(buffer, index, count);
            if (done >= 0)
            {
                destination.Write(buffer);
            }
            return done;
        }

        public override async Task<string> ReadLineAsync()
        {
            var str = await source.ReadLineAsync();
            await destination.WriteLineAsync(str);
            return str;
        }

        public override async Task<int> ReadBlockAsync(char[] buffer, int index, int count)
        {
            var done = await source.ReadBlockAsync(buffer, index, count);
            if (done > 0)
            {
                await destination.WriteAsync(buffer);
            }
            return done;
        }

        public override async Task<string> ReadToEndAsync()
        {
            var s = await source.ReadToEndAsync();
            await destination.WriteAsync(s);
            return s;
        }

        public override int Peek() => source.Peek();

        public override string ReadToEnd()
        {
            string s = source.ReadToEnd();
            destination.Write(s);
            return s;
        }

        public override string ReadLine()
        {
            var s = base.ReadLine();
            destination.WriteLine(s);
            return s;
        }

        public override async Task<int> ReadAsync(char[] buffer, int index, int count)
        {
            int done = await source.ReadAsync(buffer, index, count);
            if (done >= 0)
            {
                await destination.WriteAsync(buffer);
            }
            return done;
        }

        public override int Read(char[] cbuf, int offset, int length)
        {
            int done = source.Read(cbuf, 0, length);
            if (done >= 0)
            {
                destination.Write(cbuf);
            }
            return done;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                source.Dispose();
            }
            catch (Exception) { }

            try
            {
                destination.Flush();
                destination.Dispose();
            }
            catch (Exception) { }
            base.Dispose(disposing);
        }

        public override Encoding CurrentEncoding => source.CurrentEncoding;
        public override Stream BaseStream => source.BaseStream;

    }
