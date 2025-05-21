

using System.IO;
using Tonga.IO;
using Tonga.Scalar;

namespace Tonga.Bytes
{
    /// <summary>
    /// Input as bytes. Disposes input.
    /// </summary>
    public sealed class ConduitAsBytes : IBytes
    {
        private readonly IScalar<byte[]> bytes;

        /// <summary>
        /// Input as bytes.
        /// </summary>
        public ConduitAsBytes(IConduit src, int max = 16 << 10)
        {
            this.bytes = new AsScalar<byte[]>(() =>
            {
                var baos = new MemoryStream();

                using var source = src.Stream();
                using var stream = new TeeOnRead(new AsConduit(source), new AsConduit(baos)).Stream();
                byte[] readBuffer = new byte[max];
                while (stream.Read(readBuffer, 0, readBuffer.Length) > 0)
                { }
                var output = baos.ToArray();
                return output;
            });
        }

        /// <summary>
        /// Get the content as byte array. (Self-Disposing)
        /// </summary>
        /// <returns>content as byte array</returns>
        public byte[] Bytes()=> this.bytes.Value();
    }
}
