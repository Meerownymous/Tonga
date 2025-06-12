

using System;
using System.IO;

namespace Tonga.IO;

/// <summary>
/// Logged input.
/// </summary>
public sealed class LoggingOnReadConduit(IConduit origin, string source) : IConduit, IDisposable
{
    private readonly Lazy<Stream> stream = new(()=>
        new LoggingOnReadStream(
            origin.Stream(),
            source
        )
    );

    public Stream Stream() => this.stream.Value;

    public void Dispose() => this.stream.Value.Dispose();
}
