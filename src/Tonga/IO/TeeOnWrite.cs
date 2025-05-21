

using System;
using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// A <see cref="IConduit"/> which will copy to another <see cref="IConduit"/> while writing.
    /// </summary>
    public sealed class TeeOnWrite(IConduit target, IConduit copy) : IConduit, IDisposable
    {
        /// <summary>
        /// A <see cref="IConduit"/> which will copy to another <see cref="IConduit"/> while writing.
        /// </summary>
        /// <param name="tgt">the original target</param>
        /// <param name="cpy">the copy target</param>
        public TeeOnWrite(IConduit tgt, StreamWriter cpy) : this(tgt, new AsConduit(cpy.BaseStream))
        { }

        /// <summary>
        /// A <see cref="IConduit"/> which will copy to a file <see cref="Uri"/> while writing.
        /// </summary>
        /// <param name="tgt">the original target</param>
        /// <param name="cpy">the copy target file <see cref="Uri"/></param>
        public TeeOnWrite(IConduit tgt, Uri cpy) : this(tgt, new AsConduit(cpy))
        { }

        /// <summary>
        /// A <see cref="IConduit"/> which will copy to a <see cref="Stream"/> while writing.
        /// </summary>
        /// <param name="tgt">the original target</param>
        /// <param name="cpy">the copy target file <see cref="Uri"/></param>
        public TeeOnWrite(IConduit tgt, Stream cpy) : this(tgt, new AsConduit(cpy))
        { }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream() =>
            new TeeOnWriteStream(target.Stream(), copy.Stream());

        /// <summary>
        /// Clean up
        /// </summary>
        public void Dispose()
        {
            (target as IDisposable)?.Dispose();
            (copy as IDisposable)?.Dispose();
        }

    }
}
