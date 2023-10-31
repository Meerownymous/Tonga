using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Tonga.Map
{
    /// <summary>
    /// Map as read/write dictionary.
    /// Lazy pairs are finalized and copied on first access.
    /// </summary>
    public sealed class AsDictionary<Key, Value> : IDictionary<Key, Value>
    {
        private readonly Lazy<IDictionary<Key, Value>> memory;

        /// <summary>
        /// Map as read/write dictionary.
        /// Lazy pairs are finalized and copied on first access.
        /// </summary>
        public AsDictionary(IMap<Key, Value> origin)
        {
            this.memory =
                new Lazy<IDictionary<Key, Value>>(() =>
                {
                    var copy = new Dictionary<Key, Value>();
                    foreach(var pair in origin.Pairs())
                    {
                        copy[pair.Key()] = pair.Value();
                    }
                    return copy;
                });
        }

        public Value this[Key key]
        {
            get => this.memory.Value[key];
            set => this.memory.Value[key] = value;
        }

        public ICollection<Key> Keys => this.memory.Value.Keys;

        public ICollection<Value> Values => this.memory.Value.Values;

        public int Count => this.memory.Value.Count;

        public bool IsReadOnly => this.memory.Value.IsReadOnly;

        public void Add(Key key, Value value)
        {
            this.memory.Value.Add(key, value);
        }

        public void Add(KeyValuePair<Key, Value> item)
        {
            this.memory.Value.Add(item);
        }

        public void Clear()
        {
            this.memory.Value.Clear();
        }

        public bool Contains(KeyValuePair<Key, Value> item)
        {
            return this.memory.Value.Contains(item);
        }

        public bool ContainsKey(Key key)
        {
            return this.memory.Value.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
            this.memory.Value.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            return this.memory.Value.GetEnumerator();
        }

        public bool Remove(Key key)
        {
            return this.memory.Value.Remove(key);
        }

        public bool Remove(KeyValuePair<Key, Value> item)
        {
            return this.memory.Value.Remove(item);
        }

        public bool TryGetValue(Key key, [MaybeNullWhen(false)] out Value value)
        {
            return this.memory.Value.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.memory.Value.GetEnumerator();
        }
    }

    /// <summary>
    /// Map as read/write dictionary.
    /// Lazy pairs are finalized and copied on first access.
    /// </summary>
    public static class AsDictionary
    {
        /// <summary>
        /// Map as read/write dictionary.
        /// Lazy pairs are finalized and copied on first access.
        /// </summary>
        public static AsDictionary<Key, Value> _<Key, Value>(IMap<Key, Value> map) =>
            new AsDictionary<Key, Value>(map);
    }
}

