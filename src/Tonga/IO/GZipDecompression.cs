

using System.IO;
using System.IO.Compression;

namespace Tonga.IO;

/// <summary>
/// A conduit that decompresses.
/// </summary>
public sealed class GZipDecompression(IConduit origin) : IConduit
{
    /// <summary>
    /// A stream which is decompressing.
    /// </summary>
    /// <returns></returns>
    public Stream Stream() => new GZipStream(origin.Stream(), CompressionMode.Decompress);
}
