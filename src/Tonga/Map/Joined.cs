using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Map
{
    /// <summary>
    /// Joined map.
    /// Since 9.9.2019
    /// </summary>
    public sealed class Joined<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IPair<Key, Value> kvp, IMap<Key, Value> origin) : this(
            AsMap._(kvp), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IMapInput<Key, Value> input, IMap<Key, Value> origin) : this(
            AsMap._(input), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(params IMap<Key, Value>[] dicts) : this(
            AsEnumerable._(dicts)
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IEnumerable<IMap<Key, Value>> maps) : base(
            () =>
                AsMap._(
                    Enumerable.Joined._(
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
    /// Joined map.
    /// </summary>
    public static class Joined
    {
        /// <summary>
        /// Joined map.
        /// </summary>
        public static IMap<string, Value> _<Value>(
            IMapInput<string, Value> input, IMap<string, Value> origin)
            => new Joined<string, Value>(input, origin);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IMap<string, Value> _<Value>(params IMap<string, Value>[] dicts)
            => new Joined<string, Value>(dicts);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IMap<string, Value> _<Value>(IEnumerable<IMap<string, Value>> maps)
            => new Joined<string, Value>(maps);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IMap<Key, Value> _<Key, Value>(IPair<Key, Value> pair, IMap<Key, Value> origin)
            => new Joined<Key, Value>(pair, origin);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IMap<Key, Value> _<Key, Value>(IMapInput<Key, Value> input, IMap<Key, Value> origin)
            => new Joined<Key, Value>(input, origin);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IMap<Key, Value> _<Key, Value>(bool live, params IMap<Key, Value>[] maps)
            => new Joined<Key, Value>(maps);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IMap<Key, Value> _<Key, Value>(params IMap<Key, Value>[] maps)
            => new Joined<Key, Value>(maps);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IMap<Key, Value> _<Key, Value>(IEnumerable<IMap<Key, Value>> maps)
            => new Joined<Key, Value>(maps);
    }
}
