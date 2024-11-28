

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Intersection of two enumerables.
    /// </summary>
    public sealed class Intersection<T>(IEnumerable<T> a, IEnumerable<T> b, IEqualityComparer<T> comparison) : IEnumerable<T>
    {
        /// <summary>
        /// Intersection of two enumerables.
        /// </summary>
        public Intersection(IEnumerable<T> a, IEnumerable<T> b) : this(
            a, b, new Comparison((left,right) => left.Equals(right))
        )
        { }

        /// <summary>
        /// Intersection of two enumerables.
        /// </summary>
        public Intersection(T[] a, T[] b) : this(
            new AsEnumerable<T>(a),
            new AsEnumerable<T>(b)
        )
        { }

        /// <summary>
        /// Intersection of two enumerables.
        /// </summary>
        public Intersection(IEnumerable<T> a, IEnumerable<T> b, Func<T, T, bool> compare) : this(
            a,
            b,
            new Comparison(compare)
        )
        { }

        /// <summary>
        /// Intersection of two enumerables.
        /// </summary>
        public Intersection(T[] a, T[] b, Func<T, T, bool> compare) : this(
            new AsEnumerable<T>(a),
            new AsEnumerable<T>(b),
            new Comparison(compare)
        )
        { }

        public IEnumerator<T> GetEnumerator() => Produced(a, b, comparison);

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private static IEnumerator<T> Produced(IEnumerable<T> left, IEnumerable<T> right, IEqualityComparer<T> comparison)
        {
            var set = new HashSet<T>(right, comparison);
            var intersection = new List<T>();

            foreach (var item in left)
            {
                if (set.Contains(item))
                {
                    intersection.Add(item);
                    set.Remove(item); // Ensures each item is added only once
                }
            }

            return intersection.GetEnumerator();
        }

        private sealed class Comparison(Func<T, T, bool> comparison) : IEqualityComparer<T>
        {
            public bool Equals(T x, T y) => comparison(y, x);
            public int GetHashCode(T obj) => 0;
        }
    }

    /// <summary>
    /// Intersection of two enumerables.
    /// </summary>
    public static class Intersection
    {
        /// <summary>
        /// Intersection of two enumerables.
        /// </summary>
        public static IEnumerable<T> _<T>(IEnumerable<T> a, IEnumerable<T> b, Func<T, T, bool> compare) =>
            new Intersection<T>(a, b, compare);

        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        public static IEnumerable<T> _<T>(IEnumerable<T> a, IEnumerable<T> b) =>
            new Intersection<T>(a, b);
    }
}
