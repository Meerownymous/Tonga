

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
        private readonly Lazy<MemoryStream> cache;

        /// <summary>
        /// <see cref="IInput"/> that reads once and then returns from cache.
        /// Closes the input stream after first read.
        /// </summary>
        /// <param name="input"></param>
        public Sticky(IInput input)
        {
            this.cache =
                new Lazy<MemoryStream>(() =>
                    {
                        MemoryStream copy = new MemoryStream();
                        ReadAll._(
                            new TeeInput(input, new OutputTo(copy))
                        ).Invoke();
                        input.Stream().Dispose();
                        copy.Seek(0, SeekOrigin.Begin);
                        return copy;
                    }
                );
        }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return this.cache.Value;
        }
    }
}
