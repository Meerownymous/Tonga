using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Union of two async enumerables.
/// </summary>
public class Union<T>(IAsyncEnumerable<T> a, IAsyncEnumerable<T> b, IEqualityComparer<T> comparison) :
    IAsyncEnumerable<T>
{
    /// <summary>
    /// Union of two async enumerables.
    /// </summary>
    public Union(IAsyncEnumerable<T> a, IAsyncEnumerable<T> b) : this(
        a, b, new Comparison((left, right) => left.Equals(right))
    )
    { }

    /// <summary>
    /// Union of two async enumerables.
    /// </summary>
    public Union(IAsyncEnumerable<T> a, IAsyncEnumerable<T> b, Func<T, T, bool> compare) : this(
        a, b, new Comparison(compare)
    )
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var union = new HashSet<T>(comparison);
        await foreach (var element in new Joined<T>(a, b).WithCancellation(cancellation))
        {
            if (union.Add(element))
                yield return element;
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
    /// Union of two async enumerables.
    /// </summary>
    public static IAsyncEnumerable<T> AsUnion<T>(this IAsyncEnumerable<T> a, IAsyncEnumerable<T> b) =>
        new Union<T>(a, b);

    /// <summary>
    /// Union of two async enumerables.
    /// </summary>
    public static IAsyncEnumerable<T> AsUnion<T>(
        this IAsyncEnumerable<T> a, IAsyncEnumerable<T> b, Func<T, T, bool> compare
    ) =>
        new Union<T>(a, b, compare);
}
