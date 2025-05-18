

using System;
using System.IO;
using Tonga.Func;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// <see cref="IConduit"/> that reads once and then returns from cache.
    /// </summary>
    public sealed class Sticky(IConduit origin) : IConduit
    {
        /// <summary>
        /// the cache
        /// </summary>
        private readonly Lazy<byte[]> cache =
            new(() =>
            {
                MemoryStream copy = new MemoryStream();
                ReadAll._(
                    new TeeOnReadConduit(origin, new AsConduit(copy))
                ).Invoke();
                origin.Stream().Dispose();
                return copy.ToArray();
            }
        );

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream() => new MemoryStream(this.cache.Value);
    }
}
