using System;
using System.Collections.Generic;
using Tonga.Collection;

namespace Tonga.Map;

    /// <summary>
    /// A map which is empty.
    /// </summary>
    public sealed class Empty<Key, Value> : IMap<Key, Value>
    {
        /// <summary>
        /// A map which is empty.
        /// </summary>
        public Empty()
        { }

        public Value this[Key key] => throw new InvalidOperationException("Map is empty.");
        public ICollection<Key> Keys() => new AsCollection<Key>();
        public Func<Value> Lazy(Key key) => throw new InvalidOperationException("Map is empty.");
        public IEnumerable<IPair<Key, Value>> Pairs() => new AsCollection<IPair<Key, Value>>();
        public IMap<Key, Value> With(IPair<Key, Value> pair) => pair.AsMap();
    }

