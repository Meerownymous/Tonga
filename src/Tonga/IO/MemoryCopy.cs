

using System;
using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// A <see cref="IConduit"/> which copies the input to memory and returns the copy.
    /// </summary>
    public sealed class MemoryCopy(IConduit origin) : IConduit
    {
        private readonly Lazy<MemoryStream> memory = new(() =>
            {
                var memStream = new MemoryStream();
                origin.Stream().CopyTo(memStream);
                memStream.Seek(0, SeekOrigin.Begin);
                return memStream;
            }
        );

        public Stream Stream() => memory.Value;
    }
}
