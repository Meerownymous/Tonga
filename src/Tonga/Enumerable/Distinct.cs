using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable;

/// <summary>
/// Multiple enumerables merged together, so that every entry is unique.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class Distinct<T>(IEnumerable<IEnumerable<T>> enumerables, Func<T, T, bool> comparison) : IEnumerable<T>
{
    private readonly IEnumerable<T> result =
        new Func<IEnumerator<T>>(() =>
            Produced(
                enumerables.AsJoined(),
                new Comparison(comparison)
            )
        ).AsEnumerable();

    /// <summary>
    /// The distinct elements of one or multiple Enumerables.
    /// </summary>
    /// <param name="enumerables">enumerables to get distinct elements from</param>
    public Distinct(params IEnumerable<T>[] enumerables) : this(
        enumerables.AsEnumerable()
    )
    { }

    /// <summary>
    /// The distinct elements of one or multiple Enumerables.
    /// </summary>
    /// <param name="enumerables">enumerables to get distinct elements from</param>
    public Distinct(IEnumerable<IEnumerable<T>> enumerables) : this(
        enumerables,
        (v1, v2) => v1.Equals(v2)
    )
    { }

    public IEnumerator<T> GetEnumerator() => result.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private static IEnumerator<T> Produced(IEnumerable<T> source, Comparison comparison)
    {
        var set = new HashSet<T>(comparison);
        foreach (var item in source)
        {
            if (set.Add(item))
                yield return item;
        }
    }

    private sealed class Comparison : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> comparison;

        public Comparison(Func<T, T, bool> comparison)
        {
            this.comparison = comparison;
        }

        public bool Equals(T x, T y)
        {
            return this.comparison.Invoke(x, y);
        }

        public int GetHashCode(T obj)
        {
            return 0;
        }
    }
}

/// <summary>
/// Multiple enumerables merged together, so that every entry is unique.
/// </summary>
public static partial class EnumerableSmarts
{
    /// <summary>
    /// The distinct elements of one or multiple Enumerables.
    /// </summary>
    /// <param name="enumerables">enumerables to get distinct elements from</param>
    public static IEnumerable<T> AsDistinct<T>(this IEnumerable<T>[] enumerables) =>
        new Distinct<T>(enumerables);

    /// <summary>
    /// The distinct elements of one or multiple Enumerables.
    /// </summary>
    /// <param name="enumerables">enumerables to get distinct elements from</param>
    public static IEnumerable<T> AsDistinct<T>(this IEnumerable<IEnumerable<T>> enumerables) =>
        new Distinct<T>(enumerables);

    /// <summary>
    /// The distinct elements of one or multiple Enumerables.
    /// </summary>
    public static IEnumerable<T> AsDistinct<T>(this IEnumerable<IEnumerable<T>> enumerables, Func<T, T, bool> comparison) =>
        new Distinct<T>(enumerables, comparison);
}
