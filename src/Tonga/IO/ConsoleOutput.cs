

using System;
using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// Console output stream.
    /// </summary>
    public sealed class ConsoleOutput : IConduit
    {
        /// <summary>
        /// Console output stream.
        /// </summary>
        public ConsoleOutput()
        { }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return Console.OpenStandardOutput();
        }
    }
}
