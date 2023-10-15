using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.List
{
    /// <summary>
    /// Makes a readonly list.
    /// </summary>
    /// <typeparam name="T">type of items</typeparam>
    public sealed class LiveList<T> : ListEnvelope<T>
    {
        /// <summary>
        /// Makes a readonly list.
        /// </summary>
        public LiveList(params T[] array) : this(() =>
            new List<T>(array)
        )
        { }

        /// <summary>
        /// Makes a readonly list.
        /// </summary>
        public LiveList(IEnumerator<T> src) : this(() =>
            new List<T>(
                new EnumerableOf<T>(() => src)
            )
        )
        { }

        /// <summary>
        /// Makes a readonly list.
        /// </summary>
        public LiveList(IEnumerable<T> src) : this(
            () => new List<T>(src)
        )
        { }

        /// <summary>
        /// Makes a readonly list.
        /// </summary>
        /// <param name="src">source enumerable</param>
        public LiveList(Func<IList<T>> src) : base(
            src,
            true
        )
        { }
    }

    public static class LiveList
    {
        /// <summary>
        /// Makes a readonly list.
        /// </summary>
        public static IList<T> New<T>(params T[] array)
            => new LiveList<T>(array);

        /// <summary>
        /// Makes a readonly list.
        /// </summary>
        public static IList<T> New<T>(IEnumerator<T> src) =>
            new LiveList<T>(src);

        /// <summary>
        /// Makes a readonly list.
        /// </summary>
        public static IList<T> New<T>(IEnumerable<T> src) =>
            new LiveList<T>(src);

        /// <summary>
        /// Makes a readonly list.
        /// </summary>
        public static IList<T> New<T>(Func<IList<T>> src) =>
            new LiveList<T>(src);
    }
}
