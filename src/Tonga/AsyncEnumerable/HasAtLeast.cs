using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tonga.Fact;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Tells if an async enumerable has at least the specified item count.
/// Reads no more items than needed to decide.
/// </summary>
public sealed class HasAtLeast<T>(int amount, IAsyncEnumerable<T> source) : AsyncFactEnvelope(
    async cancellation =>
    {
        if (amount < 0)
            throw new ArgumentException($"A positive number is needed for amount (amount: {amount}).");

        var current = 0;
        var enumerator = source.GetAsyncEnumerator(cancellation);
        try
        {
            while (current < amount && await enumerator.MoveNextAsync())
                current++;
        }
        finally
        {
            await enumerator.DisposeAsync();
        }
        return current == amount;
    }
);

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Tells if an async enumerable has at least the specified item count.
    /// </summary>
    public static IAsyncFact HasAtLeast<T>(this IAsyncEnumerable<T> source, int amount) =>
        new HasAtLeast<T>(amount, source);
}
