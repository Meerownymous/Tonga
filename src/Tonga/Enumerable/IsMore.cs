using System;
using System.Collections;

namespace Tonga.Fact
{
    /// <summary>
    /// Tells if an enumerable has more than the specified items.
    /// </summary>
    public sealed class MoreThan : FactEnvelope
    {
        /// <summary>
        /// Tells if an enumerable has more than the specified items.
        /// </summary>
        public MoreThan(int amount, IEnumerable source) : base(
            new AsFact(() =>
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
            )
        )
        { }

        /// <summary>
        /// Tells if an enumerable has more than the specified items.
        /// </summary>
        public static MoreThan _(int amount, IEnumerable source) => new(amount, source);
    }
}
