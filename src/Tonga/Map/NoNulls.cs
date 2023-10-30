

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
    public sealed class NoNulls<Key, Value> : IMap<Key, Value>
    {
        private readonly IMap<Key, Value> map;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map">IDictionary</param>
        public NoNulls(IMap<Key, Value> map)
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
        }

        /// <summary>
        /// Access all keys
        /// </summary>
        public ICollection<Key> Keys() => map.Keys();

        public Func<Value> Lazy(Key key)
        {
            return () => this[key];
        }

        /// <summary>
        /// Access all values
        /// </summary>
        public IEnumerable<IPair<Key,Value>> Pairs() => map.Pairs();

        public IMap<Key, Value> With(IPair<Key, Value> pair)
        {
            if (pair.Key() == null)
                throw new ArgumentException("Key of a new pair must not be null.");

            return NoNulls._(this.map.With(pair));
        }
    }

    public static class NoNulls
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map">IDictionary</param>
        public static IMap<Key, Value> _<Key, Value>(IMap<Key, Value> map)
            => new NoNulls<Key, Value>(map);
    }
}
