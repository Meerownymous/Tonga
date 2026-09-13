using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Intersection of two async enumerables.
/// </summary>
public sealed class Intersection<T>(IAsyncEnumerable<T> a, IAsyncEnumerable<T> b, IEqualityComparer<T> comparison) :
    IAsyncEnumerable<T>
{
    /// <summary>
    /// Intersection of two async enumerables.
    /// </summary>
    public Intersection(IAsyncEnumerable<T> a, IAsyncEnumerable<T> b) : this(
        a, b, new Comparison((left, right) => left.Equals(right))
    )
    { }

    /// <summary>
    /// Intersection of two async enumerables.
    /// </summary>
    public Intersection(IAsyncEnumerable<T> a, IAsyncEnumerable<T> b, Func<T, T, bool> compare) : this(
        a, b, new Comparison(compare)
    )
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var set = new HashSet<T>(comparison);
        await foreach (var item in b.WithCancellation(cancellation))
        {
            set.Add(item);
        }
        await foreach (var item in a.WithCancellation(cancellation))
        {
            if (set.Remove(item)) // Ensures each item is yielded only once
            {
                yield return item;
            }
        }
    }

    private sealed class Comparison(Func<T, T, bool> comparison) : IEqualityComparer<T>
    {
        public bool Equals(T x, T y) => comparison(y, x);
        public int GetHashCode(T obj) => 0;
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Intersection of two async enumerables.
    /// </summary>
    public static IAsyncEnumerable<T> AsIntersection<T>(this IAsyncEnumerable<T> a, IAsyncEnumerable<T> b) =>
        new Intersection<T>(a, b);

    /// <summary>
    /// Intersection of two async enumerables.
    /// </summary>
    public static IAsyncEnumerable<T> AsIntersection<T>(
        this IAsyncEnumerable<T> a, IAsyncEnumerable<T> b, Func<T, T, bool> compare
    ) =>
        new Intersection<T>(a, b, compare);
}
