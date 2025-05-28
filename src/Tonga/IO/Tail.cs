

using System;
using System.IO;
using Tonga.Number;

namespace Tonga.IO
{
    /// <summary>
    /// Input showing only last N bytes of the stream.
    /// </summary>
    public sealed class Tail(IConduit origin, int howMuch, int max) : IConduit, IDisposable
    {
        /// <summary>
        /// Input showing only last N bytes of the stream.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="howMuch"></param>
        public Tail(IConduit origin, int howMuch) : this(origin, howMuch, 16384)
        { }

        public Stream Stream()
        {
            if (max < howMuch)
            {
                throw new ArgumentException($"Can't tail {howMuch} bytes if buffer is set to {max}");
            }
            var buffer = new byte[max];
            var response = new byte[howMuch];
            int num = 0;
            var stream = origin.Stream();

            for (int read = stream.Read(buffer, 0, buffer.Length); read > 0; read = stream.Read(buffer, 0, buffer.Length))
            {
                if (read < max && read < howMuch)
                {
                    num = this.CopyPartial(buffer, response, num, read);
                }
                else
                {
                    num = this.Copy(buffer, response, read);
                }
            }
            return new MemoryStream(response, 0, num);

        }



        private int Copy(byte[] buffer, byte[] response, int read)
        {
            Array.Copy(buffer, read - howMuch, response, 0, howMuch);
            return new MinOf(howMuch, read).ToInt();
        }


        private int CopyPartial(byte[] buffer, byte[] response, int num, int read)
        {
            int result;
            if (num > 0)
            {
                Array.Copy(response, read, response, 0, howMuch - read);
                Array.Copy(buffer, 0, response, howMuch - read, read);
                result = howMuch;
            }
            else
            {
                Array.Copy(buffer, 0, response, 0, read);
                result = read;
            }
            return result;
        }

        public void Dispose()
        {
            try
            {
                (origin as IDisposable)?.Dispose();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
