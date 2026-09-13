using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Multiple <see cref="IAsyncEnumerable{T}"/> joined together.
/// </summary>
public sealed class Joined<T>(IAsyncEnumerable<IAsyncEnumerable<T>> items) : IAsyncEnumerable<T>
{
    /// <summary>
    /// Multiple <see cref="IAsyncEnumerable{T}"/> joined together.
    /// </summary>
    public Joined(params IAsyncEnumerable<T>[] items) : this(items.AsAsyncEnumerable())
    { }

    /// <summary>
    /// Join a <see cref="IAsyncEnumerable{T}"/> with (multiple) single elements.
    /// </summary>
    public Joined(IAsyncEnumerable<T> lst, params T[] items) : this(
        lst, new AsAsyncEnumerable<T>(items)
    )
    { }

    /// <summary>
    /// Multiple <see cref="IAsyncEnumerable{T}"/> joined together.
    /// </summary>
    public Joined(IEnumerable<IAsyncEnumerable<T>> items) : this(items.AsAsyncEnumerable())
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        await foreach (var enumerable in items.WithCancellation(cancellation))
        {
            await foreach (var item in enumerable.WithCancellation(cancellation))
            {
                yield return item;
            }
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Join two <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public static IAsyncEnumerable<T> AsJoined<T>(this IAsyncEnumerable<T> lst, IAsyncEnumerable<T> items) =>
        new Joined<T>(lst, items);

    /// <summary>
    /// Join a <see cref="IAsyncEnumerable{T}"/> with (multiple) single elements.
    /// </summary>
    public static IAsyncEnumerable<T> AsJoined<T>(this IAsyncEnumerable<T> lst, params T[] items) =>
        new Joined<T>(lst, items);

    /// <summary>
    /// Multiple <see cref="IAsyncEnumerable{T}"/> joined together.
    /// </summary>
    public static IAsyncEnumerable<T> AsJoined<T>(this IAsyncEnumerable<IAsyncEnumerable<T>> items) =>
        new Joined<T>(items);

    /// <summary>
    /// Multiple <see cref="IAsyncEnumerable{T}"/> joined together.
    /// </summary>
    public static IAsyncEnumerable<T> AsJoined<T>(this IEnumerable<IAsyncEnumerable<T>> items) =>
        new Joined<T>(items);
}
