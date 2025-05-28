using System;
using System.Collections.Generic;

namespace Tonga.Map
{
    /// <summary>
    /// A map whose keys and values are sticky per value once retrieved for the first time time.
    /// </summary>
    public sealed class Compiled<Key, Value>(Func<IMap<Key, Value>> source) : IMap<Key, Value>
    {
        private readonly Dictionary<Key, IPair<Key, Value>> memory = new();
        private bool allCopied;

        public Compiled(IMap<Key, Value> source) : this(() => source)
        { }

        public Value this[Key key]
        {
            get
            {
                if (!allCopied)
                {
                    CopyAll(source(), this.memory);
                }
                return this.memory[key].Value();
            }
        }

        public ICollection<Key> Keys()
        {
            if (!this.allCopied)
            {
                CopyAll(source(), this.memory);
                this.allCopied = true;
            }

            return this.memory.Keys;
        }

        public ICollection<IPair<Key, Value>> Pairs()
        {
            if (!this.allCopied)
            {
                CopyAll(source(), this.memory);
                this.allCopied = true;
            }
            return this.memory.Values;
        }

        private static Value MemorizedValue(Key key, IMap<Key,Value> source, IDictionary<Key,IPair<Key,Value>> memory)
        {
            if(!memory.ContainsKey(key) && source.Keys().Contains(key))
            {
                memory[key] = (key, source[key]).AsPair();
            }
            return memory[key].Value();
        }

        private static void CopyAll(IMap<Key, Value> source, IDictionary<Key, IPair<Key, Value>> target)
        {
            foreach (var key in source.Keys())
            {
                if (!target.ContainsKey(key))
                    target.Add(key, (key, source[key]).AsPair());
            }
        }

        public Func<Value> Lazy(Key key)
        {
            if (!allCopied)
            {
                CopyAll(source(), this.memory);
            }
            return () => this.memory[key].Value();
        }

        IEnumerable<IPair<Key, Value>> IMap<Key, Value>.Pairs()
        {
            if (!allCopied)
            {
                CopyAll(source(), this.memory);
            }

            return this.memory.Values;
        }

        public IMap<Key, Value> With(IPair<Key, Value> pair)
        {
            var key = pair.Key();
            var value = pair.Value();
            this.memory[key] = (key, value).AsPair();
            return this;
        }
    }

    public static partial class MapSmarts
    {
        /// <summary>
        /// A map whose keys and values are sticky per value once retrieved for the first time time.
        /// </summary>
        public static IMap<Key,Value> AsCompiled<Key, Value>(IMap<Key, Value> origin) =>
            new Compiled<Key, Value>(origin);

        /// <summary>
        /// A map whose keys and values are sticky per value once retrieved for the first time time.
        /// </summary>
        public static IMap<Key, Value> AsCompiled<Key, Value>(Func<IMap<Key, Value>> origin) =>
            new Compiled<Key, Value>(origin);
    }
}
