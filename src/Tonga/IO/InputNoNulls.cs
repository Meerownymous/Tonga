

using System;
using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// Input that does not accept null.
    /// </summary>
    public sealed class InputNoNulls : IInput, IDisposable
    {
        /// <summary>
        /// original input
        /// </summary>
        private readonly IInput _origin;

        /// <summary>
        /// Input that does not accept null.
        /// </summary>
        /// <param name="input">input to check</param>
        public InputNoNulls(IInput input)
        {
            this._origin = input;
        }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            if (_origin == null)
            {
                throw new IOException("got NULL instead of a valid input");
            }

            var stream = _origin.Stream();
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
            (_origin as IDisposable)?.Dispose();
        }
    }
}
