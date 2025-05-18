

using System;
using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// Console input stream.
    /// </summary>
    public sealed class ConsoleInput : IConduit
    {
        /// <summary>
        /// Console input stream.
        /// </summary>
        public ConsoleInput()
        { }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return Console.OpenStandardInput();
        }
    }
}
