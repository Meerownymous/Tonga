

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Tonga.Enumerable;

namespace Tonga.Collection
{
    /// <summary>
    /// Collection out of other things.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AsCollection<T>(Func<ICollection<T>> origin) : ICollection<T>
    {
        private static readonly InvalidOperationException readOnlyException = new("Collection is readonly.");

        /// <summary>
        /// A collection from an array
        /// </summary>
        public AsCollection(params T[] more) : this(more.AsEnumerable())
        { }

        /// <summary>
        /// A collection from an <see cref="IEnumerator{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public AsCollection(Func<IEnumerator<T>> src) : this(src.AsEnumerable())
        { }

        /// <summary>
        /// A collection from an <see cref="IEnumerator{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public AsCollection(IEnumerator<T> src) : this(src.AsEnumerable())
        { }

        /// <summary>
        /// A collection out of an <see cref="IEnumerable{T}"/>
        /// </summary>
        public AsCollection(IEnumerable<T> src) : this(() => src)
        { }

        /// <summary>
        /// A collection out of an <see cref="IEnumerable{T}"/>
        /// </summary>
        public AsCollection(Func<IEnumerable<T>> src) : this(() =>
            {
                var col = new Collection<T>();
                foreach(var item in src())
                {
                    col.Add(item);
                }
                return col;
            }
        )
        { }

        public int Count => origin().Count;
        public bool IsReadOnly => true;
        public void Add(T item) => throw readOnlyException;
        public void Clear() => throw readOnlyException;
        public bool Contains(T item) => origin().Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => origin().CopyTo(array, arrayIndex);
        public IEnumerator<T> GetEnumerator() => origin().GetEnumerator();
        public bool Remove(T item) => throw readOnlyException;
        IEnumerator IEnumerable.GetEnumerator() => origin().GetEnumerator();
    }

    public static class CollectionSmarts
    {
        public static ICollection<TItem> AsCollection<TItem>(this ICollection<TItem> item) =>
            new AsCollection<TItem>(item);

        public static ICollection<TItem> AsCollection<TItem>(this TItem[] items) =>
            new AsCollection<TItem>(items);

        public static ICollection<TItem> AsCollection<TItem>(this Func<IEnumerator<TItem>> items) =>
            new AsCollection<TItem>(items);

        public static ICollection<TItem> AsCollection<TItem>(this IEnumerator<TItem> items) =>
            new AsCollection<TItem>(items);

        public static ICollection<TItem> AsCollection<TItem>(this IEnumerable<TItem> items) =>
            new AsCollection<TItem>(items);

        public static ICollection<TItem> AsCollection<TItem>(this System.Func<IEnumerable<TItem>> items) =>
            new AsCollection<TItem>(items);
    }
}
