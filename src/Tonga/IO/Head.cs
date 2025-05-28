

using System.IO;

namespace Tonga.IO
{
    /// <summary>
    /// Input that only shows the first N bytes of the original input.
    /// </summary>
    public sealed class Head(IConduit origin, int length) : IConduit
    {
        public Stream Stream() => new StreamHead(origin.Stream(), length);
    }
}
