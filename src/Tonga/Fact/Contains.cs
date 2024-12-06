

using System;
using System.Collections.Generic;

namespace Tonga.Fact
{
    /// <summary>
    /// Lookup if an item is in a enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Contains<T>(IEnumerable<T> src, Func<T, bool> match) : FactEnvelope(
        new AsFact(() =>
            {
                var found = false;
                var enumerator = src.GetEnumerator();
                while(!found && enumerator.MoveNext())
                {
                    found = match(enumerator.Current);
                }
                return found;
            })
        )
    {
        /// <summary>
        /// Lookup if an item is in a enumerable by calling .Equals(...) of the item.
        /// </summary>
        /// <param name="item">item to lookup</param>
        /// <param name="src">enumerable to test</param>
        public Contains(IEnumerable<T> src, T item) : this(
            src,
            cdd => cdd.Equals(item))
        { }
    }

    /// <summary>
    /// Lookup if an item is in a enumerable.
    /// </summary>
    public static class Contains
    {
        /// <summary>
        /// Lookup if an item is in a enumerable.
        /// </summary>
        public static IFact _<T>(IEnumerable<T> items, Func<T, bool> match) => new Contains<T>(items, match);
    }

    /// <summary>
    /// Lookup if an item is in a enumerable.
    /// </summary>
    public static class ContainsSmarts
    {
        /// <summary>
        /// Lookup if an item is in a enumerable.
        /// </summary>
        public static IFact Contains<T>(this IEnumerable<T> items, Func<T, bool> match) => new Contains<T>(items, match);
    }
}
