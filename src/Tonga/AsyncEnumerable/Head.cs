using System;
using System.Collections.Generic;
using System.Threading;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// A <see cref="IAsyncEnumerable{T}"/> limited to an item maximum.
/// Reads no more items than the limit.
/// </summary>
public sealed class Head<T>(IAsyncEnumerable<T> source, Func<int> limit) : IAsyncEnumerable<T>
{
    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> limited to one item.
    /// </summary>
    public Head(IAsyncEnumerable<T> enumerable) : this(enumerable, 1)
    { }

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> limited to an item maximum.
    /// </summary>
    public Head(IAsyncEnumerable<T> enumerable, int limit) : this(enumerable, () => limit)
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var max = limit();
        var taken = 0;
        var enumerator = source.GetAsyncEnumerator(cancellation);
        try
        {
            while (taken < max && await enumerator.MoveNextAsync())
            {
                taken++;
                yield return enumerator.Current;
            }
        }
        finally
        {
            await enumerator.DisposeAsync();
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> limited to one item.
    /// </summary>
    public static IAsyncEnumerable<T> AsHead<T>(this IAsyncEnumerable<T> enumerable) =>
        new Head<T>(enumerable);

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> limited to an item maximum.
    /// </summary>
    public static IAsyncEnumerable<T> AsHead<T>(this IAsyncEnumerable<T> enumerable, int limit) =>
        new Head<T>(enumerable, limit);

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> limited to an item maximum.
    /// </summary>
    public static IAsyncEnumerable<T> AsHead<T>(this IAsyncEnumerable<T> enumerable, Func<int> limit) =>
        new Head<T>(enumerable, limit);
}
