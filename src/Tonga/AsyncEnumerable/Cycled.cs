using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// A <see cref="IAsyncEnumerable{T}"/> that starts from the beginning when ended.
/// An empty source ends rather than cycling over nothing.
/// </summary>
public sealed class Cycled<T>(IAsyncEnumerable<T> enumerable) : IAsyncEnumerable<T>
{
    /// <summary>
    /// The given items, starting over when ended.
    /// </summary>
    public Cycled(params T[] items) : this(items.AsAsyncEnumerable())
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var copies = new List<T>();
        await foreach (var item in enumerable.WithCancellation(cancellation))
        {
            copies.Add(item);
            yield return item;
        }

        if (copies.Count == 0)
            yield break;

        var current = -1;
        while (true)
        {
            cancellation.ThrowIfCancellationRequested();
            current++;
            if (current >= copies.Count)
            {
                current = 0;
            }
            yield return copies[current];
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> that starts from the beginning when ended.
    /// </summary>
    public static IAsyncEnumerable<T> AsCycled<T>(this IAsyncEnumerable<T> source) => new Cycled<T>(source);
}
