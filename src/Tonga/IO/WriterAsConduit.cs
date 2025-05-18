

using System;
using System.IO;
using System.Text;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// A <see cref="StreamWriter"/> as <see cref="IConduit"/>.
    /// </summary>
    public sealed class WriterAsConduit(StreamWriter writer, IScalar<Decoder> decoder) : IConduit, IDisposable
    {
        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IConduit"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        public WriterAsConduit(StreamWriter wtr) : this(wtr, Encoding.UTF8)
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IConduit"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        /// <param name="enc">encoding of the streamwriter</param>
        public WriterAsConduit(StreamWriter wtr, Encoding enc) : this(wtr,
            enc.GetDecoder)
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IConduit"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        /// <param name="fnc">function returning a decoder for the writer</param>
        public WriterAsConduit(StreamWriter wtr, Func<Decoder> fnc) : this(wtr, AsScalar._(fnc))
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
