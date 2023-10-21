

using System;
using System.IO;
using Tonga.Scalar;
using Tonga.Text;

namespace Tonga.Bytes
{
    /// <summary>
    /// Bytes from Hex String
    /// </summary>
    public sealed class HexBytes : IBytes
    {
        private readonly IScalar<byte[]> bytes;

        /// <summary>
        /// Bytes from Hex String
        /// </summary>
        /// <param name="origin">The string in Hex format</param>
        public HexBytes(string origin) : this(Text.AsText._(origin))
        { }
        /// <summary>
        /// Bytes from Hex String
        /// </summary>
        /// <param name="origin">The string in Hex format</param>
        public HexBytes(IText origin)
        {
            this.bytes = new AsScalar<byte[]>(() =>
            {
                var hex = origin.AsString();
                if ((hex.Length & 1) == 1)
                {
                    throw new IOException("Length of hexadecimal text is odd");
                }
                byte[] raw = new byte[hex.Length / 2];
                for (int i = 0; i < raw.Length; i++)
                {
                    raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                }
                return raw;
            });
        }

        public byte[] Bytes()
        {
            return this.bytes.Value();
        }
    }
}
