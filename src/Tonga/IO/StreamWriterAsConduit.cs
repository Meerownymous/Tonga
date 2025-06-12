

using System;
using System.IO;
using System.Text;

namespace Tonga.IO
{
    /// <summary>
    /// A <see cref="StreamWriter"/> as <see cref="IConduit"/>.
    /// </summary>
    public sealed class StreamWriterAsConduit(StreamWriter writer, Func<Decoder> decoder) : IConduit, IDisposable
    {
        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IConduit"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        public StreamWriterAsConduit(StreamWriter wtr) : this(wtr, Encoding.UTF8)
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IConduit"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        /// <param name="enc">encoding of the streamwriter</param>
        public StreamWriterAsConduit(StreamWriter wtr, Encoding enc) : this(wtr,
            enc.GetDecoder
        )
        { }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns></returns>
        public Stream Stream() =>
            new WriterAsOutputStream(writer, decoder);

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Dispose() => ((IDisposable)writer).Dispose();
    }
}
