using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerator
{
    /// <summary>
    /// Enumerator for an array.
    /// </summary>
    public sealed class Array<T> : IEnumerator<T>
    {
        private int cursor;
        private readonly T[] items;

        /// <summary>
        /// Enumerator for an array.
        /// </summary>
        public Array(params T[] items)
        {
            this.cursor = -1;
            this.items = items;
        }

        public T Current
        {
            get
            {
                if (this.cursor < 0) throw new InvalidOperationException("Move the enumerator first.");
                return this.items[this.cursor];
            }
        }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            bool hasItem = this.cursor < this.items.Length - 1;
            if(hasItem)
            {
                this.cursor++;
            }
            return hasItem;
        }

        public void Reset()
        {
            this.cursor = -1;
        }
    }
}

