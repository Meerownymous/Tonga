

using System;
using System.IO;
using System.IO.Compression;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// An output that compresses.
    /// </summary>
    public sealed class GZipCompression : IConduit
    {
        // The input.
        private readonly StickyIf<GZipStream> output;

        // The buffer size.
        private readonly CompressionLevel level;

        /// <summary>
        /// The output as a gzip compressed stream.
        /// </summary>
        /// <param name="output">the output to compress</param>
        /// <param name="level">the compression level</param>
        public GZipCompression(IConduit output, CompressionLevel level = CompressionLevel.Optimal)
        {
            this.output =
                StickyIf._(
                    stream => stream.CanWrite,
                    AsScalar._(() => new GZipStream(output.Stream(), this.level, true))
                );
            this.level = level;
        }

        /// <summary>
        /// A stream configured to decompressing.
        /// </summary>
        /// <returns></returns>
        public Stream Stream()
        {
            return this.output.Value();
        }
    }
}
