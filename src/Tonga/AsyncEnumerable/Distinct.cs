using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Multiple async enumerables merged together, so that every entry is unique.
/// </summary>
public sealed class Distinct<T>(IAsyncEnumerable<IAsyncEnumerable<T>> enumerables, IEqualityComparer<T> comparison) :
    IAsyncEnumerable<T>
{
    /// <summary>
    /// The distinct elements of one or multiple async enumerables, compared by the given function.
    /// </summary>
    public Distinct(IAsyncEnumerable<IAsyncEnumerable<T>> enumerables, Func<T, T, bool> comparison) : this(
        enumerables, new EqualityComparison<T>(comparison)
    )
    { }

    /// <summary>
    /// The distinct elements of one or multiple async enumerables.
    /// </summary>
    public Distinct(params IAsyncEnumerable<T>[] enumerables) : this(enumerables.AsAsyncEnumerable())
    { }

    /// <summary>
    /// The distinct elements among the given items.
    /// </summary>
    public Distinct(params T[] items) : this(items.AsAsyncEnumerable())
    { }

    /// <summary>
    /// The distinct elements of one or multiple async enumerables.
    /// </summary>
    public Distinct(IAsyncEnumerable<IAsyncEnumerable<T>> enumerables) : this(
        enumerables,
        EqualityComparer<T>.Default
    )
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var set = new HashSet<T>(comparison);
        await foreach (var item in new Joined<T>(enumerables).WithCancellation(cancellation))
        {
            if (set.Add(item))
                yield return item;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// The distinct elements of an async enumerable.
    /// </summary>
    public static IAsyncEnumerable<T> AsDistinct<T>(this IAsyncEnumerable<T> enumerable) =>
        new Distinct<T>(enumerable);

    /// <summary>
    /// The distinct elements of multiple async enumerables.
    /// </summary>
    public static IAsyncEnumerable<T> AsDistinct<T>(this IAsyncEnumerable<T>[] enumerables) =>
        new Distinct<T>(enumerables);

    /// <summary>
    /// The distinct elements of multiple async enumerables.
    /// </summary>
    public static IAsyncEnumerable<T> AsDistinct<T>(this IAsyncEnumerable<T>[] enumerables, Func<T, T, bool> comparison) =>
        new Distinct<T>(enumerables.AsAsyncEnumerable(), comparison);
}
