using System;
using Tonga.Scalar;

namespace Tonga.Func
{
    /// <summary>
    /// Reads all content of a given input and then resets it.
    /// </summary>
    public sealed class ReadAll(IConduit source) : IAction<bool, bool>
    {
        public void Invoke(bool flush = true, bool close = true)
        {
            long size = 0;
            var stream = source.Stream();
            var memorizedPosition = 0L;
            if (stream.CanSeek)
                memorizedPosition = stream.Position;
            byte[] buf = new byte[16 << 10];

            int bytesRead;
            while ((bytesRead = stream.Read(buf, 0, buf.Length)) > 0)
            {
                size += bytesRead;
            }
            if (stream.CanSeek)
                stream.Seek(memorizedPosition, System.IO.SeekOrigin.Begin);
            if (flush) source.Stream().Flush();
            if (close) source.Stream().Close();
        }

        public static ReadAll _(IConduit source) => new(source);
    }
}

