using System;
using System.IO;

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
                new TeeOnRead(origin, new AsConduit(copy))
                    .FullRead()
                    .Trigger();

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
