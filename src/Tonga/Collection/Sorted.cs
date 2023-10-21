

using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Collection
{
    /// <summary>
    /// A sorted <see cref="ICollection{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Sorted<T> : CollectionEnvelope<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// A list with default sorting (ascending)
        /// </summary>
        /// <param name="src">the source enumerable</param>
        public Sorted(params T[] src) : this(new AsEnumerable<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> with default sorting (ascending)
        /// </summary>
        /// <param name="src">the source enumerable</param>
        public Sorted(IEnumerable<T> src) : this(
            Comparer<T>.Default,
            new LiveCollection<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source enumerable</param>
        public Sorted(Comparer<T> cmp, params T[] src) : this(cmp, new LiveCollection<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source enumerator</param>
        public Sorted(Comparer<T> cmp, IEnumerator<T> src) : this(cmp, new LiveCollection<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source enumerable</param>
        public Sorted(Comparer<T> cmp, IEnumerable<T> src) : this(cmp, new LiveCollection<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source collection</param>
        public Sorted(Comparer<T> cmp, ICollection<T> src) : base(() =>
            new LiveCollection<T>(
                new Enumerable.Sorted<T>(cmp, src)
            )
        )
        { }
    }

    public static class Sorted
    {
        /// <summary>
        public static ICollection<T> New<T>(params T[] src) where T : IComparable<T> => new Sorted<T>(src);

        public static ICollection<T> New<T>(IEnumerable<T> src) where T : IComparable<T> => new Sorted<T>(src);

        public static ICollection<T> New<T>(Comparer<T> cmp, params T[] src) where T : IComparable<T> => new Sorted<T>(cmp, src);

        public static ICollection<T> New<T>(Comparer<T> cmp, IEnumerator<T> src) where T : IComparable<T> => new Sorted<T>(cmp, src);

        public static ICollection<T> New<T>(Comparer<T> cmp, ICollection<T> src) where T : IComparable<T> => new Sorted<T>(cmp, src);
    }
}
