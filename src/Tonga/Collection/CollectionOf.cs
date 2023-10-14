

using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Collection
{
    /// <summary>
    /// Collection out of other things. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class CollectionOf<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// A collection from an array
        /// </summary>
        /// <param name="array"></param>
        public CollectionOf(params T[] array) : this(new LiveMany<T>(array))
        { }

        /// <summary>
        /// A collection from an <see cref="IEnumerator{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public CollectionOf(IEnumerator<T> src) : this(new ManyOf<T>(src))
        { }

        /// <summary>
        /// Makes a collection from an <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public CollectionOf(IEnumerable<T> src) : base(
            () =>
            {
                ICollection<T> list = new LinkedList<T>();
                foreach (T item in src)
                {
                    list.Add(item);
                }
                return list;
            },
            false
        )
        { }
    }

    public static class CollectionOf
    {
        public static ICollection<T> New<T>(params T[] array) => new CollectionOf<T>(array);

        public static ICollection<T> New<T>(IEnumerator<T> src) => new CollectionOf<T>(src);

        public static ICollection<T> New<T>(IEnumerable<T> src) => new CollectionOf<T>(src);

    }
}
