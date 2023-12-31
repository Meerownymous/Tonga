

using System;
using System.Collections;
using System.Collections.Generic;
using Tonga.List;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class Sorted<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> source;
        private readonly Comparer<T> comparer;

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> with the given items sorted by default.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(params T[] src) : this(Comparer<T>.Default, src)
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(IEnumerable<T> src) : this(Comparer<T>.Default, src)
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public Sorted(Comparer<T> cmp, IEnumerable<T> src)
        {
            this.source = src;
            this.comparer = cmp;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var sorted = SortedCopy(this.source, this.comparer);
            foreach(var item in sorted)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private static List<T> SortedCopy(IEnumerable<T> source, Comparer<T> comparer)
        {
            var result = new List<T>(source);
            result.Sort(comparer);
            return result;
        }
    }

    /// <summary>
    /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public static class Sorted
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> with the given items sorted by default.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> _<T>(params T[] src) where T : IComparable<T> =>
            new Sorted<T>(src);


        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> _<T>(IEnumerable<T> src) where T : IComparable<T> =>
            new Sorted<T>(src);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> _<T>(Comparer<T> cmp, IEnumerable<T> src) =>
            new Sorted<T>(cmp, src);
    }
}

