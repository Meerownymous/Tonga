using System;
using System.Collections.Generic;

namespace Tonga.Map
{
    /// <summary>
    /// Simplified map building.
    /// </summary>
    public abstract class MapEnvelope<Key, Value> : IMap<Key, Value>
    {
        private readonly IMap<Key, Value> origin;

        /// <summary>
        /// Simplified map building.
        /// </summary>
        public MapEnvelope(IMap<Key, Value> origin)
        {
            this.origin = origin;
        }

        public Value this[Key key] => Origin()[key];

        public ICollection<Key> Keys() => Origin().Keys();

        public Func<Value> Lazy(Key key) => this.origin.Lazy(key);

        public IEnumerable<IPair<Key, Value>> Pairs() => this.Origin().Pairs();

        public IMap<Key, Value> With(IPair<Key, Value> pair) => this.Origin().With(pair);

        private IMap<Key, Value> Origin() => this.origin;
    }
}
