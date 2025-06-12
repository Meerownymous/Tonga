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

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> AsMerged<Key, Value>(this
            (IMap<Key, Value> m1, IMap<Key, Value> m2) maps
        ) => new Merged<Key, Value>(maps.m1, maps.m2);

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> AsMerged<Key, Value>(this
            (IMap<Key, Value> m1, IMap<Key, Value> m2, IMap<Key, Value> m3) maps
        ) => new Merged<Key, Value>(maps.m1, maps.m2, maps.m3);

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> AsMerged<Key, Value>(this
            (IMap<Key, Value> m1, IMap<Key, Value> m2, IMap<Key, Value> m3, IMap<Key,Value> m4) maps
        ) => new Merged<Key, Value>(maps.m1, maps.m2, maps.m3, maps.m4);

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> AsMerged<Key, Value>(this
            (IMap<Key, Value> m1, IMap<Key, Value> m2, IMap<Key, Value> m3, IMap<Key,Value> m4, IMap<Key, Value> m5) maps
        ) => new Merged<Key, Value>(maps.m1, maps.m2, maps.m3, maps.m4, maps.m5);

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> AsMerged<Key, Value>(this
            (IMap<Key, Value> m1, IMap<Key, Value> m2, IMap<Key, Value> m3, IMap<Key,Value> m4, IMap<Key, Value> m5, IMap<Key,Value> m6) maps
        ) => new Merged<Key, Value>(maps.m1, maps.m2, maps.m3, maps.m4, maps.m5, maps.m6);

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> AsMerged<Key, Value>(this
            (IMap<Key, Value> m1, IMap<Key, Value> m2,
            IMap<Key, Value> m3, IMap<Key,Value> m4,
            IMap<Key, Value> m5, IMap<Key,Value> m6,
            IMap<Key, Value> m7
            ) maps
        ) => new Merged<Key, Value>(maps.m1, maps.m2, maps.m3, maps.m4, maps.m5, maps.m6, maps.m7);

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<Key, Value> AsMerged<Key, Value>(this
            (IMap<Key, Value> m1, IMap<Key, Value> m2,
            IMap<Key, Value> m3, IMap<Key,Value> m4,
            IMap<Key, Value> m5, IMap<Key,Value> m6,
            IMap<Key, Value> m7, IMap<Key,Value> m8
            ) maps
        ) => new Merged<Key, Value>(maps.m1, maps.m2, maps.m3, maps.m4, maps.m5, maps.m6, maps.m7, maps.m8);
    }
}
