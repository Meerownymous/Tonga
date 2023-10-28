

using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Collection
{
    /// <summary>
    /// Collection out of other things. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AsCollection<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// A collection from an array
        /// </summary>
        /// <param name="array"></param>
        public AsCollection(params T[] more) : this(AsEnumerable._(more))
        { }

        /// <summary>
        /// A collection from an <see cref="IEnumerator{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public AsCollection(IEnumerator<T> src) : this(AsEnumerable._(src))
        { }

        /// <summary>
        /// Makes a collection from an <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public AsCollection(IEnumerable<T> src) : base(
            () =>
            {
                ICollection<T> list = new LinkedList<T>();
                foreach (T item in src)
                {
                    list.Add(item);
                }
                return list;
            }
        )
        { }
    }

    public static class AsCollection
    {
        public static ICollection<T> _<T>(params T[] array) => new AsCollection<T>(array);

        public static ICollection<T> _<T>(IEnumerator<T> src) => new AsCollection<T>(src);

        public static ICollection<T> _<T>(IEnumerable<T> src) => new AsCollection<T>(src);

    }
}
