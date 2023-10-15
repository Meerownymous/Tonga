

using System;
using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.Scalar
{
    /// <summary>
    /// Lookup if an item is in a enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Contains<T> : ScalarEnvelope<bool>
    {
        /// <summary>
        /// Lookup if an item is in a enumerable by calling .Equals(...) of the item.
        /// </summary>
        /// <param name="item">item to lookup</param>
        /// <param name="src">enumerable to test</param>
        public Contains(IEnumerable<T> src, T item) : this(
            src,
            (cdd) => cdd.Equals(item))
        { }

        /// <summary>
        /// Lookup if any item matches the given function
        /// </summary>
        /// <param name="items">enumerable to search through</param>
        /// <param name="match">check to perform on each item</param>
        public Contains(IEnumerable<T> items, Func<T, bool> match) : base(() =>
            {
                var found = false;
                var enumerator = items.GetEnumerator();
                while(!found && enumerator.MoveNext())
                {
                    found = match(enumerator.Current);
                }
                return found;
            })
        { }
    }

    /// <summary>
    /// Lookup if an item is in a enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Contains
    {
        public static IScalar<bool> New<T>(IEnumerable<T> src, T item) => new Contains<T>(src, item);

        public static IScalar<bool> New<T>(IEnumerable<T> items, Func<T, bool> match) => new Contains<T>(items, match);
    }
}
