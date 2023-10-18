

using System;
using System.Collections;
using System.Collections.Generic;

using Tonga.Scalar;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

namespace Tonga.List
{
    /// <summary>
    /// List envelope. Can make a readonly list from a scalar.
    /// </summary>
    public abstract class ListEnvelopeOriginal<T> : IList<T>
    {
        private readonly InvalidOperationException readOnlyError;
        private readonly Func<IEnumerator<T>> origin;
        private readonly bool live;
        private readonly ScalarOf<IList<T>> fixedList;

        /// <summary>
        /// List envelope. Can make a readonly list from a scalar.
        /// </summary>
        /// <param name="live">value is handled live or sticky</param>
        public ListEnvelopeOriginal(IScalar<IList<T>> lst, bool live) : this(() =>
            lst.Value().GetEnumerator(),
            live
        )
        { }

        /// <summary>
        /// List envelope. Can make a readonly list from a scalar.
        /// </summary>
        /// <param name="live">value is handled live or sticky</param>
        public ListEnvelopeOriginal(IScalar<IEnumerable<T>> lst, bool live) : this(() =>
            lst.Value().GetEnumerator(),
            live
        )
        { }

        /// <summary>
        /// List envelope. Can make a readonly list from a scalar.
        /// </summary>
        /// <param name="live">value is handled live or sticky</param>
        public ListEnvelopeOriginal(Func<IList<T>> lst, bool live) : this(() =>
            lst().GetEnumerator(),
            live
        )
        { }

        /// <summary>
        /// List envelope. Can make a readonly list from a scalar.
        /// </summary>
        /// <param name="live">value is handled live or sticky</param>
        public ListEnvelopeOriginal(Func<IEnumerator<T>> enumerator, bool live)
        {
            this.live = live;
            this.origin = enumerator;
            this.fixedList =
                new ScalarOf<IList<T>>(() =>
                {
                    var result = new List<T>();
                    var enm = enumerator();
                    while (enm.MoveNext())
                    {
                        result.Add(enm.Current);
                    }
                    return result;
                });
            this.readOnlyError = new InvalidOperationException("The list is readonly.");
        }

        /// <summary>
        /// Access items.
        /// </summary>
        public T this[int index]
        {
            get
            {
                T result;
                if (this.live)
                {
                    if (index < 0)
                    {
                        throw new ArgumentOutOfRangeException($"Index of item must be > 0 but is {index}");
                    }
                    var enumerator = this.origin();
                    var idx = -1;
                    while (index >= 0 && idx < index && enumerator.MoveNext())
                    {
                        idx++;
                    }
                    if (idx == index)
                    {
                        result = enumerator.Current;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException($"Cannot get item at index {index} from list because it has only {idx} items.");
                    }
                }
                else
                {
                    result = this.fixedList.Value()[index];
                }
                return result;
            }
            set
            {
                throw this.readOnlyError;
            }
        }

        /// <summary>
        /// Count elements.
        /// </summary>
        public int Count
        {
            get
            {
                int count = 0;
                if (this.live)
                {
                    var enumerator = this.origin();
                    while (enumerator.MoveNext())
                    {
                        count++;
                    }
                }
                else
                {
                    count = this.fixedList.Value().Count;
                }
                return count;
            }
        }

        /// <summary>
        /// Test if containing the given item.
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>true if item is found</returns>
        public bool Contains(T item)
        {
            bool result = false;
            if (this.live)
            {
                var enumerator = this.origin();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.Equals(item))
                    {
                        result = true;
                        break;
                    }
                }
            }
            else
            {
                result = this.fixedList.Value().Contains(item);
            }
            return result;
        }

        /// <summary>
        /// Copy to a target array.
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">write start index</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            int idx = 0;
            if (this.live)
            {
                var enumerator = this.origin();
                while (enumerator.MoveNext())
                {
                    array[arrayIndex + idx] = enumerator.Current;
                    idx++;
                }
            }
            else
            {
                var lst = this.fixedList.Value();
                while (idx < lst.Count)
                {
                    array[arrayIndex + idx] = lst[idx];
                    idx++;
                }
            }
        }

        /// <summary>
        /// Enumerator for this list.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return this.live ? this.origin() : this.fixedList.Value().GetEnumerator();
        }

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
        public int IndexOf(T item)
        {
            var result = -1;
            if (this.live)
            {
                var enumerator = this.origin();
                var idx = -1;
                while (enumerator.MoveNext())
                {
                    idx++;
                    if (enumerator.Current.Equals(item))
                    {
                        result = idx;
                    }
                }
            }
            else
            {
                return this.fixedList.Value().IndexOf(item);
            }
            return result;
        }

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
