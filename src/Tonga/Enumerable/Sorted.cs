

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class Sorted<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private readonly List<T> source;
        private readonly bool[] sorted;
        private readonly Comparer<T> comparer;
        private readonly bool live;

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
        public Sorted(Comparer<T> cmp, IEnumerable<T> src, bool live = false)
        {
            this.source = new List<T>(src);
            this.sorted = new bool[] { false };
            this.comparer = cmp;
            this.live = live;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if(!this.IsSorted() || this.live)
            {
                this.Sort();
            }
            foreach(var item in this.source)
            {
                yield return item;
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
            this.source.Sort(this.comparer);
            this.sorted[0] = true;
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
        public static IEnumerable<T> New<T>(params T[] src) where T : IComparable<T> =>
            new Sorted<T>(src);


        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> src) where T : IComparable<T> =>
            new Sorted<T>(src);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> New<T>(Comparer<T> cmp, IEnumerable<T> src)
            where T : IComparable<T> =>
            new Sorted<T>(cmp, src);
    }
}

