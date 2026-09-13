using System.Collections.Generic;
using System.Threading;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Async enumeration of a single item.
/// </summary>
public sealed class Single<T>(T item) : IAsyncEnumerable<T>
{
    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        yield return item;
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Async enumeration of a single item.
    /// </summary>
    public static IAsyncEnumerable<T> AsAsyncSingle<T>(this T item) => new Single<T>(item);
}
