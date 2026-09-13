using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Delivers contents from a fallback source in case the original source is empty.
/// </summary>
public sealed class BackFalling<T>(IAsyncEnumerable<T> origin, IAsyncEnumerable<T> fallback) : IAsyncEnumerable<T>
{
    /// <summary>
    /// Delivers contents from a fallback source in case the original source is empty.
    /// </summary>
    public BackFalling(IAsyncEnumerable<T> origin, params T[] fallback) : this(
        origin, new AsAsyncEnumerable<T>(fallback)
    )
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var e = origin.GetAsyncEnumerator(cancellation);
        try
        {
            if (await e.MoveNextAsync())
            {
                do yield return e.Current;
                while (await e.MoveNextAsync());
            }
            else
            {
                await foreach (var item in fallback.WithCancellation(cancellation))
                    yield return item;
            }
        }
        finally
        {
            await e.DisposeAsync();
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Delivers contents from a fallback source in case the original source is empty.
    /// </summary>
    public static IAsyncEnumerable<T> AsBackFalling<T>(
        this IAsyncEnumerable<T> origin, IAsyncEnumerable<T> fallback
    ) =>
        new BackFalling<T>(origin, fallback);
}
