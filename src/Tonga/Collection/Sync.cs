

using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.Collection
{
    /// <summary>
    /// A collection which is threadsafe.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Sync<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">items to make collection from</param>
        public Sync(params T[] items) : this(new LiveCollection<T>(items))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="col">Collection to sync</param>
        public Sync(ICollection<T> col) : this(col, col)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="syncRoot">root object to sync</param>
        /// <param name="col"></param>
        public Sync(object syncRoot, ICollection<T> col) : base(
            new Scalar.Sync<ICollection<T>>(
                new Live<ICollection<T>>(() =>
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
                }
                )
            ),
            false
        )
        { }
    }

    public static class Sync
    {
        public static ICollection<T> New<T>(params T[] items) => new Sync<T>(items);
    }
}
