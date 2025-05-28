

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable;

/// <summary>
/// Items are replaced if they match a condition.
/// </summary>
/// <typeparam name="T">type of items in enumerable</typeparam>
public sealed class Replaced<T>(IEnumerable<T> origin, Func<T, bool> condition, T replacement) : IEnumerable<T>
{
    private readonly IEnumerable<T> result =
        new AsEnumerable<T>(() => Produced(origin, condition, replacement));

    /// <summary>
    /// Items are replaced if they match a condition.
    /// </summary>
    /// <param name="origin">enumerable</param>
    /// <param name="index">index at which to replace the item</param>
    /// <param name="replacement">item to insert instead</param>
    public Replaced(IEnumerable<T> origin, int index, T replacement) : this(
        origin.AsMapped((item, itemIndex) => itemIndex == index ? replacement : item),
        _ => false,
        replacement
    )
    {
    }

    public IEnumerator<T> GetEnumerator() => this.result.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private static IEnumerator<T> Produced(IEnumerable<T> origin, Func<T, bool> condition, T replacement)
    {
        var e = origin.GetEnumerator();
        while (e.MoveNext())
        {
            if (condition.Invoke(e.Current))
            {
                yield return replacement;
            }
            else
            {
                yield return e.Current;
            }
        }
    }
}

/// <summary>
/// Items are replaced if they match a condition.
/// </summary>
public static partial class EnumerableSmarts
{
    /// <summary>
    /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
    /// </summary>
    public static IEnumerable<T> AsReplaced<T>(this IEnumerable<T> origin, Func<T, bool> condition, T replacement) =>
        new Replaced<T>(origin, condition, replacement);

    /// <summary>
    /// Item at a given index is replaced.
    /// </summary>
    public static IEnumerable<T> AsReplaced<T>(this IEnumerable<T> origin, int index, T replacement) =>
        new Replaced<T>(origin, index, replacement);
}
