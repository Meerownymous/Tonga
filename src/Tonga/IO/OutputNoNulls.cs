

using System;
using System.IO;

namespace Tonga.OutputDeck
{
    /// <summary>
    /// <see cref="IOutput"/> which does not accept null as stream
    /// </summary>
    public sealed class OutputNoNulls : IOutput, IDisposable
    {
        /// <summary>
        /// the origin
        /// </summary>
        private readonly IOutput _origin;

        /// <summary>
        /// <see cref="IOutput"/> which does not accept null as stream.
        /// </summary>
        /// <param name="output">the output</param>
        public OutputNoNulls(IOutput output)
        {
            this._origin = output;
        }

        /// <summary>
        /// The stream.
        /// </summary>
        /// <returns>the stream, throws IOException if stream is null</returns>
        public Stream Stream()
        {
            if (this._origin == null)
            {
                throw new IOException("got NULL instead of a valid output");
            }

            var stream = this._origin.Stream();
            if (stream == null)
            {
                throw new IOException("got NULL instead of a valid stream");
            }
            return stream;
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Dispose()
        {
            (_origin as IDisposable)?.Dispose();
        }
    }
}
