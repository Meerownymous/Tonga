

using System;
using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// Input that does not accept null.
    /// </summary>
    public sealed class ConduitNoNulls(IConduit origin) : IConduit, IDisposable
    {
        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            if (origin == null)
                throw new IOException("got NULL instead of a valid input");

            var stream = origin.Stream();
            if (stream == null)
            {
                throw new IOException("NULL instead of a valid stream");
            }
            return stream;
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Dispose()
        {
            (origin as IDisposable)?.Dispose();
        }
    }
}
