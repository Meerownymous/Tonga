using System;
using System.Collections;
using Tonga.Fact;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Tells if an enumerable has less than the specified items.
    /// </summary>
    public sealed class IsLess : FactEnvelope
    {
        /// <summary>
        /// Tells if an enumerable has less than the specified items.
        /// </summary>
        public IsLess(int amount, IEnumerable source) : base(
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
                    return current < amount;
                }
            )
        )
        { }

        /// <summary>
        /// Tells if an enumerable has less than the specified items.
        /// </summary>
        public static IsLess _(int amount, IEnumerable source) => new IsLess(amount, source);
    }
}
