using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Map
{
    /// <summary>
    /// Maps a deeper level of a key to a value.
    /// </summary>
    public sealed class DeepMap<Surface, Deep, Value> : IMap<Surface, Value>
    {
        private readonly Func<Surface, Deep> digDown;
        private readonly IMap<Deep, Value> deep;
        private readonly IMap<Surface, Value> shallow;

        /// <summary>
        /// Maps a deeper level of a key to a value.
        /// </summary>
        public DeepMap(Func<Surface, Deep> digDown, IMap<Surface, Value> origin)
        {
            this.digDown = digDown;
            this.deep =
                AsMap._(() =>
                    Mapped._(
                        pair => AsPair._(digDown(pair.Key()), pair.Value()),
                        origin.Pairs()
                    )
                );
            this.shallow = origin;
        }

        public Value this[Surface key] => this.deep[this.digDown(key)];

        public ICollection<Surface> Keys()
        {
            return this.shallow.Keys();
        }

        public Func<Value> Lazy(Surface key)
        {
            return () => this[key];
        }

        public IEnumerable<IPair<Surface, Value>> Pairs()
        {
            return this.shallow.Pairs();
        }

        public IMap<Surface, Value> With(IPair<Surface, Value> pair)
        {
            return DeepMap._(this.digDown, this.shallow.With(pair));
        }
    }

    /// <summary>
    /// Maps a deeper level of a key to a value.
    /// </summary>
    public static class DeepMap
    {
        /// <summary>
        /// Maps a deeper level of a key to a value.
        /// </summary>
        public static DeepMap<Surface, Deep, Value> _<Surface, Deep, Value>(
            Func<Surface, Deep> digDown, IMap<Surface, Value> origin
        ) => new DeepMap<Surface, Deep, Value>(digDown, origin);
    }

}

