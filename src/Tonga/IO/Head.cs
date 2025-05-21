

using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// Input that only shows the first N bytes of the original input.
    /// </summary>
    public sealed class Head : IConduit
    {
        private readonly IConduit origin;
        private readonly int length;

        /// <summary>
        /// Input that only shows the first N bytes of the original input.
        /// </summary>
        /// <param name="origin">Input</param>
        /// <param name="length">Length</param>
        public Head(IConduit origin, int length)
        {
            this.origin = origin;
            this.length = length;
        }

        public Stream Stream()
        {
            return new StreamHead(this.origin.Stream(), this.length);
        }
    }
}
