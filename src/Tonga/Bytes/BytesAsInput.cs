

using System;
using System.IO;

namespace Tonga.Bytes
{
    /// <summary>
    /// Bytes as input.
    /// </summary>
    public sealed class BytesAsInput : IInput
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly IBytes source;

        /// <summary>
        /// Bytes as input.
        /// </summary>
        /// <param name="text">a text</param>
        public BytesAsInput(IText text) : this(new BytesOf(text))
        { }

        /// <summary>
        /// Bytes as input.
        /// </summary>
        /// <param name="text">a string</param>
        public BytesAsInput(String text) : this(new BytesOf(text))
        { }

        /// <summary>
        /// Bytes as input.
        /// </summary>
        /// <param name="bytes">byte array</param>
        public BytesAsInput(byte[] bytes) : this(new BytesOf(bytes))
        { }

        /// <summary>
        /// Bytes as input.
        /// </summary>
        /// <param name="bytes">bytes</param>
        public BytesAsInput(IBytes bytes)
        {
            this.source = bytes;
        }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return new MemoryStream(this.source.AsBytes());
        }
    }
}
