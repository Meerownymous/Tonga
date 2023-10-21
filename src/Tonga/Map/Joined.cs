

using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Map
{
    /// <summary>
    /// Joined map.
    /// Since 9.9.2019
    /// </summary>
    public sealed class Joined : MapEnvelope
    {
        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IPair kvp, IDictionary<string, string> origin) : this(
            new MapOf(kvp),
            origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IMapInput input, IDictionary<string, string> origin) : this(
            new MapOf(input),
            origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(params IDictionary<string, string>[] dicts) : this(
            new Enumerable.AsEnumerable<IDictionary<string, string>>(dicts)
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IEnumerable<IDictionary<string, string>> dicts, bool rejectBuildingAllValues = true) : base(
            () =>
                new LazyDict(
                    Enumerable.Joined._(
                        Mapped._(dict =>
                            Mapped._((key) => new AsPair(key, () => dict[key]),
                                dict.Keys
                            ),
                            dicts
                        )
                    )
                ),
                rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<string,Value> _<Value>(IPair<Value> kvp, IDictionary<string, Value> origin, bool live = false)
            => new Joined<Value>(kvp, origin, live);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<string,Value> _<Value>(IMapInput<Value> input, IDictionary<string, Value> origin, bool live = false)
            => new Joined<Value>(input, origin);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<string,Value> _<Value>(params IDictionary<string, Value>[] dicts)
            => new Joined<Value>(dicts);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<string,Value> _<Value>(bool live, params IDictionary<string, Value>[] dicts)
            => new Joined<Value>(live, dicts);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<string,Value> _<Value>(IEnumerable<IDictionary<string, Value>> dicts, bool live = false)
            => new Joined<Value>(dicts, live);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<Key, Value> _<Key, Value>(IPair<Key, Value> kvp, IDictionary<Key, Value> origin, bool live = false)
            => new Joined<Key, Value>(kvp, origin, live);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<Key, Value> _<Key, Value>(IMapInput<Key, Value> input, IDictionary<Key, Value> origin, bool live = false)
            => new Joined<Key, Value>(input, origin, live);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<Key, Value> _<Key, Value>(bool live, params IDictionary<Key, Value>[] dicts)
            => new Joined<Key, Value>(live, dicts);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<Key, Value> _<Key, Value>(params IDictionary<Key, Value>[] dicts)
            => new Joined<Key, Value>(dicts);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<Key, Value> _<Key, Value>(IEnumerable<IDictionary<Key, Value>> dicts, bool live = false)
            => new Joined<Key, Value>(dicts, live);
    }

    /// <summary>
    /// Joined map.
    /// Since 9.9.2019
    /// </summary>
    public sealed class Joined<Value> : MapEnvelope<Value>
    {
        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IPair<Value> kvp, IDictionary<string, Value> origin, bool live = false) : this(
            live,
            new MapOf<Value>(kvp), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IMapInput<Value> input, IDictionary<string, Value> origin, bool live = false) : this(
            live,
            new MapOf<Value>(input), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(params IDictionary<string, Value>[] dicts) : this(
            false,
            dicts
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(bool live, params IDictionary<string, Value>[] dicts) : this(
            new AsEnumerable<IDictionary<string, Value>>(dicts),
            live
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IEnumerable<IDictionary<string, Value>> dicts, bool live = false) : base(
            () =>
                new LazyDict<string, Value>(
                    Enumerable.Joined._(
                        Mapped._(dict =>
                            Mapped._((key) => new AsPair<string, Value>(key, () => dict[key]),
                                dict.Keys
                            ),
                            dicts
                        )
                    )
                )
            ,
            live
        )
        { }
    }


    /// <summary>
    /// Joined map.
    /// Since 9.9.2019
    /// </summary>
    public sealed class Joined<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IPair<Key, Value> kvp, IDictionary<Key, Value> origin, bool live = false) : this(
            live,
            new MapOf<Key, Value>(kvp), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IMapInput<Key, Value> input, IDictionary<Key, Value> origin, bool live = false) : this(
            live,
            new MapOf<Key, Value>(input), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(bool live, params IDictionary<Key, Value>[] dicts) : this(
            new Enumerable.AsEnumerable<IDictionary<Key, Value>>(dicts),
            live
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(params IDictionary<Key, Value>[] dicts) : this(
            new Enumerable.AsEnumerable<IDictionary<Key, Value>>(dicts),
            false
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IEnumerable<IDictionary<Key, Value>> dicts, bool live = false) : base(
            () =>
                new LazyDict<Key, Value>(
                    Enumerable.Joined._(
                        Mapped._(dict =>
                            Mapped._((key) => AsPair._(key, () => dict[key]),
                                dict.Keys
                            ),
                            dicts
                        )
                    )
                ),
            live
        )
        { }
    }
}
