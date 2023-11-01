using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Scalar;

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
        public Merged(params IMap<Key, Value>[] dicts) : this(
            AsEnumerable._(dicts)
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
        public static IMap<string, Value> _<Value>(
            IMapInput<string, Value> input, IMap<string, Value> origin)
            => new Merged<string, Value>(input, origin);

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<string, Value> _<Value>(params IMap<string, Value>[] dicts)
            => new Merged<string, Value>(dicts);

        /// <summary>
        /// Merged map.
        /// </summary>
        public static IMap<string, Value> _<Value>(IEnumerable<IMap<string, Value>> maps)
            => new Merged<string, Value>(maps);

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
        public static IMap<Key, Value> _<Key, Value>(bool live, params IMap<Key, Value>[] maps)
            => new Merged<Key, Value>(maps);

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
