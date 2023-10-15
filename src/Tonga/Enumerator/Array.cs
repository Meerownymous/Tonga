using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tonga.Enumerator
{
    /// <summary>
    /// Enumerator for an array.
    /// </summary>
    public sealed class Array<T> : IEnumerator<T>
    {
        private readonly int[] cursor;
        private readonly T[] items;

        /// <summary>
        /// Enumerator for an array.
        /// </summary>
        public Array(params T[] items)
        {
            this.cursor = new int[1] { -1 };
            this.items = items;
        }

        public T Current
        {
            get
            {
                if (this.cursor[0] < 0) throw new InvalidOperationException("Move the enumerator first.");
                Debug.WriteLine("- "+ this.items[this.cursor[0]]);
                return this.items[this.cursor[0]];
            }
        }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            bool hasItem = this.cursor[0] < this.items.Length - 1;
            if(hasItem)
            {
                this.cursor[0]++;
            }
            return hasItem;
        }

        public void Reset()
        {
            this.cursor[0] = -1;
        }
    }
}

