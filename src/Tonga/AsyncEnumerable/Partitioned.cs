using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Async enumerable partitioned by a given size.
/// </summary>
public sealed class Partitioned<T>(int size, IAsyncEnumerable<T> items) : IAsyncEnumerable<IAsyncEnumerable<T>>
{
    public async IAsyncEnumerator<IAsyncEnumerable<T>> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var source = items.GetAsyncEnumerator(cancellation);
        try
        {
            while (await source.MoveNextAsync())
            {
                var partition = new List<T> { source.Current };
                while (partition.Count < size && await source.MoveNextAsync())
                {
                    partition.Add(source.Current);
                }
                yield return partition.AsAsyncEnumerable();
            }
        }
        finally
        {
            await source.DisposeAsync();
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Async enumerable partitioned by a given size.
    /// </summary>
    public static IAsyncEnumerable<IAsyncEnumerable<T>> AsPartitioned<T>(this IAsyncEnumerable<T> items, int size) =>
        new Partitioned<T>(size, items);
}
