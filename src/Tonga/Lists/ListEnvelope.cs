

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
    public abstract class ListEnvelope<T> : IList<T>
    {
        private readonly InvalidOperationException readOnlyError;
        private readonly IList<T> origin;

        /// <summary>
        /// List envelope. Not sticky, will enumerate on each access.
        /// </summary>
        /// <param name="live">value is handled live or sticky</param>
        public ListEnvelope(IList<T> list)
        {
            this.origin = list;
            this.readOnlyError = new InvalidOperationException("The list is readonly.");
        }

        /// <summary>
        /// Access items.
        /// </summary>
        public T this[int index]
        {
            get => this.origin[index];
            set => throw this.readOnlyError;
        }

        /// <summary>
        /// Count elements.
        /// </summary>
        public int Count
        {
            get => this.origin.Count;
        }

        /// <summary>
        /// Test if containing the given item.
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>true if item is found</returns>
        public bool Contains(T item) => this.origin.Contains(item);

        /// <summary>
        /// Copy to a target array.
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">write start index</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.origin.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Enumerator for this list.
        /// </summary>
        public IEnumerator<T> GetEnumerator() => this.origin.GetEnumerator();

        /// <summary>
        /// Enumerator for this list.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Index of given item.
        /// </summary>
        public int IndexOf(T item) => this.origin.IndexOf(item);

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
        public void Clear()
        {
            throw this.readOnlyError;
        }

        /// <summary>
        /// Unsupported.
        /// </summary>
        public void Insert(int index, T item)
        {
            throw this.readOnlyError;
        }

        /// <summary>
        /// Unsupported.
        /// </summary>
        public bool Remove(T item)
        {
            throw readOnlyError;
        }

        /// <summary>
        /// Unsupported.
        /// </summary>
        public void RemoveAt(int index)
        {
            throw this.readOnlyError;
        }
    }
}
