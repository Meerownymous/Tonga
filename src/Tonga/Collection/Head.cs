

using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Collection
{
    /// <summary>
    /// A collection which is limited to a number of elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Head<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lmt">max number of items to limit to</param>
        /// <param name="src">items to limit</param>
        public Head(int lmt, params T[] src) : this(lmt, AsEnumerable._(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lmt">max number of items to limit to</param>
        /// <param name="src">Enumerator to limit</param>
        public Head(int lmt, IEnumerator<T> src) : this(lmt, Enumerable.AsEnumerable._(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lmt">requested number of items</param>
        /// <param name="src">enumerable of items</param>
        public Head(int lmt, IEnumerable<T> src) : this(lmt, new LiveCollection<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source collection</param>
        /// <param name="lmt">requested number of elements</param>
        public Head(int lmt, ICollection<T> src) : base(
            () => new LiveCollection<T>(
                new Enumerable.Head<T>(src, lmt)
            )
        )
        { }
    }

    /// <summary>
    /// A collection which is limited to a number of elements.
    /// </summary>
    public static class HeadOf
    {
        /// <summary>
        /// A collection which is limited to a number of elements.
        /// </summary>
        public static ICollection<T> _<T>(int lmt, params T[] src) => new Head<T>(lmt, src);

        /// <summary>
        /// A collection which is limited to a number of elements.
        /// </summary>
        public static ICollection<T> _<T>(int lmt, ICollection<T> src) => new Head<T>(lmt, src);

        /// <summary>
        /// A collection which is limited to a number of elements.
        /// </summary>
        public static ICollection<T> _<T>(int lmt, IEnumerable<T> src) => new Head<T>(lmt, src);

        /// <summary>
        /// A collection which is limited to a number of elements.
        /// </summary>
        public static ICollection<T> _<T>(int lmt, IEnumerator<T> src) => new Head<T>(lmt, src);
    }
}
