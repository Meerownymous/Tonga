

using System;
using System.IO;
using Tonga.Func;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// <see cref="IInput"/> that reads once and then returns from cache.
    /// </summary>
    public sealed class Sticky : IInput
    {
        /// <summary>
        /// the cache
        /// </summary>
        private readonly Lazy<byte[]> cache;

        /// <summary>
        /// <see cref="IInput"/> that reads once and then returns from cache.
        /// Closes the input stream after copzing.
        /// </summary>
        /// <param name="input"></param>
        public Sticky(IInput input)
        {
            this.cache =
                new Lazy<byte[]>(() =>
                    {
                        MemoryStream copy = new MemoryStream();
                        ReadAll._(
                            new TeeInput(input, new OutputTo(copy))
                        ).Invoke();
                        input.Stream().Dispose();
                        return copy.ToArray();
                    }
                );
        }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return new MemoryStream(this.cache.Value);
        }
    }
}
