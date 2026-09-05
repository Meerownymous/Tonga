using System;
using System.Collections;
using Tonga.Fact;

namespace Tonga.Enumerable;

/// <summary>
/// Tells if an enumerable has at least the specified item count.
/// Reads no more items than needed to decide.
/// </summary>
public sealed class HasAtLeast(int amount, IEnumerable source) : FactEnvelope(
    () =>
    {
        if (amount < 0) throw new ArgumentException($"A positive number is needed for amount (amount: {amount}).");
        var current = 0;
        var enumerator = source.GetEnumerator();
        while (current < amount && enumerator.MoveNext())
            current++;

        return current == amount;
    }
);

public static partial class EnumerableSmarts
{
    /// <summary>
    /// Tells if an enumerable has at least the specified item count.
    /// </summary>
    public static IFact HasAtLeast(this IEnumerable source, int amount) => new HasAtLeast(amount, source);
}
