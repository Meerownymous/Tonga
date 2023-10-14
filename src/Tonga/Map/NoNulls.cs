

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Map
{
    /// <summary>
    /// A decorator of map that tolerates no NULLs.
    /// </summary>
    /// <typeparam name="Key">type of key</typeparam>
    /// <typeparam name="Value">type of value</typeparam>
    public sealed class NoNulls<Key, Value> : IDictionary<Key, Value>
    {
        private readonly IDictionary<Key, Value> map;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map">IDictionary</param>
        public NoNulls(IDictionary<Key, Value> map)
        {
            this.map = map;
        }

        /// <summary>
        /// Access a value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Value this[Key key]
        {
            get
            {
                if(key == null) throw new ArgumentException("key can't be null.");
                var value = map[key];
                if (value == null) throw new ArgumentException($"Value returned by [{key}] is null.");
                return map[key];
            }
            set
            {
                if (key == null) throw new ArgumentException("key can't be null.");
                if (value == null) throw new ArgumentException($"Value returned by [{key}] is null.");
                map[key] = value;
            }
        }

        /// <summary>
        /// Access all keys
        /// </summary>
        public ICollection<Key> Keys => map.Keys;

        /// <summary>
        /// Access all values
        /// </summary>
        public ICollection<Value> Values => map.Values;

        /// <summary>
        /// Count entries
        /// </summary>
        public int Count => map.Count;

        /// <summary>
        /// Gets a value indicating whether the map is read-only.
        /// </summary>
        public bool IsReadOnly => map.IsReadOnly;

        /// <summary>
        /// Adds an element with the provided key and value to the map
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(Key key, Value value)
        {
            if (key == null) throw new ArgumentException("key can't be null.");
            if (value == null) throw new ArgumentException($"Value returned by [{key}] is null.");
            map.Add(key, value);
        }

        /// <summary>
        /// Adds an element with the provided entry to the map
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<Key, Value> item)
        {
            if (item.Key == null) throw new ArgumentException("key can't be null.");
            if (item.Value == null) throw new ArgumentException($"Value returned by [{item.Key}] is null.");
            map.Add(item);
        }

        /// <summary>
        /// Removes all elements from the map
        /// </summary>
        public void Clear()
        {
            map.Clear();
        }

        /// <summary>
        /// Test if map contains entry
        /// </summary>
        /// <param name="item">item to check</param>
        /// <returns>true if it contains the entry</returns>
        public bool Contains(KeyValuePair<Key, Value> item)
        {
            if (item.Key == null) throw new ArgumentException("key can't be null.");
            if (item.Value == null) throw new ArgumentException($"Value returned by [{item.Key}] is null.");
            return map.Contains(item);
        }

        /// <summary>
        /// Test if map contains key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>true if it contains the key</returns>
        public bool ContainsKey(Key key)
        {
            if (key == null) throw new ArgumentException("key can't be null.");
            return map.ContainsKey(key);
        }

        /// <summary>
        /// Copy this to an array
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">index to start</param>
        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
            map.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// The enumerator
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            return map.GetEnumerator();
        }

        /// <summary>
        ///  Removes the entry with the specified key from the map
        /// </summary>
        public bool Remove(Key key)
        {
            return map.Remove(key);
        }

        /// <summary>
        /// Removes the first occurrence of a specific entry from the map
        /// </summary>
        public bool Remove(KeyValuePair<Key, Value> item)
        {
            return map.Remove(item);
        }

        /// <summary>
        /// Tries to get value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">target to store value</param>
        /// <returns>true if success</returns>
        public bool TryGetValue(Key key, out Value value)
        {
            if (key == null) throw new ArgumentException("key can't be null.");
            var result = map.TryGetValue(key, out value);
            if (value == null) throw new ArgumentException($"Value returned by [{key}] is null.");
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return map.GetEnumerator();
        }
    }

    public static class NoNulls
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map">IDictionary</param>
        public static IDictionary<Key, Value> New<Key, Value>(IDictionary<Key, Value> map)
            => new NoNulls<Key, Value>(map);
    }
}
