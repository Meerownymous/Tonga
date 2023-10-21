using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Enumeration of a single item.
    /// </summary>
    public class Single<T> : IEnumerable<T>
    {
        private readonly T item;

        /// <summary>
        /// Enumeration of a single item.
        /// </summary>
        public Single(T item)
        {
            this.item = item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield return this.item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public static class Single
    {
        public static Single<T> From<T>(T item) => new Single<T>(item);
    }
}

