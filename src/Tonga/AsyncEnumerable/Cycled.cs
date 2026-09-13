using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// A <see cref="IAsyncEnumerable{T}"/> that starts from the beginning when ended.
/// </summary>
public sealed class Cycled<T>(IAsyncEnumerable<T> enumerable) : IAsyncEnumerable<T>
{
    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var copies = new List<T>();
        await foreach (var item in enumerable.WithCancellation(cancellation))
        {
            copies.Add(item);
            yield return item;
        }

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
