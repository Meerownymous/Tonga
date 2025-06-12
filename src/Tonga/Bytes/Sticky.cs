

using System;
using Tonga.Scalar;

namespace Tonga.Bytes
{
    /// <summary>
    /// Bytes out of other objects that are reloaded on every call
    /// </summary>
    public sealed class Sticky : IBytes
    {
        private readonly Lazy<byte[]> bytes;

        /// <summary>
        /// Reloads the bytes input on every call
        /// </summary>
        /// <param name="input">The input</param>
        public Sticky(Func<IConduit> input) : this(() => new AsBytes(input()))
        { }

        /// <summary>
        /// Relaods the bytes on every call
        /// </summary>
        /// <param name="bytes"></param>
        public Sticky(IScalar<IBytes> bytes) : this(bytes.Value)
        { }

        public Sticky(Func<IBytes> bytes)
        {
            this.bytes = new Lazy<byte[]>(() => bytes().Raw());
        }

        public byte[] Raw()
        {
            return this.bytes.Value;
        }
    }
}
