

using System;
using System.IO;
using System.Text;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
    /// </summary>
    public sealed class WriterAsOutput : IOutput, IDisposable
    {
        /// <summary>
        /// the writer
        /// </summary>
        private readonly StreamWriter _writer;

        /// <summary>
        /// encoding of the writer
        /// </summary>
        private readonly IScalar<Decoder> _decoder;

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        public WriterAsOutput(StreamWriter wtr) : this(wtr, Encoding.UTF8)
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        /// <param name="enc">encoding of the streamwriter</param>
        public WriterAsOutput(StreamWriter wtr, Encoding enc) : this(wtr,
            () => enc.GetDecoder())
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        /// <param name="fnc">function returning a decoder for the writer</param>
        public WriterAsOutput(StreamWriter wtr, System.Func<Decoder> fnc) : this(wtr, AsScalar._(fnc))
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        /// <param name="ddr">decoder for the writer</param>
        public WriterAsOutput(StreamWriter wtr, IScalar<Decoder> ddr)
        {
            this._writer = wtr;
            this._decoder = ddr;
        }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns></returns>
        public Stream Stream()
        {
            return new WriterAsOutputStream(this._writer, this._decoder);
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)this._writer).Dispose();
        }

    }
}
