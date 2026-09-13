using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Items are replaced if they match a condition.
/// </summary>
public sealed class Replaced<T>(IAsyncEnumerable<T> origin, Func<T, bool> condition, T replacement) :
    IAsyncEnumerable<T>
{
    /// <summary>
    /// Item at a given index is replaced.
    /// </summary>
    public Replaced(IAsyncEnumerable<T> origin, int index, T replacement) : this(
        origin.AsMapped((item, itemIndex) => itemIndex == index ? replacement : item),
        _ => false,
        replacement
    )
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        await foreach (var item in origin.WithCancellation(cancellation))
        {
            yield return condition(item) ? replacement : item;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Items are replaced if they match a condition.
    /// </summary>
    public static IAsyncEnumerable<T> AsReplaced<T>(
        this IAsyncEnumerable<T> origin, Func<T, bool> condition, T replacement
    ) =>
        new Replaced<T>(origin, condition, replacement);

    /// <summary>
    /// Item at a given index is replaced.
    /// </summary>
    public static IAsyncEnumerable<T> AsReplaced<T>(this IAsyncEnumerable<T> origin, int index, T replacement) =>
        new Replaced<T>(origin, index, replacement);
}
