

using System.IO;
using Tonga.Bytes;

namespace Tonga.IO
{
    /// <summary>
    /// Input with no data.
    /// </summary>
    public sealed class DeadConduit : IConduit
    {
        /// <summary>
        /// Input with no data.
        /// </summary>
        public DeadConduit()
        { }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return new BytesAsConduit(new EmptyBytes()).Stream();
        }
    }
}
