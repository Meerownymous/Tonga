using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Map
{
    /// <summary>
    /// Merged map.
    /// </summary>
    public sealed class Merged<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// Merged map.
        /// </summary>
        public Merged(IPair<Key, Value> kvp, IMap<Key, Value> origin) : this(
            AsMap._(kvp), origin
        )
        { }

        /// <summary>
        /// Merged map.
        /// </summary>
        public Merged(IMapInput<Key, Value> input, IMap<Key, Value> origin) : this(
            AsMap._(input), origin
        )
        { }

        /// <summary>
        /// Merged map.
        /// </summary>
        public Merged(params IMap<Key, Value>[] maps) : this(
            AsEnumerable._(maps)
        )
        { }

        /// <summary>
        /// Merged map.
        /// </summary>
        public Merged(IEnumerable<IMap<Key, Value>> maps) : base(
            AsMap._(
                Joined._(
                    Mapped._(
                        map => map.Pairs(),
                        maps
                    )
                )
            )
        )
        { }
    }

    /// <summary>
    /// Merged map.
    /// </summary>
    public static class Merged
    {
        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> _<Key, Value>(IPair<Key, Value> pair, IMap<Key, Value> origin)
            => new Merged<Key, Value>(pair, origin);

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> _<Key, Value>(IMapInput<Key, Value> input, IMap<Key, Value> origin)
            => new Merged<Key, Value>(input, origin);

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> _<Key, Value>(params IMap<Key, Value>[] maps)
            => new Merged<Key, Value>(maps);

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> _<Key, Value>(IEnumerable<IMap<Key, Value>> maps)
            => new Merged<Key, Value>(maps);
    }
}
