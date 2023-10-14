

using System.IO;
using System.IO.Compression;

namespace Tonga.IO
{
    /// <summary>
    /// A input that decompresses.
    /// </summary>
    public sealed class GZipInput : IInput
    {
        // The input.
        private readonly IInput origin;

        /// <summary>
        /// The input as a gzip stream.
        /// </summary>
        /// <param name="input">the input</param>
        public GZipInput(IInput input)
        {
            this.origin = input;
        }

        /// <summary>
        /// A stream which is decompressing.
        /// </summary>
        /// <returns></returns>
        public Stream Stream()
        {
            return new GZipStream(this.origin.Stream(), CompressionMode.Decompress);
        }
    }
}
