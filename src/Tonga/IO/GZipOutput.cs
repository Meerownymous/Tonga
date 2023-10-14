

using System.IO;
using System.IO.Compression;

namespace Tonga.IO
{
    /// <summary>
    /// A output that compresses.
    /// </summary>
    public sealed class GZipOutput : IOutput
    {
        // The input.
        private readonly IOutput _output;

        // The buffer size.
        private readonly CompressionLevel _level;

        /// <summary>
        /// The output as a gzip output. It compresses with level 'optimal'.
        /// </summary>
        /// <param name="output">the input</param>
        public GZipOutput(IOutput output) : this(output, CompressionLevel.Optimal)
        { }

        /// <summary>
        /// The output as a gzip compressed stream.
        /// </summary>
        /// <param name="output">the output to compress</param>
        /// <param name="level">the compression level</param>
        public GZipOutput(IOutput output, CompressionLevel level)
        {
            this._output = output;
            this._level = level;
        }

        /// <summary>
        /// A stream configured to decompressing.
        /// </summary>
        /// <returns></returns>
        public Stream Stream()
        {
            return new GZipStream(this._output.Stream(), this._level, true);
        }
    }
}
