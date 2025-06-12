

using System;
using System.IO;

namespace Tonga.IO;

/// <summary>
/// Conduit which returns an alternate value if it fails.
/// </summary>
public sealed class BackFalling(IConduit origin, Func<Exception, IConduit> fbk) : IConduit, IDisposable
{
    /// <summary>
    /// Conduit which returns an alternate value if it fails.
    /// </summary>
    public BackFalling(IConduit origin) : this(origin, new DeadConduit())
    { }

    /// <summary>
    /// Conduit which returns an alternate value if it fails.
    /// </summary>
    public BackFalling(IConduit origin, IConduit alt) : this(origin, _ => alt)
    { }

    /// <summary>
    /// Get the stream.
    /// </summary>
    /// <returns>the stream</returns>
    public Stream Stream()
    {
        Stream stream;
        try
        {
            stream = origin.Stream();
        }
        catch (Exception ex)
        {
            stream = fbk.Invoke(ex).Stream();
        }
        return stream;
    }

    /// <summary>
    /// Clean up.
    /// </summary>
    public void Dispose() => (origin as IDisposable)?.Dispose();
}

public static partial class IOSmarts
{
    /// <summary>
    /// Conduit which returns an alternate value if it fails.
    /// </summary>
    public static IConduit AsBackFalling(this IConduit origin) => new BackFalling(origin);

    /// <summary>
    /// Conduit which returns an alternate value if it fails.
    /// </summary>
    public static IConduit AsBackFalling(this IConduit origin, IConduit alt) => new BackFalling(origin, alt);

    /// <summary>
    /// Conduit which returns an alternate value from the given <see cref="Func{IOException, IInput}"/>if it fails.
    /// </summary>
    public static IConduit AsBackFalling(this IConduit origin, Func<Exception, IConduit> alt) =>
        new BackFalling(origin, alt);
}
