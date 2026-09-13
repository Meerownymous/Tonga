using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tonga.Fact;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Tells if an async enumerable has more than the specified items.
/// Reads no more items than needed to decide.
/// </summary>
public sealed class HasMoreThan<T>(int amount, IAsyncEnumerable<T> source) : AsyncFactEnvelope(
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
        return current > amount;
    }
);

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Tells if an async enumerable has more than the specified items.
    /// </summary>
    public static IAsyncFact HasMoreThan<T>(this IAsyncEnumerable<T> source, int amount) =>
        new HasMoreThan<T>(amount, source);
}
