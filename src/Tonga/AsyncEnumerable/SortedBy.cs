using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// A <see cref="IAsyncEnumerable{T}"/> sorted by a key extracted from each item.
/// </summary>
public sealed class SortedBy<T, TKey>(
    Func<T, TKey> subjectExtraction,
    Comparer<TKey> cmp,
    IAsyncEnumerable<T> src
) : IAsyncEnumerable<T> where TKey : IComparable<TKey>
{
    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> sorted by a key, compared by default.
    /// </summary>
    public SortedBy(Func<T, TKey> swap, IAsyncEnumerable<T> src) : this(swap, Comparer<TKey>.Default, src)
    { }

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> sorted by a key.
    /// </summary>
    public SortedBy(Func<T, TKey> swap, Comparison<TKey> compare, IAsyncEnumerable<T> src) : this(
        swap, Comparer<TKey>.Create(compare), src
    )
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var list = new List<T>();
        await foreach (var item in src.WithCancellation(cancellation))
        {
            list.Add(item);
        }
        list.Sort((a, b) => cmp.Compare(subjectExtraction(a), subjectExtraction(b)));
        foreach (var item in list)
        {
            yield return item;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> sorted by a key, compared by default.
    /// </summary>
    public static IAsyncEnumerable<T> AsSortedBy<T, TKey>(this IAsyncEnumerable<T> src, Func<T, TKey> swap)
        where TKey : IComparable<TKey> =>
        new SortedBy<T, TKey>(swap, src);

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> sorted by a key.
    /// </summary>
    public static IAsyncEnumerable<T> AsSortedBy<T, TKey>(
        this IAsyncEnumerable<T> src, Func<T, TKey> swap, Comparer<TKey> cmp
    )
        where TKey : IComparable<TKey> =>
        new SortedBy<T, TKey>(swap, cmp, src);
}
