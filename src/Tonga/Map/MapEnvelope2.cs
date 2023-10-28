using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Map
{
    /// <summary>
    /// Simplified map building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class MapEnvelope2<Key, Value> : IDictionary<Key, Value>
    {
        private readonly InvalidOperationException rejectWriteExc =
            new InvalidOperationException("Writing is not supported, it's a read-only map");

        private readonly Func<IDictionary<Key, Value>> origin;

        /// <summary>
        /// Simplified map building.
        /// </summary>
        public MapEnvelope2(Func<IDictionary<Key, Value>> origin)
        {
            this.origin = origin;
        }

        public Value this[Key key]
        {
            get
            {
                try
                {
                    return Origin()[key];
                }
                catch (KeyNotFoundException)
                {
                    throw new ArgumentException("The requested key is not present in the map.");
                }
            }
            set => throw this.rejectWriteExc;
        }

        public ICollection<Key> Keys => Origin().Keys;

        public ICollection<Value> Values => Origin().Values;

        public int Count => Origin().Count;

        public bool IsReadOnly => true;

        public void Add(Key key, Value value)
        {
            throw this.rejectWriteExc;
        }

        public void Add(KeyValuePair<Key, Value> item)
        {
            throw this.rejectWriteExc;
        }

        public void Clear()
        {
            throw this.rejectWriteExc;
        }

        public bool Contains(KeyValuePair<Key, Value> item)
        {
            return Origin().Contains(item);
        }

        public bool ContainsKey(Key key)
        {
            return Origin().ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
            Origin().CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            return Origin().GetEnumerator();
        }

        public bool Remove(Key key)
        {
            throw this.rejectWriteExc;
        }

        public bool Remove(KeyValuePair<Key, Value> item)
        {
            throw this.rejectWriteExc;
        }

        public bool TryGetValue(Key key, out Value value)
        {
            return Origin().TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Origin().GetEnumerator();
        }

        private IDictionary<Key, Value> Origin() => this.origin();
    }
}
