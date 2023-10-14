

using System;
using System.Text;
using Tonga.Scalar;

namespace Tonga.Bytes
{
    /// <summary>
    /// Encodes all origin bytes using the Base64 encoding scheme.
    /// </summary>
    public sealed class BytesBase64 : IBytes
    {
        private readonly IScalar<byte[]> bytes;

        /// <summary>
        /// Encoded origin bytes using the Base64 encoding scheme.
        /// </summary>
        /// <param name="bytes"></param>
        public BytesBase64(IBytes bytes)
        {
            this.bytes = new ScalarOf<byte[]>(() =>
                Encoding.UTF8.GetBytes(
                    Convert.ToBase64String(
                        bytes.AsBytes()
                    )
                ));
        }

        /// <summary>
        /// The bytes encoded as Base64
        /// </summary>
        /// <returns></returns>
        public byte[] AsBytes()
        {
            return this.bytes.Value();
        }
    }
}
