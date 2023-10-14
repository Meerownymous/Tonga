

using System;
using Tonga.Scalar;

namespace Tonga.Bytes
{
    /// <summary>
    /// Bytes out of other objects that are reloaded on every call
    /// </summary>
    public sealed class LiveBytes : IBytes
    {
        private readonly IScalar<IBytes> bytes;

        /// <summary>
        /// Reloads the bytes input on every call
        /// </summary>
        /// <param name="input">The input</param>
        public LiveBytes(Func<IInput> input) : this(() => new BytesOf(input()))
        { }

        /// <summary>
        /// Relaods the bytes on every call
        /// </summary>
        /// <param name="bytes"></param>
        public LiveBytes(Func<IBytes> bytes) : this(new Live<IBytes>(bytes))
        { }

        private LiveBytes(IScalar<IBytes> bytes)
        {
            this.bytes = bytes;
        }

        public byte[] AsBytes()
        {
            return this.bytes.Value().AsBytes();
        }
    }
}
