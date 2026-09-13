using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// A filtered <see cref="IAsyncEnumerable{T}"/>.
/// Pass a filter function which will be applied to all items.
/// </summary>
public sealed class Filtered<T>(Func<T, CancellationToken, ValueTask<bool>> pass, IAsyncEnumerable<T> src) :
    IAsyncEnumerable<T>
{
    /// <summary>
    /// A filtered <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public Filtered(Func<T, bool> pass, IAsyncEnumerable<T> src) : this(
        (item, _) => new ValueTask<bool>(pass(item)),
        src
    )
    { }

    /// <summary>
    /// A filtered <see cref="IAsyncEnumerable{T}"/> which awaits the condition.
    /// </summary>
    public Filtered(Func<T, ValueTask<bool>> pass, IAsyncEnumerable<T> src) : this(
        (item, _) => pass(item),
        src
    )
    { }

    /// <summary>
    /// The given items filtered by the given condition.
    /// </summary>
    public Filtered(Func<T, bool> pass, params T[] items) : this(pass, items.AsAsyncEnumerable())
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        await foreach (var item in src.WithCancellation(cancellation))
        {
            if (await pass(item, cancellation))
            {
                yield return item;
            }
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// A filtered <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public static IAsyncEnumerable<T> AsFiltered<T>(this IAsyncEnumerable<T> src, Func<T, bool> fnc) =>
        new Filtered<T>(fnc, src);

    /// <summary>
    /// A filtered <see cref="IAsyncEnumerable{T}"/> which awaits the condition.
    /// </summary>
    public static IAsyncEnumerable<T> AsFiltered<T>(this IAsyncEnumerable<T> src, Func<T, ValueTask<bool>> fnc) =>
        new Filtered<T>(fnc, src);
}
