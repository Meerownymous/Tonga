

using System;
using System.IO;
using System.Text;

namespace Tonga.IO
{
    /// <summary>
    /// A <see cref="IOutput"/> which will copy to another <see cref="IOutput"/> while writing.
    /// </summary>
    public sealed class TeeOutput : IOutput, IDisposable
    {
        private readonly IOutput _target;
        private readonly IOutput _copy;

        /// <summary>
        /// A <see cref="IOutput"/> which will copy to another <see cref="IOutput"/> while writing.
        /// </summary>
        /// <param name="tgt">the original target</param>
        /// <param name="cpy">the copy target</param>
        public TeeOutput(IOutput tgt, StreamWriter cpy) : this(tgt, new OutputTo(cpy))
        { }

        /// <summary>
        /// A <see cref="IOutput"/> which will copy to another <see cref="StreamWriter"/> while writing.
        /// </summary>
        /// <param name="tgt">the original target</param>
        /// <param name="cpy">the copy target</param>
        /// <param name="enc">encoding for the copy target</param>
        public TeeOutput(IOutput tgt, StreamWriter cpy, Encoding enc) : this(tgt, new OutputTo(cpy, enc))
        { }

        /// <summary>
        /// A <see cref="IOutput"/> which will copy to a file <see cref="Uri"/> while writing.
        /// </summary>
        /// <param name="tgt">the original target</param>
        /// <param name="cpy">the copy target file <see cref="Uri"/></param>
        public TeeOutput(IOutput tgt, Uri cpy) : this(tgt, new OutputTo(cpy))
        { }

        /// <summary>
        /// A <see cref="IOutput"/> which will copy to a <see cref="Stream"/> while writing.
        /// </summary>
        /// <param name="tgt">the original target</param>
        /// <param name="cpy">the copy target file <see cref="Uri"/></param>        
        public TeeOutput(IOutput tgt, Stream cpy) : this(tgt, new OutputTo(cpy))
        { }

        /// <summary>
        /// A <see cref="IOutput"/> which will copy to another <see cref="IOutput"/> while writing.
        /// </summary>
        /// <param name="tgt">the original target</param>
        /// <param name="cpy">the copy target file <see cref="Uri"/></param>        
        public TeeOutput(IOutput tgt, IOutput cpy)
        {
            this._target = tgt;
            this._copy = cpy;
        }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return new TeeOutputStream(
                this._target.Stream(), this._copy.Stream()
            );
        }

        /// <summary>
        /// Clean up
        /// </summary>
        public void Dispose()
        {
            (_target as IDisposable)?.Dispose();
            (_copy as IDisposable)?.Dispose();
        }

    }
}
