using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Map;

/// <summary>
/// A map from string to typed value.
/// </summary>
public sealed class AsMap<Key, Value> : IMap<Key, Value>
{
    private readonly object lck;
    private readonly Lazy<IDictionary<Key, IPair<Key, Value>>> map;

    /// <summary>
    /// A map from the given KeyValuePairs
    /// </summary>
    public AsMap(params (Key key, Value value)[] pairs) : this(
        pairs.AsMapped(
            pair => pair.AsPair()
        )
    )
    { }

    /// <summary>
    /// A map from the given KeyValuePairs
    /// </summary>
    public AsMap(IPair<Key, Value> item, params IPair<Key, Value>[] more) : this(
        more.AsEnumerable()
            .AsJoined(item)
    )
    { }

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">inputs</param>
    public AsMap(params IMapInput<Key, Value>[] inputs) : this(
        inputs.AsEnumerable()
    )
    { }

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public AsMap(IEnumerable<IMapInput<Key, Value>> inputs) : this(
        () =>
        {
            IMap<Key, Value> merge = new Empty<Key, Value>();
            foreach (var input in inputs)
                merge = input.Merged(merge);

            return merge.Pairs();
        }
    )
    { }

    /// <summary>
    /// A map from the given dictionary.
    /// </summary>
    /// <param name="pairs">map content</param>
    public AsMap(IEnumerable<IPair<Key, Value>> pairs) : this(() => pairs)
    { }

    /// <summary>
    /// A map from the given dictionary.
    /// </summary>
    /// <param name="pairs">map content</param>
    public AsMap(Func<IEnumerable<IPair<Key, Value>>> pairs)
    {
        this.lck = new object();
        this.map = new Lazy<IDictionary<Key, IPair<Key, Value>>>(
            () =>
            {
                lock (this.lck)
                {
                    var dict = new Dictionary<Key, IPair<Key, Value>>();
                    foreach (var pair in pairs())
                    {
                        dict[pair.Key()] = pair;
                    }
                    return dict;
                }
            });
    }

    public Value this[Key key] =>
        ExceptionSwap._(
            () => this.map.Value[key].Value(),
            (ex) => new ArgumentException(ex.Message, ex)
        ).Value();

    public Func<Value> Lazy(Key key)
    {
        return () => this.map.Value[key].Value();
    }

    public ICollection<Key> Keys()
    {
        return this.map.Value.Keys;
    }

    public IEnumerable<IPair<Key, Value>> Pairs()
    {
        return this.map.Value.Values;
    }

    public IMap<Key, Value> With(IPair<Key, Value> pair)
    {
        lock (this.lck)
        {
            this.map.Value[pair.Key()] = pair;
        }
        return this;
    }
}

public static partial class MapSmarts
{
    /// <summary>
    /// A map from the given keys and values.
    /// </summary>
    public static IMap<Key, Value> AsMap<Key, Value>(this (Key key, Value value)[] pairs)
        => new AsMap<Key, Value>(
            pairs.AsMapped(
                pair => new AsPair<Key, Value>(pair.key, pair.value)
            )
        );

    /// <summary>
    /// A map from the given keys and values.
    /// </summary>
    public static IMap<Key, Value> AsMap<Key, Value>(this (Key key, Value value) pair)
        => new AsMap<Key, Value>(pair);

    /// <summary>
    /// A map from the given keys and values.
    /// </summary>
    public static IMap<Key, Value> AsMap<Key, Value>(this IPair<Key, Value> pair)
        => new AsMap<Key, Value>(pair);

    /// <summary>
    /// A map from the given key value pairs.
    /// </summary>
    /// <param name="entries">enumerable of kvps</param>
    public static IMap<Key, Value> AsMap<Key, Value>(this IEnumerable<IPair<Key, Value>> entries)
        => new AsMap<Key, Value>(entries);

    /// <summary>
    /// A map from the given key value pairs.
    /// </summary>
    /// <param name="entries">enumerable of kvps</param>
    public static IMap<Key, Value> AsMap<Key, Value>(this Func<IEnumerable<IPair<Key, Value>>> entries)
        => new AsMap<Key, Value>(entries);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(this IMapInput<Key, Value>[] inputs)
        => new AsMap<Key, Value>(inputs);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(this IMapInput<Key, Value> inputs)
        => new AsMap<Key, Value>(inputs);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(this IEnumerable<IMapInput<Key, Value>> inputs)
        => new AsMap<Key, Value>(inputs);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(this (IPair<Key,Value> p1, IPair<Key,Value> p2) inputs)
        => new AsMap<Key, Value>(inputs.p1, inputs.p2);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(
        this (
            IPair<Key,Value> p1,
            IPair<Key,Value> p2,
            IPair<Key,Value> p3
        ) inputs
    )
        => new AsMap<Key, Value>(inputs.p1, inputs.p2, inputs.p3);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(
        this (
            IPair<Key,Value> p1,
            IPair<Key,Value> p2,
            IPair<Key,Value> p3,
            IPair<Key,Value> p4
            ) inputs
    )
        => new AsMap<Key, Value>(inputs.p1, inputs.p2, inputs.p3, inputs.p4);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(
        this (
            IPair<Key,Value> p1,
            IPair<Key,Value> p2,
            IPair<Key,Value> p3,
            IPair<Key,Value> p4,
            IPair<Key,Value> p5
        ) inputs
    )
        => new AsMap<Key, Value>(inputs.p1, inputs.p2, inputs.p3, inputs.p4, inputs.p5);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(
        this (
            IPair<Key,Value> p1,
            IPair<Key,Value> p2,
            IPair<Key,Value> p3,
            IPair<Key,Value> p4,
            IPair<Key,Value> p5,
            IPair<Key,Value> p6
            ) inputs
    )
        => new AsMap<Key, Value>(inputs.p1, inputs.p2, inputs.p3, inputs.p4, inputs.p5, inputs.p6);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(
        this (
            IPair<Key,Value> p1,
            IPair<Key,Value> p2,
            IPair<Key,Value> p3,
            IPair<Key,Value> p4,
            IPair<Key,Value> p5,
            IPair<Key,Value> p6,
            IPair<Key,Value> p7
            ) inputs
    )
        => new AsMap<Key, Value>(inputs.p1, inputs.p2, inputs.p3, inputs.p4, inputs.p5, inputs.p6, inputs.p7);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(
        this (
            IPair<Key,Value> p1,
            IPair<Key,Value> p2,
            IPair<Key,Value> p3,
            IPair<Key,Value> p4,
            IPair<Key,Value> p5,
            IPair<Key,Value> p6,
            IPair<Key,Value> p7,
            IPair<Key,Value> p8
            ) inputs
    )
        => new AsMap<Key, Value>(
            inputs.p1, inputs.p2, inputs.p3, inputs.p4, inputs.p5, inputs.p6, inputs.p7, inputs.p8
        );

            /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(
        this (
            IMapInput<Key,Value> p1, IMapInput<Key,Value> p2
        ) inputs)
        => new AsMap<Key, Value>(inputs.p1, inputs.p2);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(
        this (
            IMapInput<Key,Value> p1,
            IMapInput<Key,Value> p2,
            IMapInput<Key,Value> p3
        ) inputs
    )
        => new AsMap<Key, Value>(inputs.p1, inputs.p2, inputs.p3);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(
        this (
            IMapInput<Key,Value> p1,
            IMapInput<Key,Value> p2,
            IMapInput<Key,Value> p3,
            IMapInput<Key,Value> p4
            ) inputs
    )
        => new AsMap<Key, Value>(inputs.p1, inputs.p2, inputs.p3, inputs.p4);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(
        this (
            IMapInput<Key,Value> p1,
            IMapInput<Key,Value> p2,
            IMapInput<Key,Value> p3,
            IMapInput<Key,Value> p4,
            IMapInput<Key,Value> p5
        ) inputs
    )
        => new AsMap<Key, Value>(inputs.p1, inputs.p2, inputs.p3, inputs.p4, inputs.p5);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(
        this (
            IMapInput<Key,Value> p1,
            IMapInput<Key,Value> p2,
            IMapInput<Key,Value> p3,
            IMapInput<Key,Value> p4,
            IMapInput<Key,Value> p5,
            IMapInput<Key,Value> p6
            ) inputs
    )
        => new AsMap<Key, Value>(inputs.p1, inputs.p2, inputs.p3, inputs.p4, inputs.p5, inputs.p6);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(
        this (
            IMapInput<Key,Value> p1,
            IMapInput<Key,Value> p2,
            IMapInput<Key,Value> p3,
            IMapInput<Key,Value> p4,
            IMapInput<Key,Value> p5,
            IMapInput<Key,Value> p6,
            IMapInput<Key,Value> p7
            ) inputs
    )
        => new AsMap<Key, Value>(inputs.p1, inputs.p2, inputs.p3, inputs.p4, inputs.p5, inputs.p6, inputs.p7);

    /// <summary>
    /// A map from the given inputs.
    /// </summary>
    /// <param name="inputs">enumerable of map inputs</param>
    public static IMap<Key, Value> AsMap<Key, Value>(
        this (
            IMapInput<Key,Value> p1,
            IMapInput<Key,Value> p2,
            IMapInput<Key,Value> p3,
            IMapInput<Key,Value> p4,
            IMapInput<Key,Value> p5,
            IMapInput<Key,Value> p6,
            IMapInput<Key,Value> p7,
            IMapInput<Key,Value> p8
            ) inputs
    )
        => new AsMap<Key, Value>(
            inputs.p1, inputs.p2, inputs.p3, inputs.p4, inputs.p5, inputs.p6, inputs.p7, inputs.p8
        );

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Key,Value> AsMap<Key,Value>(this
        (
        (Key k, Value v) p1,
        (Key k,Value v) p2
        ) pairs
    ) => new AsMap<Key,Value>(pairs.p1, pairs.p2);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Key,Value> AsMap<Key,Value>(this
        (
        (Key k, Value v) p1,
        (Key k,Value v) p2,
        (Key k,Value v) p3
        ) pairs
    ) => new AsMap<Key,Value>(pairs.p1, pairs.p2, pairs.p3);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Key,Value> AsMap<Key,Value>(this
        (
        (Key k, Value v) p1,
        (Key k,Value v) p2,
        (Key k,Value v) p3,
        (Key k,Value v) p4
        ) pairs
    ) => new AsMap<Key,Value>(pairs.p1, pairs.p2, pairs.p3, pairs.p4);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Key,Value> AsMap<Key,Value>(this
        (
        (Key k, Value v) p1,
        (Key k,Value v) p2,
        (Key k,Value v) p3,
        (Key k,Value v) p4,
        (Key k,Value v) p5
        ) pairs
    ) => new AsMap<Key,Value>(pairs.p1, pairs.p2, pairs.p3, pairs.p4, pairs.p5);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Key,Value> AsMap<Key,Value>(this
        (
        (Key k, Value v) p1,
        (Key k,Value v) p2,
        (Key k,Value v) p3,
        (Key k,Value v) p4,
        (Key k,Value v) p5,
        (Key k,Value v) p6
        ) pairs
    ) => new AsMap<Key,Value>(pairs.p1, pairs.p2, pairs.p3, pairs.p4, pairs.p5, pairs.p6);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Key,Value> AsMap<Key,Value>(this
        (
        (Key k, Value v) p1,
        (Key k,Value v) p2,
        (Key k,Value v) p3,
        (Key k,Value v) p4,
        (Key k,Value v) p5,
        (Key k,Value v) p6,
        (Key k,Value v) p7
        ) pairs
    ) => new AsMap<Key,Value>(pairs.p1, pairs.p2, pairs.p3, pairs.p4, pairs.p5, pairs.p6 ,pairs.p7);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Key,Value> AsMap<Key,Value>(this
        (
            (Key k, Value v) p1,
            (Key k,Value v) p2,
            (Key k,Value v) p3,
            (Key k,Value v) p4,
            (Key k,Value v) p5,
            (Key k,Value v) p6,
            (Key k,Value v) p7,
            (Key k,Value v) p8
        ) pairs
    ) => new AsMap<Key,Value>(pairs.p1, pairs.p2, pairs.p3, pairs.p4, pairs.p5, pairs.p6 ,pairs.p7, pairs.p8);
}
