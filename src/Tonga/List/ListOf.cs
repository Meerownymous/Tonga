

using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.List
{
    /// <summary>
    /// Makes a readonly list.
    /// </summary>
    /// <typeparam name="T">type of items</typeparam>
    public sealed class ListOf<T> : ListEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="array">source array</param>
        public ListOf(params T[] array) : this(new Transit<T>(array))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerator</param>
        public ListOf(IEnumerator<T> src) : base(() => src, false)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerable</param>
        public ListOf(IEnumerable<T> src) : base(
            () => src.GetEnumerator(),
            false
        )
        { }
    }

    public static class ListOf
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="array">source array</param>
        public static IList<T> New<T>(params T[] array)
            => new ListOf<T>(array);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerator</param>
        public static IList<T> New<T>(IEnumerator<T> src)
            => new ListOf<T>(src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerable</param>
        public static IList<T> New<T>(IEnumerable<T> src)
            => new ListOf<T>(src);
    }
}
