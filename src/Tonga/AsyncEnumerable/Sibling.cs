using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tonga.Scalar;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Element before or after another element in an <see cref="IAsyncEnumerable{T}"/>.
/// </summary>
public sealed class Sibling<T>(
    T item,
    IAsyncEnumerable<T> source,
    int relativeposition,
    Func<IAsyncEnumerable<T>, T> fallback
) : AsyncScalarEnvelope<T>(
    async cancellation =>
    {
        var trace = new Queue<T>();
        var itemFound = false;
        var siblingFound = false;
        var remaining = relativeposition;
        var stickySource = source.AsSticky();
        var enumerator = stickySource.GetAsyncEnumerator(cancellation);
        var result = default(T);

        try
        {
            while (!siblingFound && await enumerator.MoveNextAsync())
            {
                if (!itemFound && item.CompareTo(enumerator.Current) == 0)
                    itemFound = true;

                if (remaining < 0)
                {
                    if (!itemFound)
                    {
                        trace.Enqueue(enumerator.Current);
                        if (trace.Count > Math.Abs(remaining))
                            trace.Dequeue();
                    }
                    else
                    {
                        if (trace.Count < Math.Abs(remaining))
                            result = fallback(stickySource);
                        else
                        {
                            result = trace.ToArray()[Math.Abs(remaining) - 1];
                            siblingFound = true;
                        }
                        break;
                    }
                }
                else if (itemFound)
                {
                    while (remaining > 0 && await enumerator.MoveNextAsync())
                        remaining--;

                    if (remaining > 0)
                        result = fallback(stickySource);
                    else
                    {
                        result = enumerator.Current;
                        siblingFound = true;
                    }
                    break;
                }
            }
        }
        finally
        {
            await enumerator.DisposeAsync();
        }

        if (!siblingFound)
            result = fallback(stickySource);
        return result;
    })
    where T : IComparable<T>
{
    /// <summary>
    /// Next neighbour element in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public Sibling(T item, IAsyncEnumerable<T> source) : this(
        item, source, 1, _ => throw new ArgumentException("Can't get neighbour from iterable")
    )
    { }

    /// <summary>
    /// Next neighbour in an <see cref="IAsyncEnumerable{T}"/> with a fallback value.
    /// </summary>
    public Sibling(T item, IAsyncEnumerable<T> source, T fallback) : this(item, source, 1, _ => fallback)
    { }

    /// <summary>
    /// Element at a relative position in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public Sibling(T item, IAsyncEnumerable<T> source, int relativeposition) : this(
        item,
        source,
        relativeposition,
        _ => throw new IOException($"Can't get neighbour at position {relativeposition} from iterable")
    )
    { }

    /// <summary>
    /// Element at a relative position in an <see cref="IAsyncEnumerable{T}"/> with a fallback value.
    /// </summary>
    public Sibling(T item, IAsyncEnumerable<T> source, int relativeposition, T fallback) : this(
        item, source, relativeposition, _ => fallback
    )
    { }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Next neighbour element in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> Sibling<T>(this IAsyncEnumerable<T> source, T item)
        where T : IComparable<T> =>
        new Sibling<T>(item, source);

    /// <summary>
    /// Next neighbour in an <see cref="IAsyncEnumerable{T}"/> with a fallback value.
    /// </summary>
    public static IAsyncScalar<T> Sibling<T>(this IAsyncEnumerable<T> source, T item, T fallback)
        where T : IComparable<T> =>
        new Sibling<T>(item, source, fallback);

    /// <summary>
    /// Element at a relative position in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> Sibling<T>(this IAsyncEnumerable<T> source, T item, int relativeposition)
        where T : IComparable<T> =>
        new Sibling<T>(item, source, relativeposition);

    /// <summary>
    /// Element at a relative position in an <see cref="IAsyncEnumerable{T}"/> with a fallback value.
    /// </summary>
    public static IAsyncScalar<T> Sibling<T>(
        this IAsyncEnumerable<T> source, T item, int relativeposition, T fallback
    )
        where T : IComparable<T> =>
        new Sibling<T>(item, source, relativeposition, fallback);
}
