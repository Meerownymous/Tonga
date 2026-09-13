using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// A reversed <see cref="IAsyncEnumerable{T}"/>.
/// Reversing needs every item, so this reads the whole source before yielding the first item.
/// </summary>
public sealed class Reversed<T>(IAsyncEnumerable<T> src) : IAsyncEnumerable<T>
{
    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var items = new List<T>();
        await foreach (var item in src.WithCancellation(cancellation))
        {
            items.Add(item);
        }
        items.Reverse();
        foreach (var item in items)
        {
            yield return item;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// A reversed <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public static IAsyncEnumerable<T> AsReversed<T>(this IAsyncEnumerable<T> src) => new Reversed<T>(src);
}
