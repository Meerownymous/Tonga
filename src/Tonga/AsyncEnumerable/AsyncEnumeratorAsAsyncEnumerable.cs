using System;
using System.Collections.Generic;
using System.Threading;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// A given async enumerator as an async enumerable.
/// </summary>
public sealed class AsyncEnumeratorAsAsyncEnumerable<T>(
    Func<CancellationToken, IAsyncEnumerator<T>> enumerator
) : IAsyncEnumerable<T>
{
    /// <summary>
    /// A given async enumerator as an async enumerable.
    /// </summary>
    public AsyncEnumeratorAsAsyncEnumerable(IAsyncEnumerator<T> enumerator) : this(_ => enumerator)
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var enm = enumerator(cancellation);
        while (await enm.MoveNextAsync())
        {
            yield return enm.Current;
        }
    }
}
