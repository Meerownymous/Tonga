

using System;
using System.Text;
using Tonga.Scalar;

namespace Tonga.Bytes
{
    /// <summary>
    /// Origin bytes decoded using the Base64 encoding scheme.
    /// </summary>
    public sealed class Base64Bytes : IBytes
    {
        private readonly IScalar<byte[]> bytes;

        /// <summary>
        /// Origin bytes decoded using the Base64 encoding scheme.
        /// </summary>
        /// <param name="bytes">origin bytes</param>
        public Base64Bytes(IBytes bytes)
        {
            this.bytes = new AsScalar<byte[]>(() =>
            {
                var byts = bytes.Bytes();
                string base64String = Encoding.UTF8.GetString(byts, 0, byts.Length);
                return Convert.FromBase64String(base64String);
            });
        }

        /// <summary>
        /// The 
        /// </summary>
        /// <returns></returns>
        public byte[] Bytes()
        {
            return this.bytes.Value();
        }
    }
}
