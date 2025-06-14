

using System;
using System.Collections;
using System.Collections.Generic;

using Tonga.Scalar;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

namespace Tonga.List
{
    /// <summary>
    /// List envelope.
    /// </summary>
    public abstract class ListEnvelope<T>(IList<T> origin) : IList<T>
    {
        private readonly InvalidOperationException readOnlyError = new("The list is readonly.");

        /// <summary>
        /// Access items.
        /// </summary>
        public T this[int index]
        {
            get => origin[index];
            set => throw this.readOnlyError;
        }

        /// <summary>
        /// Count elements.
        /// </summary>
        public int Count
        {
            get => origin.Count;
        }

        /// <summary>
        /// Test if containing the given item.
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>true if item is found</returns>
        public bool Contains(T item) => origin.Contains(item);

        /// <summary>
        /// Copy to a target array.
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">write start index</param>
        public void CopyTo(T[] array, int arrayIndex) =>
            origin.CopyTo(array, arrayIndex);

        /// <summary>
        /// Enumerator for this list.
        /// </summary>
        public IEnumerator<T> GetEnumerator() => origin.GetEnumerator();

        /// <summary>
        /// Enumerator for this list.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Index of given item.
        /// </summary>
        public int IndexOf(T item) => origin.IndexOf(item);

        /// <summary>
        /// This is a readonly collection, always true.
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// Not supported.
        /// </summary>
        public void Add(T item) { throw this.readOnlyError; }

        /// <summary>
        /// Unsupported.
        /// </summary>
        public void Clear() => throw this.readOnlyError;

        /// <summary>
        /// Unsupported.
        /// </summary>
        public void Insert(int index, T item) => throw this.readOnlyError;

        /// <summary>
        /// Unsupported.
        /// </summary>
        public bool Remove(T item) => throw readOnlyError;

        /// <summary>
        /// Unsupported.
        /// </summary>
        public void RemoveAt(int index) => throw this.readOnlyError;
    }
}
