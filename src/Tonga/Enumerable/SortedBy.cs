

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    public sealed class SortedBy<T, TKey>(
        Func<T, TKey> subjectExtraction, Comparer<TKey> cmp, IEnumerable<T> src
    ) : IEnumerable<T> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> with the given items sorted by default.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="src">enumerable to sort</param>
        public SortedBy(Func<T, TKey> swap, params T[] src) : this(
            swap,
            Comparer<TKey>.Default,
            src
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="src">enumerable to sort</param>
        public SortedBy(Func<T, TKey> swap, IEnumerable<T> src) : this(
            swap,
            Comparer<TKey>.Default,
            src
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        public SortedBy(Func<T, TKey> swap, Comparison<TKey> compare, IEnumerable<T> src) : this(
            swap,
            Comparer<TKey>.Create(compare),
            src
        )
        { }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in this.Sorted(cmp))
            {
                yield return item.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private SortedDictionary<TKey, T> Sorted(Comparer<TKey> cmp)
        {
            var map = new SortedDictionary<TKey, T>(cmp);
            foreach (var item in src)
            {
                map[subjectExtraction.Invoke(item)] = item;
            }
            return map;
        }
    }

    /// <summary>
    /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    public static class SortedBy
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> with the given items sorted by default.
        /// </summary>
        public static IEnumerable<T> _<T, TKey>(Func<T, TKey> swap, params T[] src) where TKey : IComparable<TKey> =>
            new SortedBy<T, TKey>(swap, src);


        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> _<T, TKey>(Func<T, TKey> swap, IEnumerable<T> src) where TKey : IComparable<TKey> =>
            new SortedBy<T, TKey>(swap, src);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> _<T, TKey>(Func<T, TKey> swap, Comparer<TKey> cmp, IEnumerable<T> src) where TKey : IComparable<TKey> =>
            new SortedBy<T, TKey>(swap, cmp, src);
    }

    /// <summary>
    /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    public static class SortedBySmarts
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> with the given items sorted by default.
        /// </summary>
        public static IEnumerable<T> SortedBy<T, TKey>(this T[] src, Func<T, TKey> swap) where TKey : IComparable<TKey> =>
            new SortedBy<T, TKey>(swap, src);


        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> SortedBy<T, TKey>(this IEnumerable<T> src, Func<T, TKey> swap) where TKey : IComparable<TKey> =>
            new SortedBy<T, TKey>(swap, src);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> SortedBy<T, TKey>(this IEnumerable<T> src, Func<T, TKey> swap, Comparer<TKey> cmp) where TKey : IComparable<TKey> =>
            new SortedBy<T, TKey>(swap, cmp, src);
    }
}

