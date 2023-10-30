

using System.IO;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// <see cref="IInput"/> that reads once and then returns from cache.
    /// </summary>
    public sealed class StickyInput : IInput
    {
        /// <summary>
        /// the cache
        /// </summary>
        private readonly IScalar<byte[]> cache;

        /// <summary>
        /// <see cref="IInput"/> that reads once and then returns from cache.
        /// Closes the input stream after first read.
        /// </summary>
        /// <param name="input"></param>
        public StickyInput(IInput input)
        {
            this.cache =
                Sticky._(
                    AsScalar._(() =>
                    {
                        MemoryStream baos = new MemoryStream();
                        new LengthOf(
                            new TeeInput(input, new OutputTo(baos))
                        ).Value();
                        input.Stream().Dispose();
                        return baos.ToArray();
                    })
                );
        }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return new MemoryStream(this.cache.Value());
        }
    }
}
