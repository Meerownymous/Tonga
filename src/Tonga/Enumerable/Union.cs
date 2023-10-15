

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Union objects in two enumerables.
    /// </summary>
    public class Union<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> a;
        private readonly IEnumerable<T> b;
        private readonly IEqualityComparer<T> comparison;
        private readonly Ternary<T> result;

        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        /// <param name="compare">Condition to match</param>
        public Union(IEnumerable<T> a, IEnumerable<T> b, bool live = false) : this(
            a, b, new Comparison<T>((left,right) => left.Equals(right)), live
        )
        { }

        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        public Union(IEnumerable<T> a, IEnumerable<T> b, Func<T, T, bool> compare, bool live = false) : this(
            a,
            b,
            new Comparison<T>(compare),
            live
        )
        { }

        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        public Union(IEnumerable<T> a, IEnumerable<T> b, IEqualityComparer<T> compare, bool live = false)
        {
            this.a = a;
            this.b = b;
            this.comparison = compare;
            this.result =
                Ternary.Pipe(
                    EnumerableOf.Pipe(Produced),
                    Sticky.New(Produced),
                    live
                );
        }

        public IEnumerator<T> GetEnumerator() => this.result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private IEnumerator<T> Produced()
        {
            var set = new HashSet<T>(this.comparison);
            foreach(var element in this.b)
            {
                set.Add(element);
            }

            foreach (T element in this.a)
            {
                if(!set.Add(element))
                    yield return element;
            }
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
        public static IEnumerable<T> New<T>(IEnumerable<T> a, IEnumerable<T> b, Func<T, T, bool> compare) =>
            new Union<T>(a, b, compare);

        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        public static IEnumerable<T> New<T>(IEnumerable<T> a, IEnumerable<T> b) =>
            new Union<T>(a, b);
    }
}
