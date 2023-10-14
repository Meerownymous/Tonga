

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
        private readonly SortedDictionary<TKey, T> map;
        private readonly Func<T, TKey> subjectExtraction;
        private readonly bool[] sorted;

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
            this.map = new SortedDictionary<TKey,T>(cmp);
            this.subjectExtraction = subjectExtraction;
            this.source = src;
            this.sorted = new bool[1] { false };
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (!this.IsSorted())
            {
                this.Sort();
            }
            foreach (var item in this.map)
            {
                yield return item.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private bool IsSorted()
        {
            return this.sorted[0];
        }

        private void Sort()
        {
            foreach(var item in this.source)
            {
                this.map[this.subjectExtraction.Invoke(item)] = item;
            }
            this.sorted[0] = true;
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
        public static IEnumerable<T> New<T, TKey>(Func<T, TKey> swap, params T[] src) where TKey : IComparable<TKey> =>
            new SortedBy<T, TKey>(swap, src);


        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> New<T, TKey>(Func<T, TKey> swap, IEnumerable<T> src) where TKey : IComparable<TKey> =>
            new SortedBy<T, TKey>(swap, src);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> New<T, TKey>(Func<T, TKey> swap, Comparer<TKey> cmp, IEnumerable<T> src) where TKey : IComparable<TKey> =>
            new SortedBy<T, TKey>(swap, cmp, src);
    }
}

