

using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Collection
{
    /// <summary>
    /// A collection which is limited to a number of elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class HeadOf<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lmt">max number of items to limit to</param>
        /// <param name="src">items to limit</param>
        public HeadOf(int lmt, params T[] src) : this(lmt, AsEnumerable._(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lmt">max number of items to limit to</param>
        /// <param name="src">Enumerator to limit</param>
        public HeadOf(int lmt, IEnumerator<T> src) : this(lmt, Enumerable.AsEnumerable._(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lmt">requested number of items</param>
        /// <param name="src">enumerable of items</param>
        public HeadOf(int lmt, IEnumerable<T> src) : this(lmt, new LiveCollection<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source collection</param>
        /// <param name="lmt">requested number of elements</param>
        public HeadOf(int lmt, ICollection<T> src) : base(
            () => new LiveCollection<T>(
                new Enumerable.Head<T>(src, lmt)
            )
        )
        { }
    }

    public static class HeadOf
    {
        public static ICollection<T> _<T>(int lmt, params T[] src) => new HeadOf<T>(lmt, src);
        public static ICollection<T> _<T>(int lmt, ICollection<T> src) => new HeadOf<T>(lmt, src);
        public static ICollection<T> _<T>(int lmt, IEnumerable<T> src) => new HeadOf<T>(lmt, src);
        public static ICollection<T> _<T>(int lmt, IEnumerator<T> src) => new HeadOf<T>(lmt, src);
    }
}
