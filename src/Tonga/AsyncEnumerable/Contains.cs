using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tonga.Fact;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Lookup if an item is in an async enumerable.
/// Reads no further than the first match.
/// </summary>
public sealed class Contains<T>(IAsyncEnumerable<T> src, Func<T, bool> match) : AsyncFactEnvelope(
    async cancellation =>
    {
        var found = false;
        var enumerator = src.GetAsyncEnumerator(cancellation);
        try
        {
            while (!found && await enumerator.MoveNextAsync())
            {
                found = match(enumerator.Current);
            }
        }
        finally
        {
            await enumerator.DisposeAsync();
        }
        return found;
    }
)
{
    /// <summary>
    /// Lookup if an item is in an async enumerable by calling .Equals(...) of the item.
    /// </summary>
    public Contains(IAsyncEnumerable<T> src, T item) : this(src, cdd => cdd.Equals(item))
    { }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Lookup if an item is in an async enumerable.
    /// </summary>
    public static IAsyncFact Contains<T>(this IAsyncEnumerable<T> src, Func<T, bool> match) =>
        new Contains<T>(src, match);

    /// <summary>
    /// Lookup if an item is in an async enumerable by calling .Equals(...) of the item.
    /// </summary>
    public static IAsyncFact Contains<T>(this IAsyncEnumerable<T> src, T item) => new Contains<T>(src, item);
}
