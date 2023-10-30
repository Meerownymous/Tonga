

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class SortedBy<T, TKey> : IEnumerable<T>
        where TKey : IComparable<TKey>
    {
        private readonly IEnumerable<T> source;
        private readonly Comparer<TKey> comparer;
        private readonly Func<T, TKey> subjectExtraction;

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
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="src">enumerable to sort</param>
        public SortedBy(Func<T, TKey> swap, Comparison<TKey> compare, IEnumerable<T> src) : this(
            swap,
            Comparer<TKey>.Create(compare),
            src
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="subjectExtraction">func to swap the type to a sortable type</param>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public SortedBy(Func<T, TKey> subjectExtraction, Comparer<TKey> cmp, IEnumerable<T> src)
        {
            this.comparer = cmp;
            this.subjectExtraction = subjectExtraction;
            this.source = src;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in this.Sorted(this.comparer))
            {
                yield return item.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private SortedDictionary<TKey, T> Sorted(Comparer<TKey> cmp)
        {
            var map = new SortedDictionary<TKey, T>(cmp);
            foreach (var item in this.source)
            {
                map[this.subjectExtraction.Invoke(item)] = item;
            }
            return map;
        }
    }

    /// <summary>
    /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public static class SortedBy
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> with the given items sorted by default.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="src">enumerable to sort</param>
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
}

