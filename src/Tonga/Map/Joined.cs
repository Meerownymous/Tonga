

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
        public Joined(IKvp kvp, IDictionary<string, string> origin) : this(
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
            new LiveMany<IDictionary<string, string>>(dicts)
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IEnumerable<IDictionary<string, string>> dicts, bool rejectBuildingAllValues = true) : base(
            () =>
                new LazyDict(
                    new Enumerable.Joined<IKvp>(
                        new Mapped<IDictionary<string, string>, IEnumerable<IKvp>>(dict =>
                            new LiveMany<IKvp>(() =>
                                new Live<IEnumerator<IKvp>>(() =>
                                {
                                    IEnumerable<IKvp> list = new ManyOf<IKvp>();
                                    foreach (var key in dict.Keys)
                                    {
                                        list = new Enumerable.Joined<IKvp>(list, new KvpOf(key, () => dict[key]));
                                    }
                                    return list.GetEnumerator();
                                }).Value()
                            ),
                            dicts,
                            live: false
                        ),
                        live: false
                    )
                ),
                rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<string,Value> New<Value>(IKvp<Value> kvp, IDictionary<string, Value> origin, bool live = false)
            => new Joined<Value>(kvp, origin, live);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<string,Value> New<Value>(IMapInput<Value> input, IDictionary<string, Value> origin, bool live = false)
            => new Joined<Value>(input, origin);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<string,Value> New<Value>(params IDictionary<string, Value>[] dicts)
            => new Joined<Value>(dicts);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<string,Value> New<Value>(bool live, params IDictionary<string, Value>[] dicts)
            => new Joined<Value>(live, dicts);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<string,Value> New<Value>(IEnumerable<IDictionary<string, Value>> dicts, bool live = false)
            => new Joined<Value>(dicts, live);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(IKvp<Key, Value> kvp, IDictionary<Key, Value> origin, bool live = false)
            => new Joined<Key, Value>(kvp, origin, live);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(IMapInput<Key, Value> input, IDictionary<Key, Value> origin, bool live = false)
            => new Joined<Key, Value>(input, origin, live);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(bool live, params IDictionary<Key, Value>[] dicts)
            => new Joined<Key, Value>(live, dicts);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(params IDictionary<Key, Value>[] dicts)
            => new Joined<Key, Value>(dicts);

        /// <summary>
        /// Joined map.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerable<IDictionary<Key, Value>> dicts, bool live = false)
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
        public Joined(IKvp<Value> kvp, IDictionary<string, Value> origin, bool live = false) : this(
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
            new LiveMany<IDictionary<string, Value>>(dicts),
            live
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IEnumerable<IDictionary<string, Value>> dicts, bool live = false) : base(
            () =>
                new LazyDict<string, Value>(
                    new Enumerable.Joined<IKvp<string, Value>>(
                        new Mapped<IDictionary<string, Value>, IEnumerable<IKvp<string, Value>>>(dict =>
                            new LiveMany<IKvp<string, Value>>(() =>
                                {
                                    IEnumerable<IKvp<string, Value>> list = new ManyOf<IKvp<string, Value>>();
                                    foreach (var key in dict.Keys)
                                    {
                                        list = new Enumerable.Joined<IKvp<string, Value>>(list, new KvpOf<string, Value>(key, () => dict[key]));
                                    }
                                    return list.GetEnumerator();
                                }
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
        public Joined(IKvp<Key, Value> kvp, IDictionary<Key, Value> origin, bool live = false) : this(
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
            new LiveMany<IDictionary<Key, Value>>(dicts),
            live
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(params IDictionary<Key, Value>[] dicts) : this(
            new LiveMany<IDictionary<Key, Value>>(dicts),
            false
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IEnumerable<IDictionary<Key, Value>> dicts, bool live = false) : base(
            () =>
                new LazyDict<Key, Value>(
                    new Enumerable.Joined<IKvp<Key, Value>>(
                        new Mapped<IDictionary<Key, Value>, IEnumerable<IKvp<Key, Value>>>(dict =>
                            new LiveMany<IKvp<Key, Value>>(() =>
                                {
                                    IEnumerable<IKvp<Key, Value>> list = new ManyOf<IKvp<Key, Value>>();
                                    foreach (var key in dict.Keys)
                                    {
                                        list = new Enumerable.Joined<IKvp<Key, Value>>(list, new KvpOf<Key, Value>(key, () => dict[key]));
                                    }
                                    return list.GetEnumerator();
                                }
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
}
