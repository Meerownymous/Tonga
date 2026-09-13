using System;
using System.Collections.Generic;
using System.Threading;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// A given async enumerator as an async enumerable.
/// </summary>
public sealed class AsyncEnumeratorAsAsyncEnumerable<T> : IAsyncEnumerable<T>
{
    private readonly Func<CancellationToken, IAsyncEnumerator<T>> enumerator;
    private readonly bool owned;

    /// <summary>
    /// A given async enumerator as an async enumerable.
    /// The enumerator is created here, so it is disposed here.
    /// </summary>
    public AsyncEnumeratorAsAsyncEnumerable(Func<CancellationToken, IAsyncEnumerator<T>> enumerator) : this(
        enumerator, true
    )
    { }

    /// <summary>
    /// A given async enumerator as an async enumerable.
    /// The enumerator belongs to the caller, so it is not disposed here.
    /// </summary>
    public AsyncEnumeratorAsAsyncEnumerable(IAsyncEnumerator<T> enumerator) : this(_ => enumerator, false)
    { }

    private AsyncEnumeratorAsAsyncEnumerable(Func<CancellationToken, IAsyncEnumerator<T>> enumerator, bool owned)
    {
        this.enumerator = enumerator;
        this.owned = owned;
    }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var enm = this.enumerator(cancellation);
        try
        {
            while (await enm.MoveNextAsync())
            {
                yield return enm.Current;
            }
        }
        finally
        {
            if (this.owned)
                await enm.DisposeAsync();
        }
    }
}
