using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Tonga.Scalar;

namespace Tonga.Map
{
    /// <summary>
    /// A map whose keys and values are sticky per value once retrieved for the first time time.
    /// </summary>
    public sealed class Sticky<Key, Value> : IDictionary<Key, Value>
    {
        private readonly Lazy<IDictionary<Key, Value>> source;
        private readonly IDictionary<Key, Value> memory;
        private readonly InvalidOperationException rejectWriteException =
            new InvalidOperationException("Writing is not supported, it's a read-only map");

        /// <summary>
        /// A map whose keys and values are sticky per value once retrieved for the first time time.
        /// </summary>
        public Sticky(IDictionary<Key, Value> source) : this(() => source)
        { }

        /// <summary>
        /// A map whose keys and values are sticky per value once retrieved for the first time time.
        /// </summary>
        public Sticky(Func<IDictionary<Key, Value>> source)
        {
            this.source = new Lazy<IDictionary<Key, Value>>(source);
            this.memory = new Dictionary<Key, Value>();
        }

        public Value this[Key key]
        {
            get => MemorizedValue(key, this.source.Value, this.memory);
            set => throw this.rejectWriteException;
        }

        public ICollection<Key> Keys => this.source.Value.Keys;

        public ICollection<Value> Values
        {
            get
            {
                CopyAll(this.source.Value, this.memory);
                return this.memory.Values;
            }
        }

        public int Count => this.source.Value.Count;

        public bool IsReadOnly => true;

        public void Add(Key key, Value value)
        {
            throw this.rejectWriteException;
        }

        public void Add(KeyValuePair<Key, Value> item)
        {
            throw this.rejectWriteException;
        }

        public void Clear()
        {
            throw this.rejectWriteException;
        }

        public bool Contains(KeyValuePair<Key, Value> item)
        {
            _ = MemorizedValue(item.Key, this.source.Value, this.memory);
            return this.memory.Contains(item);
        }

        public bool ContainsKey(Key key)
        {
            return this.source.Value.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
            CopyAll(this.source.Value, this.memory);
            this.memory.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator() => this.memory.GetEnumerator();

        public bool Remove(Key key)
        {
            throw this.rejectWriteException;
        }

        public bool Remove(KeyValuePair<Key, Value> item)
        {
            throw this.rejectWriteException;
        }

        public bool TryGetValue(Key key, [MaybeNullWhen(false)] out Value value)
        {
            Value result;
            bool success;
            if (!IsMemorized(key, this.source.Value))
            {
                success = this.source.Value.TryGetValue(key, out result);
                Memorize(key, result, this.memory);
            }
            else
            {
                success = true;
                result = MemorizedValue(key, this.source.Value, this.memory);
            }
            value = result;
            return success;
            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private static void Memorize(Key key, Value value, IDictionary<Key, Value> memory)
        {
            memory[key] = value;
        }

        private static bool IsMemorized(Key key, IDictionary<Key, Value> memory)
        {
            return memory.ContainsKey(key);
        }

        private static Value MemorizedValue(Key key, IDictionary<Key,Value> source, IDictionary<Key,Value> memory)
        {
            if(!memory.ContainsKey(key) && source.ContainsKey(key))
            {
                memory[key] = source[key];
            }
            return memory[key];
        }

        public static void CopyAll(IDictionary<Key, Value> source, IDictionary<Key, Value> target)
        {
            if (source.Count != target.Count)
            {
                foreach (var key in source.Keys)
                {
                    if (!target.ContainsKey(key))
                        target.Add(key, source[key]);
                }
            }
        }
    }

    public static class Sticky
    {
        /// <summary>
        /// A map whose keys and values are sticky per value once retrieved for the first time time.
        /// </summary>
        public static Sticky<Key, Value> _<Key, Value>(IDictionary<Key, Value> origin) =>
            new Sticky<Key, Value>(() => origin);

        /// <summary>
        /// A map whose keys and values are sticky per value once retrieved for the first time time.
        /// </summary>
        public static Sticky<Key, Value> _<Key, Value>(Func<IDictionary<Key, Value>> origin) =>
            new Sticky<Key, Value>(origin);
    }
}