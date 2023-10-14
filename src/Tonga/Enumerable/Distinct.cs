

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Multiple enumerables merged together, so that every entry is unique.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Distinct<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> all;
        private readonly Comparison<T> comparison;
        private readonly IEnumerable<T> result;

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public Distinct(params IEnumerable<T>[] enumerables) : this(
            new LiveMany<IEnumerable<T>>(enumerables)
        )
        { }

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public Distinct(IEnumerable<IEnumerable<T>> enumerables, bool live = false) : this(
            enumerables,
            (v1, v2) => v1.Equals(v2),
            live
        )
        { }

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        /// <param name="comparison">comparison to evaluate distinction</param>
        public Distinct(IEnumerable<IEnumerable<T>> enumerables, Func<T, T, bool> comparison, bool live = false)
        {
            this.all = new Joined<T>(enumerables);
            this.comparison = new Comparison<T>(comparison);
            this.result =
                Ternary.New(
                    LiveMany.New(Produced),
                    Sticky.By(Produced),
                    live
                );
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerable<T> Produced()
        {
            var set = new HashSet<T>(this.comparison);
            foreach (var item in this.all)
            {
                if (set.Add(item))
                    yield return item;
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
                return this.comparison.Invoke(x, y);
            }

            public int GetHashCode(T obj)
            {
                return 0;
            }
        }
    }

    /// <summary>
    /// Multiple enumerables merged together, so that every entry is unique.
    /// </summary>
    public static class Distinct
    {
        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public static IEnumerable<T> New<T>(params IEnumerable<T>[] enumerables) => new Distinct<T>(enumerables);

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public static IEnumerable<T> New<T>(IEnumerable<IEnumerable<T>> enumerables) => new Distinct<T>(enumerables);

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        /// <param name="comparison">comparison to evaluate distinction</param>
        public static IEnumerable<T> New<T>(IEnumerable<IEnumerable<T>> enumerables, Func<T, T, bool> comparison) => new Distinct<T>(enumerables, comparison);
    }


}