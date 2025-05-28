using System.IO;
using System.IO.Compression;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// An output that compresses.
    /// </summary>
    public sealed class GZipCompressing(IConduit output, CompressionLevel level = CompressionLevel.Optimal) : IConduit
    {
        /// <summary>
        /// A stream configured to decompressing.
        /// </summary>
        public Stream Stream() => new GZipStream(output.Stream(), level, true);
    }
}
