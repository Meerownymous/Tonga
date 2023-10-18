

using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.List
{
    /// <summary>
    /// A list which is threadsafe.
    /// </summary>
    public sealed class SyncList<T> : ListEnvelopeOriginal<T>
    {
        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        public SyncList() : this(new object())
        { }

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        public SyncList(object syncRoot) : this(
            syncRoot,
            new ListOf<T>()
        )
        { }

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="items">Items to make list from</param>
        public SyncList(params T[] items) : this(
            new ListOf<T>(items)
        )
        { }

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="items">Items to make list from</param>
        public SyncList(IEnumerable<T> items) : this(
            new ListOf<T>(items)
        )
        { }

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="lst">Items to make list from</param>
        public SyncList(ICollection<T> lst) : this(
            new ListOf<T>(lst)
        )
        { }

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="lst">List to sync</param>
        public SyncList(IList<T> lst) : this(
            lst,
            lst
        )
        { }

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="syncRoot">Root object to sync</param>
        public SyncList(object syncRoot, IList<T> col) : base(
            new Sync<IEnumerable<T>>(
                new Live<IEnumerable<T>>(() =>
                {
                    lock (syncRoot)
                    {
                        var tmp = new List<T>();
                        foreach (var item in col)
                        {
                            tmp.Add(item);
                        }
                        return tmp;
                    }
                })
            ),
            false
        )
        { }
    }

    public static class SyncList
    {
        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        public static IList<T> New<T>()
            => new SyncList<T>();

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        public static IList<T> New<T>(object syncRoot)
            => new SyncList<T>(syncRoot);

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="items">Items to make list from</param>
        public static IList<T> New<T>(params T[] items)
            => new SyncList<T>(items);

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="items">Items to make list from</param>
        public static IList<T> New<T>(IEnumerable<T> items)
            => new SyncList<T>(items);

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="items">Items to make list from</param>
        public static IList<T> New<T>(IEnumerator<T> items)
            => new SyncList<T>(items);

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="lst">Items to make list from</param>
        public static IList<T> New<T>(ICollection<T> lst)
            => new SyncList<T>(lst);

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="lst">List to sync</param>
        public static IList<T> New<T>(IList<T> lst)
            => new SyncList<T>(lst);

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="syncRoot">Root object to sync</param>
        public static IList<T> New<T>(object syncRoot, IList<T> col)
            => new SyncList<T>(syncRoot, col);
    }
}
