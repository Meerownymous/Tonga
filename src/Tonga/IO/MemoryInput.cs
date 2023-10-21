

using System.IO;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// A <see cref="IInput"/> which saves the input as memorystream
    /// </summary>
    public sealed class MemoryInput : IInput
    {
        private readonly IScalar<MemoryStream> memory;

        /// <summary>
        /// A <see cref="IInput"/> which saves the input as memorystream
        /// </summary>
        /// <param name="input"></param>
        public MemoryInput(IInput input)
        {
            this.memory =
                new AsScalar<MemoryStream>(() =>
                {
                    var memStream = new MemoryStream();
                    input.Stream().CopyTo(memStream);
                    memStream.Seek(0, SeekOrigin.Begin);
                    return memStream;
                });
        }

        public Stream Stream()
        {
            return this.memory.Value();
        }
    }
}
