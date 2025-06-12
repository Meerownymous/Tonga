

using System;
using System.IO;

namespace Tonga.IO;

/// <summary>
/// Input copied to and then returning from memory.
/// </summary>
public sealed class MemoryCopy(Func<Stream> origin) : IConduit
{
    /// <summary>
    /// Input copied to and then returning from memory.
    /// </summary>
    public MemoryCopy(Stream origin) : this(() => origin)
    { }

    /// <summary>
    /// Input copied to and then returning from memory.
    /// </summary>
    public MemoryCopy(IConduit origin) : this(origin.Stream)
    { }

    private readonly Lazy<MemoryStream> memory = new(() =>
        {
            var memStream = new MemoryStream();
            origin().CopyTo(memStream);
            memStream.Seek(0, SeekOrigin.Begin);
            return memStream;
        }
    );

    public Stream Stream() => memory.Value;
}
