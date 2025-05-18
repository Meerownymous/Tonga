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
    public sealed class AsList<T> : IList<T>
    {
        private readonly Func<IEnumerable<T>> origin;
        private readonly InvalidOperationException readOnlyError;

        /// <summary>
        /// ctor
        /// </summary>
        public AsList(params T[] items) : this(() => new AsEnumerable<T>(items))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerable</param>
        public AsList(IEnumerable<T> src) : this(() => src)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerable</param>
        public AsList(Func<IEnumerable<T>> src)
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
                var items = this.origin();
                foreach(var item in items)
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
                return (int)Length._(this.origin()).Value();
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
            foreach (var candidate in this.origin())
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
            foreach(var item in this.origin())
            {
                array[arrayIndex++] = item;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.origin().GetEnumerator();
        }

        public int IndexOf(T item)
        {
            var found = false;
            var index = -1;
            foreach (var candidate in this.origin())
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

    public static class AsList
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerable</param>
        public static IList<string> _(params string[] src)
            => new AsList<string>(src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerable</param>
        public static IList<T> _<T>(Func<IEnumerable<T>> src)
            => new AsList<T>(src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerable</param>
        public static IList<T> _<T>(IEnumerable<T> src)
            => new AsList<T>(src);
    }
}
