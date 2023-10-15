

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
        private readonly IEnumerable<T> result;

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public Distinct(params IEnumerable<T>[] enumerables) : this(
            EnumerableOf.Pipe(enumerables)
        )
        { }

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public Distinct(IEnumerable<IEnumerable<T>> enumerables) : this(
            enumerables,
            (v1, v2) => v1.Equals(v2)
        )
        { }

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        /// <param name="comparison">comparison to evaluate distinction</param>
        public Distinct(IEnumerable<IEnumerable<T>> enumerables, Func<T, T, bool> comparison)
        {
            this.result =
                EnumerableOf.Pipe(() =>
                    this.Produced(
                        Joined.New(enumerables),
                        new Comparison<T>(comparison)
                    )
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

        private IEnumerator<T> Produced(IEnumerable<T> source, Comparison<T> comparison)
        {
            var set = new HashSet<T>(comparison);
            foreach (var item in source)
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
        public static IEnumerable<T> Sticky<T>(params IEnumerable<T>[] enumerables) =>
            Enumerable.Sticky.New(new Distinct<T>(enumerables));

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public static IEnumerable<T> Sticky<T>(IEnumerable<IEnumerable<T>> enumerables) =>
            Enumerable.Sticky.New(new Distinct<T>(enumerables));

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        /// <param name="comparison">comparison to evaluate distinction</param>
        public static IEnumerable<T> Sticky<T>(IEnumerable<IEnumerable<T>> enumerables, Func<T, T, bool> comparison) =>
            Enumerable.Sticky.New(new Distinct<T>(enumerables, comparison));

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public static IEnumerable<T> Pipe<T>(params IEnumerable<T>[] enumerables) =>
            new Distinct<T>(enumerables);

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public static IEnumerable<T> Pipe<T>(IEnumerable<IEnumerable<T>> enumerables) =>
            new Distinct<T>(enumerables);

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        /// <param name="comparison">comparison to evaluate distinction</param>
        public static IEnumerable<T> Pipe<T>(IEnumerable<IEnumerable<T>> enumerables, Func<T, T, bool> comparison) =>
            new Distinct<T>(enumerables, comparison);
    }


}