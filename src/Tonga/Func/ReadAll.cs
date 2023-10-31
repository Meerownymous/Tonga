using System;
using Tonga.Scalar;

namespace Tonga.Func
{
    /// <summary>
    /// Reads all content of a given input and then resets it.
    /// </summary>
    public sealed class ReadAll : IAction<bool, bool>
    {
        private readonly IInput input;

        /// <summary>
        /// Reads all content of a given input and then resets it.
        /// </summary>
        public ReadAll(IInput input)
        {
            this.input = input;
        }

        public void Invoke(bool flush = true, bool close = true)
        {
            long size = 0;
            var stream = input.Stream();
            var memorizedPosition = 0L;
            if (stream.CanSeek)
                memorizedPosition = stream.Position;
            byte[] buf = new byte[16 << 10];

            int bytesRead;
            while ((bytesRead = stream.Read(buf, 0, buf.Length)) > 0)
            {
                size += (long)bytesRead;
            }
            if (stream.CanSeek)
                stream.Seek(memorizedPosition, System.IO.SeekOrigin.Begin);
            if (flush) this.input.Stream().Flush();
            if (close) this.input.Stream().Close();
        }

        public static ReadAll _(IInput input) => new ReadAll(input);
    }
}

