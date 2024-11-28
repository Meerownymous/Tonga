

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Union objects in two enumerables.
    /// </summary>
    public class Union<T>(IEnumerable<T> a, IEnumerable<T> b, IEqualityComparer<T> comparison) : IEnumerable<T>
    {
        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        public Union(IEnumerable<T> a, IEnumerable<T> b) : this(
            a, b, new Comparison<T>((left,right) => left.Equals(right))
        )
        { }

        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        public Union(IEnumerable<T> a, IEnumerable<T> b, Func<T, T, bool> compare) : this(
            a,
            b,
            new Comparison<T>(compare)
        )
        { }

        public IEnumerator<T> GetEnumerator() => Produced(a, b, comparison);

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private static IEnumerator<T> Produced(IEnumerable<T> a, IEnumerable<T> b, IEqualityComparer<T> comparison)
        {
            var all = new HashSet<T>(comparison);
            var union = new HashSet<T>(comparison);

            foreach(var element in Joined._(a, b))
            {
                if(!all.Add(element))
                    union.Add(element);
            }
            return union.GetEnumerator();
        }

        private sealed class Comparison<TItem> : IEqualityComparer<T>
        {
            private readonly Func<T, T, bool> comparison;

            public Comparison(Func<T, T, bool> comparison)
            {
                this.comparison = comparison;
            }

            public bool Equals(T x, T y)
            {
                return this.comparison.Invoke(y, x);
            }

            public int GetHashCode(T obj)
            {
                return 0;
            }
        }
    }

    /// <summary>
    /// Union objects in two enumerables.
    /// </summary>
    public static class Union
    {
        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        /// <param name="compare">Condition to match</param>
        public static IEnumerable<T> _<T>(IEnumerable<T> a, IEnumerable<T> b, Func<T, T, bool> compare) =>
            new Union<T>(a, b, compare);

        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        public static IEnumerable<T> _<T>(IEnumerable<T> a, IEnumerable<T> b) =>
            new Union<T>(a, b);
    }
}
