using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Tonga.Map
{
    /// <summary>
    /// A map whose keys and values are sticky per value once retrieved for the first time time.
    /// </summary>
    public sealed class Sticky<Key, Value> : IMap<Key, Value>
    {
        private readonly IMap<Key, Value> source;
        private readonly IDictionary<Key, IPair<Key, Value>> memory;
        private bool allCopied = false;

        /// <summary>
        /// A map whose keys and values are sticky per value once retrieved for the first time time.
        /// </summary>
        public Sticky(Func<IMap<Key, Value>> source)
        {
            this.source = AsMap._(() => source().Pairs());
            this.memory = new Dictionary<Key, IPair<Key,Value>>();
        }

        public Value this[Key key]
        {
            get => MemorizedValue(key, this.source, this.memory);
        }

        public ICollection<Key> Keys() => this.source.Keys();

        public ICollection<IPair<Key, Value>> Pairs()
        {
            if (!this.allCopied)
            {
                CopyAll(this.source, this.memory);
                this.allCopied = true;
            }
            return this.memory.Values;
        }

        private static Value MemorizedValue(Key key, IMap<Key,Value> source, IDictionary<Key,IPair<Key,Value>> memory)
        {
            if(!memory.ContainsKey(key) && source.Keys().Contains(key))
            {
                memory[key] = AsPair._(key, source[key]);
            }
            return memory[key].Value();
        }

        private static void CopyAll(IMap<Key, Value> source, IDictionary<Key, IPair<Key, Value>> target)
        {
            foreach (var key in source.Keys())
            {
                if (!target.ContainsKey(key))
                    target.Add(key, AsPair._(key, source[key]));
            }
        }

        public Func<Value> Lazy(Key key) => () => MemorizedValue(key, this.source, this.memory);

        IEnumerable<IPair<Key, Value>> IMap<Key, Value>.Pairs()
        {
            CopyAll(this.source, this.memory);
            return this.memory.Values;
        }

        public IMap<Key, Value> With(IPair<Key, Value> pair)
        {
            var key = pair.Key();
            var value = pair.Value();
            this.memory[key] = AsPair._(key, value);
            return this;
        }
    }

    public static class Sticky
    {
        /// <summary>
        /// A map whose keys and values are sticky per value once retrieved for the first time time.
        /// </summary>
        public static Sticky<Key, Value> _<Key, Value>(IMap<Key, Value> origin) =>
            new Sticky<Key, Value>(() => origin);

        /// <summary>
        /// A map whose keys and values are sticky per value once retrieved for the first time time.
        /// </summary>
        public static Sticky<Key, Value> _<Key, Value>(Func<IMap<Key, Value>> origin) =>
            new Sticky<Key, Value>(origin);
    }
}