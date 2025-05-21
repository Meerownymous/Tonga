

using System;
using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// Console input stream.
    /// </summary>
    public sealed class ConsoleInput : IConduit
    {
        private readonly Lazy<Stream> consoleStream = new(Console.OpenStandardInput);
        /// <summary>
        /// Console input stream.
        /// </summary>
        public ConsoleInput()
        { }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream() => consoleStream.Value;
    }
}
