using System;
using System.Collections.Generic;
using Tonga.Scalar;
using Tonga.Text;

namespace Tonga.Enumerable;

/// <summary>
/// Element from position in a <see cref="IEnumerable{T}"/>.
/// </summary>
/// <typeparam name="T">type of element</typeparam>
public sealed class ItemAt<T>(
    IEnumerable<T> source, int position, Func<Exception, IEnumerable<T>, T> fallback) : ScalarEnvelope<T>(
    () =>
    {
        T result;
        try
        {
            if (position < 0)
            {
                throw new InvalidOperationException(
                    new Formatted(
                        "The position must be non-negative but is {0}",
                        position.ToString()
                    ).Str()
                );
            }

            var enumerator = source.GetEnumerator();
            bool moved;
            for(var current = 0;current<=position;current++)
            {
                moved = enumerator.MoveNext();
                if(current == 0 && !moved)
                    throw new InvalidOperationException($"Enumerable is empty.");
                if (!moved)
                    throw new InvalidOperationException($"Cannot get item {position + 1} - The enumerable has only {current} items.");
            }
            result = enumerator.Current;
        }
        catch (Exception ex)
        {
            result = fallback.Invoke(ex, source);
        }
        return result;
    }
)
{
    /// <summary>
    /// Element at position in <see cref="IEnumerable{T}"/> with given Exception thrown on fallback
    /// </summary>
    /// <param name="source"></param>
    /// <param name="position"></param>
    /// <param name="ex"></param>
    public ItemAt(IEnumerable<T> source, int position, Exception ex) : this(
        source,
        position,
        _ => throw ex
    )
    { }

    /// <summary>
    /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="position">position</param>
    /// <param name="fallback">fallback func</param>
    public ItemAt(IEnumerable<T> source, int position, T fallback) : this(
        source,
        position,
        _ => fallback
    )
    { }

    /// <summary>
    /// Element from position in a <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="position">position of item</param>
    public ItemAt(IEnumerable<T> source, int position) : this(
            source,
            position,
            (ex, _) => throw new ArgumentException(
                new Formatted(
                    "Cannot get element at position {0}: {1}",
                    (position+1).ToString(),
                    ex.Message,
                    position.ToString()
                ).Str()
            )
    )
    { }

    /// <summary>
    /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="position">position of item</param>
    /// <param name="fallback">fallback func</param>
    public ItemAt(IEnumerable<T> source, int position, Func<IEnumerable<T>, T> fallback) : this(
        source,
        position,
        (_, enumerable) =>
            fallback.Invoke(enumerable)
    )
    { }
}

public static partial class EnumerableSmarts
{
    /// <summary>
    /// Element at position in <see cref="IEnumerable{T}"/> with given Exception thrown on fallback
    /// </summary>
    /// <param name="source"></param>
    /// <param name="position"></param>
    /// <param name="ex"></param>
    public static IScalar<T> ItemAt<T>(this IEnumerable<T> source, int position, Exception ex)
        => new ItemAt<T>(source, position, ex);

    /// <summary>
    /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="position">position</param>
    /// <param name="fallback">fallback func</param>
    public static IScalar<T> ItemAt<T>(this IEnumerable<T> source, int position, T fallback)
        => new ItemAt<T>(source, position, fallback);

    /// <summary>
    /// Element from position in a <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="position">position of item</param>
    public static IScalar<T> ItemAt<T>(this IEnumerable<T> source, int position)
        => new ItemAt<T>(source, position);

    /// <summary>
    /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="position">position of item</param>
    /// <param name="fallback">fallback func</param>
    public static IScalar<T> ItemAt<T>(this IEnumerable<T> source, int position, Func<IEnumerable<T>, T> fallback)
        => new ItemAt<T>(source, position, fallback);

    /// <summary>
    /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="position">position of item</param>
    /// <param name="fallback">fallback func</param>
    public static IScalar<T> ItemAt<T>(this IEnumerable<T> source, int position, Func<Exception, IEnumerable<T>, T> fallback)
        => new ItemAt<T>(source, position, fallback);
}
