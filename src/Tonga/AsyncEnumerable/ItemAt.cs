using System;
using System.Collections.Generic;
using Tonga.Scalar;
using Tonga.Text;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Element from a position in an <see cref="IAsyncEnumerable{T}"/>.
/// Reads no further than the requested position.
/// </summary>
public sealed class ItemAt<T>(
    IAsyncEnumerable<T> source, int position, Func<Exception, IAsyncEnumerable<T>, T> fallback
) : AsyncScalarEnvelope<T>(
    async cancellation =>
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

            var enumerator = source.GetAsyncEnumerator(cancellation);
            try
            {
                for (var current = 0; current <= position; current++)
                {
                    var moved = await enumerator.MoveNextAsync();
                    if (current == 0 && !moved)
                        throw new InvalidOperationException("Enumerable is empty.");
                    if (!moved)
                        throw new InvalidOperationException(
                            $"Cannot get item {position + 1} - The enumerable has only {current} items."
                        );
                }
                result = enumerator.Current;
            }
            finally
            {
                await enumerator.DisposeAsync();
            }
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            result = fallback(ex, source);
        }
        return result;
    }
)
{
    /// <summary>
    /// Element at a position with a given exception thrown on fallback.
    /// </summary>
    public ItemAt(IAsyncEnumerable<T> source, int position, Exception ex) : this(
        source, position, (_, _) => throw ex
    )
    { }

    /// <summary>
    /// Element at a position with a fallback value.
    /// </summary>
    public ItemAt(IAsyncEnumerable<T> source, int position, T fallback) : this(
        source, position, (_, _) => fallback
    )
    { }

    /// <summary>
    /// Element from a position in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public ItemAt(IAsyncEnumerable<T> source, int position) : this(
        source,
        position,
        (ex, _) => throw new ArgumentException(
            new Formatted(
                "Cannot get element at position {0}: {1}",
                (position + 1).ToString(),
                ex.Message
            ).Str()
        )
    )
    { }

    /// <summary>
    /// Element at a position with a fallback function.
    /// </summary>
    public ItemAt(IAsyncEnumerable<T> source, int position, Func<IAsyncEnumerable<T>, T> fallback) : this(
        source, position, (_, enumerable) => fallback(enumerable)
    )
    { }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Element from a position in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> ItemAt<T>(this IAsyncEnumerable<T> source, int position) =>
        new ItemAt<T>(source, position);

    /// <summary>
    /// Element at a position with a given exception thrown on fallback.
    /// </summary>
    public static IAsyncScalar<T> ItemAt<T>(this IAsyncEnumerable<T> source, int position, Exception ex) =>
        new ItemAt<T>(source, position, ex);

    /// <summary>
    /// Element at a position with a fallback value.
    /// </summary>
    public static IAsyncScalar<T> ItemAt<T>(this IAsyncEnumerable<T> source, int position, T fallback) =>
        new ItemAt<T>(source, position, fallback);

    /// <summary>
    /// Element at a position with a fallback function.
    /// </summary>
    public static IAsyncScalar<T> ItemAt<T>(
        this IAsyncEnumerable<T> source, int position, Func<IAsyncEnumerable<T>, T> fallback
    ) =>
        new ItemAt<T>(source, position, fallback);

    /// <summary>
    /// Element at a position with a fallback function.
    /// </summary>
    public static IAsyncScalar<T> ItemAt<T>(
        this IAsyncEnumerable<T> source, int position, Func<Exception, IAsyncEnumerable<T>, T> fallback
    ) =>
        new ItemAt<T>(source, position, fallback);
}
