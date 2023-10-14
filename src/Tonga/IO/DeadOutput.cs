

using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// Output to dev/null.
    /// </summary>
    public sealed class DeadOutput : IOutput
    {
        /// <summary>
        /// Output to dev/null.
        /// </summary>
        public DeadOutput()
        { }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return new DeadStream();
        }
    }
}
