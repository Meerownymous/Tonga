

using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// Logged output.
    /// </summary>
    public sealed class LoggingOutput : IOutput
    {
        private readonly IOutput origin;
        private readonly string destination;

        /// <summary>
        /// Logged output.
        /// </summary>
        /// <param name="output">Data output</param>
        /// <param name="destination">The name of destination data</param>
        public LoggingOutput(IOutput output, string destination)
        {
            this.origin = output;
            this.destination = destination;
        }

        public Stream Stream()
        {
            return
                new LoggingOutputStream(
                    this.origin.Stream(),
                    this.destination
                );
        }
    }
}
