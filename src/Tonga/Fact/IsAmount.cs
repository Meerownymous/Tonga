using System;
using System.Collections;

namespace Tonga.Fact
{
    /// <summary>
    /// Tells if an enumerable has exactly the the specified item count.
    /// </summary>
    public sealed class IsAmount : FactEnvelope
    {
        /// <summary>
        /// Tells if an enumerable has exactly the the specified item count.
        /// </summary>
        public IsAmount(int amount, IEnumerable source) : base(
            new AsFact(() =>
                {
                    if (amount < 0)
                    {
                        throw new ArgumentException($"A positive number is needed for amount (amount: {amount}).");
                    }
                    var current = 0;
                    var enumerator = source.GetEnumerator();
                    while (enumerator.MoveNext() && current <= amount)
                    {
                        current++;
                    }
                    return current == amount;
                }
            )
        )
        { }
    }
}
