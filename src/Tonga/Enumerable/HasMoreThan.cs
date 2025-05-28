using System;
using System.Collections;
using Tonga.Fact;

namespace Tonga.Enumerable;

/// <summary>
/// Tells if an enumerable has more than the specified items.
/// </summary>
public sealed class HasMoreThan(int amount, IEnumerable source) : FactEnvelope(
    () =>
    {
        if (amount < 0)
        {
            throw new ArgumentException($"A positive number is needed for amount (amount: {amount})");
        }

        var current = 0;
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext() && current <= amount)
        {
            current++;
        }

        return current > amount;
    }
);

public static partial class EnumerableSmarts
{
    /// <summary>
    /// Tells if an enumerable has more than the specified items.
    /// </summary>
    public static IFact HasMoreThan(IEnumerable source, int amount) => new HasMoreThan(amount, source);
}
