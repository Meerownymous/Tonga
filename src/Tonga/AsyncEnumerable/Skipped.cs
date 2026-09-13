using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// A <see cref="IAsyncEnumerable{T}"/> which skips a given count of items.
/// </summary>
public sealed class Skipped<T>(IAsyncEnumerable<T> enumerable, int skip) : IAsyncEnumerable<T>
{
    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var skipped = 0;
        await foreach (var item in enumerable.WithCancellation(cancellation))
        {
            if (skipped < skip)
            {
                skipped++;
            }
            else yield return item;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> which skips a given count of items.
    /// </summary>
    public static IAsyncEnumerable<T> AsSkipped<T>(this IAsyncEnumerable<T> enumerable, int skip) =>
        new Skipped<T>(enumerable, skip);
}
