

using System.IO;
using Tonga.Bytes;

namespace Tonga.IO
{
    /// <summary>
    /// Input with no data.
    /// </summary>
    public sealed class DeadInput : IInput
    {
        /// <summary>
        /// Input with no data.
        /// </summary>
        public DeadInput()
        { }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return new BytesAsInput(new EmptyBytes()).Stream();
        }
    }
}
