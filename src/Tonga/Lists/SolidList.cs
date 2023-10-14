

using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.List
{
    /// <summary>
    /// A list that is both sticky and threadsafe.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SolidList<T> : ListEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">items to decorate</param>
        public SolidList(params T[] items) : this(new ManyOf<T>(items))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">items to decorate</param>
        public SolidList(IEnumerable<T> items) : base(() =>
            new SyncList<T>(
                new ListOf<T>(items)
            ),
            false
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enumerator">items to decorate</param>
        public SolidList(IEnumerator<T> enumerator) : base(() =>
            new SyncList<T>(
                new ListOf<T>(enumerator)
            ),
            false
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">list to decorate</param>
        public SolidList(ICollection<T> list) : base(() =>
            new SyncList<T>(
                new ListOf<T>(list)
            ),
            false
        )
        { }
    }

    public static class SolidList
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">items to decorate</param>
        public static IList<T> New<T>(params T[] items)
            => new SolidList<T>(items);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">items to decorate</param>
        public static IList<T> New<T>(IEnumerable<T> items)
            => new SolidList<T>(items);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enumerator">items to decorate</param>
        public static IList<T> New<T>(IEnumerator<T> enumerator)
            => new SolidList<T>(enumerator);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">list to decorate</param>
        public static IList<T> New<T>(ICollection<T> list)
            => new SolidList<T>(list);
    }
}
