

using System;
using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// Console error output stream.
    /// </summary>
    public sealed class ConsoleErrorOutput : IConduit
    {
        /// <summary>
        /// Console error output stream.
        /// </summary>
        public ConsoleErrorOutput()
        { }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream() => Console.OpenStandardError();
    }
}
