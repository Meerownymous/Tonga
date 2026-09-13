using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// An <see cref="IAsyncEnumerable{T}"/> as a plain <see cref="IEnumerable{T}"/>.
/// <para>
/// Every step blocks the calling thread until the source delivers. On a thread with a
/// synchronization context that runs continuations on that same thread, this deadlocks.
/// Reach for it at the edge of a codebase that cannot await, not inside one that can.
/// </para>
/// </summary>
public sealed class AsSync<T>(IAsyncEnumerable<T> source, CancellationToken cancellation) : IEnumerable<T>
{
    /// <summary>
    /// An <see cref="IAsyncEnumerable{T}"/> as a plain <see cref="IEnumerable{T}"/>.
    /// </summary>
    public AsSync(IAsyncEnumerable<T> source) : this(source, CancellationToken.None)
    { }

    public IEnumerator<T> GetEnumerator()
    {
        var enumerator = source.GetAsyncEnumerator(cancellation);
        try
        {
            while (enumerator.MoveNextAsync().AsTask().GetAwaiter().GetResult())
            {
                yield return enumerator.Current;
            }
        }
        finally
        {
            enumerator.DisposeAsync().AsTask().GetAwaiter().GetResult();
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// An <see cref="IAsyncEnumerable{T}"/> as a plain <see cref="IEnumerable{T}"/>.
    /// Every step blocks the calling thread.
    /// </summary>
    public static IEnumerable<T> AsSync<T>(this IAsyncEnumerable<T> source) => new AsSync<T>(source);

    /// <summary>
    /// An <see cref="IAsyncEnumerable{T}"/> as a plain <see cref="IEnumerable{T}"/>.
    /// Every step blocks the calling thread.
    /// </summary>
    public static IEnumerable<T> AsSync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellation) =>
        new AsSync<T>(source, cancellation);
}
