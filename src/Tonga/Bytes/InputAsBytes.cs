

using System.IO;
using Tonga.IO;
using Tonga.Scalar;

namespace Tonga.Bytes
{
    /// <summary>
    /// Input as bytes. Disposes input.
    /// </summary>
    public sealed class InputAsBytes : IBytes
    {
        private readonly IScalar<byte[]> bytes;

        /// <summary>
        /// Input as bytes.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="max">maximum buffer size</param>
        public InputAsBytes(IInput input, int max = 16 << 10)
        {
            this.bytes = new AsScalar<byte[]>(() =>
            {
                var baos = new MemoryStream();
                byte[] output;
                var source = input.Stream();
                var stream = new TeeInput(new InputOf(source), new OutputTo(baos)).Stream();
                byte[] readBuffer = new byte[max];
                while ((stream.Read(readBuffer, 0, readBuffer.Length)) > 0)
                { }
                stream.Flush();
                output = baos.ToArray();
                return output;
            });
        }

        /// <summary>
        /// Get the content as byte array. (Self-Disposing)
        /// </summary>
        /// <returns>content as byte array</returns>
        public byte[] Bytes()
        {
            return this.bytes.Value();
        }
    }
}
