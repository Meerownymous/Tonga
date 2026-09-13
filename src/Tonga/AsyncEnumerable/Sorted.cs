using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// A <see cref="IAsyncEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
/// Sorting needs every item, so this reads the whole source before yielding the first item.
/// </summary>
public sealed class Sorted<T>(Comparer<T> cmp, IAsyncEnumerable<T> src) : IAsyncEnumerable<T>
{
    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> sorted by default.
    /// </summary>
    public Sorted(IAsyncEnumerable<T> src) : this(Comparer<T>.Default, src)
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var sorted = new List<T>();
        await foreach (var item in src.WithCancellation(cancellation))
        {
            sorted.Add(item);
        }
        sorted.Sort(cmp);
        foreach (var item in sorted)
        {
            yield return item;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> sorted by default.
    /// </summary>
    public static IAsyncEnumerable<T> AsSorted<T>(this IAsyncEnumerable<T> src) where T : IComparable<T> =>
        new Sorted<T>(src);

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    public static IAsyncEnumerable<T> AsSorted<T>(this IAsyncEnumerable<T> src, Comparer<T> cmp) =>
        new Sorted<T>(cmp, src);
}
