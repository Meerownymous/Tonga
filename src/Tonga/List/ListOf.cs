using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.List
{
    /// <summary>
    /// Makes a readonly list.
    /// </summary>
    /// <typeparam name="T">type of items</typeparam>
    public sealed class ListOf<T> : IList<T>
    {
        private readonly IEnumerable<T> origin;
        private readonly InvalidOperationException readOnlyError;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="array">source array</param>
        public ListOf(params T[] items) : this(new EnumerableOf<T>(items))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerable</param>
        public ListOf(IEnumerable<T> src)
        {
            this.origin = src;
            this.readOnlyError = new InvalidOperationException("The list is readonly.");
        }

        public T this[int index]
        {
            get
            {
                T result = default(T);
                var found = false;
                var current = -1;
                foreach(var item in this.origin)
                {
                    current++;
                    if(current == index)
                    {
                        found = true;
                        result = item;
                        break;
                    }
                }
                if (!found)
                    throw new ArgumentException($"Cannot get item at {index} - only {current + 1} are in the list.");
                return result;
            }
            set => throw this.readOnlyError;
        }

        public int Count
        {
            get
            {
                return new LengthOf(this.origin).Value();
            }
        }

        public bool IsReadOnly => true;

        public void Add(T item)
        {
            throw this.readOnlyError;
        }

        public void Clear()
        {
            throw this.readOnlyError;
        }

        public bool Contains(T item)
        {
            var found = false;
            var current = -1;
            foreach (var candidate in this.origin)
            {
                current++;
                if (item.Equals(candidate))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach(var item in this.origin)
            {
                array[arrayIndex++] = item;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.origin.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            var found = false;
            var index = -1;
            foreach (var candidate in this.origin)
            {
                index++;
                if (item.Equals(candidate))
                {
                    found = true;
                    break;
                }
            }
            return found ? index : -1;
        }

        public void Insert(int index, T item)
        {
            throw this.readOnlyError;
        }

        public bool Remove(T item)
        {
            throw this.readOnlyError;
        }

        public void RemoveAt(int index)
        {
            throw this.readOnlyError;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public static class ListOf
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerable</param>
        public static IList<string> Pipe(params string[] src)
            => new ListOf<string>(src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerable</param>
        public static IList<T> Pipe<T>(IEnumerable<T> src)
            => new ListOf<T>(src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerable</param>
        public static IList<T> Sticky<T>(IEnumerable<T> src)
            => List.Sticky.New(new ListOf<T>(src));
    }
}
