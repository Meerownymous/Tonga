using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tonga.Fact;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Tells if an async enumerable has less than the specified items.
/// Reads no more items than needed to decide.
/// </summary>
public sealed class HasLessThan<T>(int amount, IAsyncEnumerable<T> source) : AsyncFactEnvelope(
    async cancellation =>
    {
        if (amount < 0)
            throw new ArgumentException($"A positive number is needed for amount (amount: {amount})");

        var current = 0;
        var enumerator = source.GetAsyncEnumerator(cancellation);
        try
        {
            while (current <= amount && await enumerator.MoveNextAsync())
                current++;
        }
        finally
        {
            await enumerator.DisposeAsync();
        }
        return current < amount;
    }
);

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Tells if an async enumerable has less than the specified items.
    /// </summary>
    public static IAsyncFact HasLessThan<T>(this IAsyncEnumerable<T> source, int amount) =>
        new HasLessThan<T>(amount, source);
}
