

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
    public sealed class Sorted<T>(Comparer<T> cmp, IEnumerable<T> src) : IEnumerable<T>
    {
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

        public IEnumerator<T> GetEnumerator()
        {
            var sorted = SortedCopy(src, cmp);
            foreach(var item in sorted)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

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
    public static class Sorted
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> with the given items sorted by default.
        /// </summary>
        public static IEnumerable<T> _<T>(params T[] src) where T : IComparable<T> =>
            new Sorted<T>(src);


        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        public static IEnumerable<T> _<T>(IEnumerable<T> src) where T : IComparable<T> =>
            new Sorted<T>(src);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        public static IEnumerable<T> _<T>(Comparer<T> cmp, IEnumerable<T> src) =>
            new Sorted<T>(cmp, src);
    }

    /// <summary>
    /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    public static class SortedSmarts
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> with the given items sorted by default.
        /// </summary>
        public static IEnumerable<T> Sorted<T>(this T[] src) where T : IComparable<T> =>
            new Sorted<T>(src);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        public static IEnumerable<T> Sorted<T>(this IEnumerable<T> src) where T : IComparable<T> =>
            new Sorted<T>(src);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        public static IEnumerable<T> Sorted<T>(this IEnumerable<T> src, Comparer<T> cmp) =>
            new Sorted<T>(cmp, src);
    }
}

