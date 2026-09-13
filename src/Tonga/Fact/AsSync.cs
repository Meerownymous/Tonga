using System.Threading;

namespace Tonga.Fact;

/// <summary>
/// An <see cref="IAsyncFact"/> as a plain <see cref="IFact"/>.
/// <para>
/// Checking blocks the calling thread until the source answers. On a thread with a
/// synchronization context that runs continuations on that same thread, this deadlocks.
/// </para>
/// </summary>
public sealed class AsSync(IAsyncFact origin, CancellationToken cancellation) : IFact
{
    /// <summary>
    /// An <see cref="IAsyncFact"/> as a plain <see cref="IFact"/>.
    /// </summary>
    public AsSync(IAsyncFact origin) : this(origin, CancellationToken.None)
    { }

    public bool IsTrue() => origin.IsTrue(cancellation).AsTask().GetAwaiter().GetResult();

    public bool IsFalse() => !this.IsTrue();
}

public static partial class AsFactSmarts
{
    /// <summary>
    /// An <see cref="IAsyncFact"/> as a plain <see cref="IFact"/>.
    /// Checking blocks the calling thread.
    /// </summary>
    public static IFact AsSync(this IAsyncFact origin) => new AsSync(origin);

    /// <summary>
    /// An <see cref="IAsyncFact"/> as a plain <see cref="IFact"/>.
    /// Checking blocks the calling thread.
    /// </summary>
    public static IFact AsSync(this IAsyncFact origin, CancellationToken cancellation) =>
        new AsSync(origin, cancellation);
}
