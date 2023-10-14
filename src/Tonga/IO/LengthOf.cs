

namespace Tonga.IO
{
    /// <summary>
    /// Length of <see cref="IInput"/>. (Self-Disposing)
    /// </summary>
    public sealed class LengthOf : IScalar<long>
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly IInput _source;

        /// <summary>
        /// buffer size
        /// </summary>
        private readonly int _size;

        /// <summary>
        /// Length of <see cref="IInput"/> by reading all bytes.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="max">maximum buffer size</param>
        public LengthOf(IInput input, int max = 16 << 10)
        {
            this._source = input;
            this._size = max;
        }

        /// <summary>
        /// Get the length. (Self-Disposing)
        /// </summary>
        /// <returns>the length</returns>
        public long Value()
        {
            long length = 0L;
            using (var stream = _source.Stream())
            {
                byte[] buf = new byte[this._size];

                int bytesRead;
                while ((bytesRead = stream.Read(buf, 0, buf.Length)) > 0)
                {
                    length += (long)bytesRead;
                }
            }
            return length;
        }
    }
}
