using System;
using System.Collections.Generic;
using Tonga.Collection;
using Tonga.Enumerable;

namespace Tonga.Map
{
    /// <summary>
    /// A map which is empty.
    /// </summary>
    public sealed class EmptyLookup<Key, Value> : IMap<Key, Value>
    {
        /// <summary>
        /// A map which is empty.
        /// </summary>
        public EmptyLookup()
        { }

        public Value this[Key key] => throw new InvalidOperationException("Map is empty.");

        public ICollection<Key> Keys()
        {
            return new AsCollection<Key>();
        }

        public Func<Value> Lazy(Key key)
        {
            throw new InvalidOperationException("Map is empty.");
        }

        public IEnumerable<IPair<Key, Value>> Pairs()
        {
            return new AsCollection<IPair<Key, Value>>();
        }

        public IMap<Key, Value> With(IPair<Key, Value> pair)
        {
            return AsLookup._(pair);
        }
    }
}
