

using System;
using System.Text;
using Tonga.Scalar;

namespace Tonga.Bytes
{
    /// <summary>
    /// Encodes all origin bytes using the Base64 encoding scheme.
    /// </summary>
    public sealed class Base64Encoded : IBytes
    {
        private readonly IScalar<byte[]> bytes;

        /// <summary>
        /// Encoded origin bytes using the Base64 encoding scheme.
        /// </summary>
        /// <param name="bytes"></param>
        public Base64Encoded(IBytes bytes)
        {
            this.bytes =
                AsScalar._(() =>
                    Encoding.UTF8.GetBytes(
                        Convert.ToBase64String(
                            bytes.Bytes()
                        )
                    )
                );
        }

        /// <summary>
        /// The bytes encoded as Base64
        /// </summary>
        /// <returns></returns>
        public byte[] Bytes()
        {
            return this.bytes.Value();
        }
    }
}
