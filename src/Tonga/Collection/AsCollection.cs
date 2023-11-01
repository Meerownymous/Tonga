

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tonga.Enumerable;

namespace Tonga.Collection
{
    /// <summary>
    /// Collection out of other things. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AsCollection<T> : ICollection<T>
    {
        private static InvalidOperationException readOnlyException =
            new InvalidOperationException("Collection is readonly.");
        private readonly Func<ICollection<T>> items;

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
        public AsCollection(Func<IEnumerator<T>> src) : this(AsEnumerable._(src))
        { }

        /// <summary>
        /// A collection from an <see cref="IEnumerator{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public AsCollection(IEnumerator<T> src) : this(AsEnumerable._(src))
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

        /// <summary>
        /// A collection out of an <see cref="ICollection{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public AsCollection(Func<ICollection<T>> src)
        {
            this.items = src;
        }

        public int Count => this.items().Count;

        public bool IsReadOnly => true;

        public void Add(T item)
        {
            throw readOnlyException;
        }

        public void Clear()
        {
            throw readOnlyException;
        }

        public bool Contains(T item)
        {
            return this.items().Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.items().CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.items().GetEnumerator();
        }

        public bool Remove(T item)
        {
            throw readOnlyException;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items().GetEnumerator();
        }
    }

    public static class AsCollection
    {
        /// <summary>
        /// A collection out of an <see cref="IEnumerable{T}"/>
        /// </summary>
        public static ICollection<T> _<T>(Func<IEnumerator<T>> src) => new AsCollection<T>(src);

        /// <summary>
        /// A collection out of an <see cref="IEnumerable{T}"/>
        /// </summary>
        public static ICollection<T> _<T>(IEnumerator<T> src) => new AsCollection<T>(src);

        /// <summary>
        /// A collection out of an <see cref="IEnumerable{T}"/>
        /// </summary>
        public static ICollection<T> _<T>(Func<IEnumerable<T>> src) => new AsCollection<T>(src);

        /// <summary>
        /// A collection out of an <see cref="IEnumerable{T}"/>
        /// </summary>
        public static ICollection<T> _<T>(IEnumerable<T> src) => new AsCollection<T>(src);

        /// <summary>
        /// A collection out of an <see cref="ICollection{T}"/>
        /// </summary>
        public static ICollection<T> _<T>(Func<ICollection<T>> src) => new AsCollection<T>(src);

    }
}
