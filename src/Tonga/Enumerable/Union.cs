

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tonga.Enumerable;

/// <summary>
/// Union of two enumerables.
/// </summary>
public class Union<T>(IEnumerable<T> a, IEnumerable<T> b, IEqualityComparer<T> comparison) : IEnumerable<T>
{
    /// <summary>
    /// Union of two enumerables.
    /// </summary>
    public Union(IEnumerable<T> a, IEnumerable<T> b) : this(
        a, b, new Comparison((left,right) => left.Equals(right))
    )
    { }

    /// <summary>
    /// Union of two enumerables.
    /// </summary>
    public Union(IEnumerable<T> a, IEnumerable<T> b, Func<T, T, bool> compare) : this(
        a,
        b,
        new Comparison(compare)
    )
    { }

    public IEnumerator<T> GetEnumerator()
    {
        var union = new HashSet<T>(comparison);
        foreach(var element in new Joined<T>(a, b))
        {
            if(union.Add(element))
                yield return element;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();


    private sealed class Comparison(Func<T, T, bool> comparison) : IEqualityComparer<T>
    {
        public bool Equals(T x, T y) => comparison(y, x);
        public int GetHashCode(T obj) => 0;
    }
}

/// <summary>
/// Union objects in two enumerables.
/// </summary>
public static partial class EnumerableSmarts
{
    /// <summary>
    /// Union of two enumerables.
    /// </summary>
    public static IEnumerable<T> AsUnion<T>(this IEnumerable<T> a, IEnumerable<T> b, Func<T, T, bool> compare) =>
        new Union<T>(a, b, compare);

    /// <summary>
    /// Union of two enumerables.
    /// </summary>
    public static IEnumerable<T> AsUnion<T>(this IEnumerable<T> a, IEnumerable<T> b) =>
        new Union<T>(a, b);
}
