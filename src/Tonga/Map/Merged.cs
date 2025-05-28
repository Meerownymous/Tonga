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
        public Merged(IPair<Key, Value> pair, IMap<Key, Value> origin) : this(
            pair.AsMap(), origin
        )
        { }

        /// <summary>
        /// Merged map.
        /// </summary>
        public Merged(IMapInput<Key, Value> input, IMap<Key, Value> origin) : this(
            new AsMap<Key,Value>(input), origin
        )
        { }

        /// <summary>
        /// Merged map.
        /// </summary>
        public Merged(params IMap<Key, Value>[] maps) : this(maps.AsEnumerable())
        { }

        /// <summary>
        /// Merged map.
        /// </summary>
        public Merged(IEnumerable<IMap<Key, Value>> maps) : base(
            maps.AsMapped(map => map.Pairs())
                .AsJoined()
                .AsMap<Key,Value>()
        )
        { }
    }

    /// <summary>
    /// Merged map.
    /// </summary>
    public static partial class MapSmarts
    {
        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> AsMerged<Key, Value>(this IMap<Key, Value> origin, IPair<Key, Value> pair)
            => new Merged<Key, Value>(pair, origin);

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> AsMerged<Key, Value>(this IMap<Key, Value> origin, IMapInput<Key, Value> input)
            => new Merged<Key, Value>(input, origin);

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> AsMerged<Key, Value>(this IMap<Key, Value>[] maps)
            => new Merged<Key, Value>(maps);

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> AsMerged<Key, Value>(this IEnumerable<IMap<Key, Value>> maps)
            => new Merged<Key, Value>(maps);
    }
}
