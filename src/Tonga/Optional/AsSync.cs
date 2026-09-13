using System;
using System.Threading;

namespace Tonga.Optional;

/// <summary>
/// An <see cref="IAsyncOptional{TValue}"/> as a plain <see cref="IOptional{TValue}"/>.
/// <para>
/// Every question blocks the calling thread until the source answers. On a thread with a
/// synchronization context that runs continuations on that same thread, this deadlocks.
/// </para>
/// </summary>
public sealed class AsSync<TValue>(IAsyncOptional<TValue> origin, CancellationToken cancellation) :
    IOptional<TValue>
{
    /// <summary>
    /// An <see cref="IAsyncOptional{TValue}"/> as a plain <see cref="IOptional{TValue}"/>.
    /// </summary>
    public AsSync(IAsyncOptional<TValue> origin) : this(origin, CancellationToken.None)
    { }

    public bool Has() => origin.Has(cancellation).AsTask().GetAwaiter().GetResult();

    public IOptional<TValue> IfHas(Action<TValue> then)
    {
        origin
            .IfHas((item, _) =>
            {
                then(item);
                return default;
            })
            .Has(cancellation)
            .AsTask()
            .GetAwaiter()
            .GetResult();
        return this;
    }

    public IOptional<TValue> IfNot(Action then)
    {
        origin
            .IfNot(_ =>
            {
                then();
                return default;
            })
            .Has(cancellation)
            .AsTask()
            .GetAwaiter()
            .GetResult();
        return this;
    }

    public TValue Value() => origin.Value(cancellation).AsTask().GetAwaiter().GetResult();
}

public static partial class AsyncOptionalSmarts
{
    /// <summary>
    /// An <see cref="IAsyncOptional{TValue}"/> as a plain <see cref="IOptional{TValue}"/>.
    /// Every question blocks the calling thread.
    /// </summary>
    public static IOptional<TValue> AsSync<TValue>(this IAsyncOptional<TValue> origin) =>
        new AsSync<TValue>(origin);

    /// <summary>
    /// An <see cref="IAsyncOptional{TValue}"/> as a plain <see cref="IOptional{TValue}"/>.
    /// Every question blocks the calling thread.
    /// </summary>
    public static IOptional<TValue> AsSync<TValue>(
        this IAsyncOptional<TValue> origin, CancellationToken cancellation
    ) =>
        new AsSync<TValue>(origin, cancellation);
}
