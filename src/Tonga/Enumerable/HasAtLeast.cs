using System;
using System.Collections;
using Tonga.Fact;

namespace Tonga.Enumerable;

/// <summary>
/// Tells if an enumerable has exactly the the specified item count.
/// </summary>
public sealed class HasAtLeast(int amount, IEnumerable source) : FactEnvelope(
    () =>
    {
        if (amount < 0) throw new ArgumentException($"A positive number is needed for amount (amount: {amount}).");
        var current = 0;
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext() && current <= amount)
            current++;

        return current == amount;
    }
);

public static partial class EnumerableSmarts
{
    /// <summary>
    /// Tells if an enumerable has less than the specified items.
    /// </summary>
    public static IFact HasAtLeast(this IEnumerable source, int amount) => new HasLessThan(amount, source);
}
