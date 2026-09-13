using System.Threading;

namespace Tonga.Scalar;

/// <summary>
/// An <see cref="IAsyncScalar{TValue}"/> as a plain <see cref="IScalar{Val}"/>.
/// <para>
/// <see cref="Value"/> blocks the calling thread until the source delivers. On a thread with a
/// synchronization context that runs continuations on that same thread, this deadlocks.
/// </para>
/// </summary>
public sealed class AsSync<T>(IAsyncScalar<T> origin, CancellationToken cancellation) : IScalar<T>
{
    /// <summary>
    /// An <see cref="IAsyncScalar{TValue}"/> as a plain <see cref="IScalar{Val}"/>.
    /// </summary>
    public AsSync(IAsyncScalar<T> origin) : this(origin, CancellationToken.None)
    { }

    public T Value() => origin.Value(cancellation).AsTask().GetAwaiter().GetResult();
}

public static partial class ScalarSmarts
{
    /// <summary>
    /// An <see cref="IAsyncScalar{TValue}"/> as a plain <see cref="IScalar{Val}"/>.
    /// Asking for the value blocks the calling thread.
    /// </summary>
    public static IScalar<T> AsSync<T>(this IAsyncScalar<T> origin) => new AsSync<T>(origin);

    /// <summary>
    /// An <see cref="IAsyncScalar{TValue}"/> as a plain <see cref="IScalar{Val}"/>.
    /// Asking for the value blocks the calling thread.
    /// </summary>
    public static IScalar<T> AsSync<T>(this IAsyncScalar<T> origin, CancellationToken cancellation) =>
        new AsSync<T>(origin, cancellation);
}
