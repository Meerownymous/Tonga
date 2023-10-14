

using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// Input that only shows the first N bytes of the original input.
    /// </summary>
    public sealed class HeadOf : IInput
    {
        private readonly IInput origin;
        private readonly int length;

        /// <summary>
        /// Input that only shows the first N bytes of the original input.
        /// </summary>
        /// <param name="origin">Input</param>
        /// <param name="length">Length</param>
        public HeadOf(IInput origin, int length)
        {
            this.origin = origin;
            this.length = length;
        }

        public Stream Stream()
        {
            return new HeadInputStream(this.origin.Stream(), this.length);
        }
    }
}
