

using System;
using System.Collections.Generic;
using System.IO;
using Tonga.Scalar;

namespace Tonga.Enumerable;

/// <summary>
/// Element before or after another element in a <see cref="IEnumerable{T}"/>.
/// </summary>
/// <typeparam name="T">type of element</typeparam>
public sealed class Sibling<T>(
    T item,
    IEnumerable<T> source,
    int relativeposition,
    Func<IEnumerable<T>, T> fallback
) : ScalarEnvelope<T>(
    () =>
    {
        var trace = new Queue<T>();
        var itemFound = false;
        var siblingFound = false;
        var stickySource = source.AsSticky();
        var enumerator = stickySource.GetEnumerator();
        T result = default(T);
        while (!siblingFound && enumerator.MoveNext())
        {
            if (!itemFound && item.CompareTo(enumerator.Current) == 0)
                itemFound = true;

            if (relativeposition < 0)
            {
                if (!itemFound)
                {
                    trace.Enqueue(enumerator.Current);
                    if (trace.Count > Math.Abs(relativeposition))
                    {
                        trace.Dequeue();
                    }
                }
                else
                {
                    if (trace.Count < Math.Abs(relativeposition))
                    {
                        result = fallback.Invoke(stickySource);
                    }
                    else
                    {
                        result = trace.ToArray()[Math.Abs(relativeposition) - 1];
                        siblingFound = true;
                    }

                    break;
                }
            }
            else
            {
                if (itemFound)
                {
                    while (relativeposition > 0 && enumerator.MoveNext())
                    {
                        relativeposition--;
                    }

                    if (relativeposition > 0)
                    {
                        result = fallback.Invoke(stickySource);
                    }
                    else
                    {
                        result = enumerator.Current;
                        siblingFound = true;
                        break;
                    }
                }
            }
        }

        if (!siblingFound)
            result = fallback.Invoke(stickySource);
        return result;

    })
    where T : IComparable<T>
{
    /// <summary>
    /// Next neighbour element in a <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="item">item to start</param>
    public Sibling(T item, IEnumerable<T> source) : this(
        item,
        source,
        1,
        _ => throw new ArgumentException("Can't get neighbour from iterable")
    )
    {
    }

    /// <summary>
    /// Next neighbour in a <see cref="IEnumerable{T}"/> with a fallback value.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="fallback">fallback func</param>
    /// <param name="item">item to start</param>
    public Sibling(T item, IEnumerable<T> source, T fallback) : this(item, source, 1,
        _ => fallback
    )
    {
    }

    /// <summary>
    /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="relativeposition">requested position relative to the given item</param>
    /// <param name="item">item to start</param>
    public Sibling(T item, IEnumerable<T> source, int relativeposition) : this(
        item, source, relativeposition,
        _ => throw new IOException($"Can't get neighbour at position {relativeposition} from iterable")
    )
    {
    }

    /// <summary>
    /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="relativeposition">requested position relative to the given item</param>
    /// <param name="fallback">fallback func</param>
    /// <param name="item">item to start</param>
    public Sibling(T item, IEnumerable<T> source, int relativeposition, T fallback) : this(item, source,
        relativeposition, _ => fallback)
    {
    }
}

public static partial class EnumerableSmarts
{

    /// <summary>
    /// Next neighbour element in a <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="item">item to start</param>
    public static IScalar<T> Sibling<T>(this IEnumerable<T> source, T item)
        where T : IComparable<T> =>
        new Sibling<T>(item, source);

    /// <summary>
    /// Next neighbour in a <see cref="IEnumerable{T}"/> with a fallback value.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="fallback">fallback func</param>
    /// <param name="item">item to start</param>
    public static IScalar<T> Sibling<T>(this IEnumerable<T> source, T item, T fallback)
        where T : IComparable<T> =>
        new Sibling<T>(item, source, fallback);

    /// <summary>
    /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="relativeposition">requested position relative to the given item</param>
    /// <param name="item">item to start</param>
    public static IScalar<T> Sibling<T>(this IEnumerable<T> source, T item, int relativeposition)
        where T : IComparable<T> =>
        new Sibling<T>(item, source, relativeposition);

    /// <summary>
    /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="relativeposition">requested position relative to the given item</param>
    /// <param name="fallback">fallback func</param>
    /// <param name="item">item to start</param>
    public static IScalar<T> Sibling<T>(this IEnumerable<T> source, T item, int relativeposition, T fallback)
        where T : IComparable<T> =>
        new Sibling<T>(item, source, relativeposition, fallback);
}

