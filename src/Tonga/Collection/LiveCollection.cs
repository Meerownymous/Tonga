

using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Collection
{
    /// <summary>
    /// Envelope for collections. 
    /// It accepts a scalar and makes readonly Collection from it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class LiveCollection<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// Makes a collection from an array
        /// </summary>
        /// <param name="array"></param>
        public LiveCollection(params T[] items) : this(new EnumerableOf<T>(items))
        { }

        /// <summary>
        /// Makes a collection from an <see cref="IEnumerator{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public LiveCollection(IEnumerator<T> src) : this(Enumerable.EnumerableOf.Pipe(src))
        { }

        /// <summary>
        /// Makes a collection from an <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public LiveCollection(IEnumerable<T> src) : base(
            () => src.GetEnumerator(),
            true
        )
        { }
    }

    public static class LiveCollection
    {
        public static ICollection<T> New<T>(IEnumerator<T> src) => new LiveCollection<T>(src);
        public static ICollection<T> New<T>(IEnumerable<T> src) => new LiveCollection<T>(src);
    }
}
