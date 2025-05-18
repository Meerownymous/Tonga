

using System.IO;

namespace Tonga.IO;

/// <summary>
/// Logged input.
/// </summary>
public sealed class LoggingOnReadConduit(IConduit origin, string source) : IConduit
{
    public Stream Stream() =>
        new LoggingOnReadStream(
            origin.Stream(),
            source
        );
}
