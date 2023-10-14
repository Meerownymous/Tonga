

using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Map
{

    /// <summary>
    /// A map that is both threadsafe and sticky.
    /// </summary>
    /// <typeparam name="Key">type of key</typeparam>
    /// <typeparam name="Value">type of value</typeparam>
    public sealed class Solid<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// A map from the given Tuple pairs.
        /// </summary>
        /// <param name="pairs">Pairs of mappings</param>
        public Solid(Tuple<Key, Value>[] pairs) : this(
            new LiveMany<Tuple<Key, Value>>(pairs)
        )
        { }

        /// <summary>
        /// A map from the given Tuple pairs.
        /// </summary>
        /// <param name="pairs">Pairs of mappings</param>
        public Solid(IEnumerable<Tuple<Key, Value>> pairs) : this(
            new Mapped<Tuple<Key, Value>, KeyValuePair<Key, Value>>(
                tpl => new KeyValuePair<Key, Value>(tpl.Item1, tpl.Item2),
                pairs,
                live: true
            )
        )
        { }

        /// <summary>
        /// Makes a map from the given values.
        /// </summary>
        /// <param name="list"></param>
        public Solid(params KeyValuePair<Key, Value>[] list) : this(
            new LiveMany<KeyValuePair<Key, Value>>(list)
        )
        { }

        /// <summary>
        /// Makes a map by merging the given values into the given dictionary.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">list of values to merge</param>
        public Solid(IDictionary<Key, Value> map, params KeyValuePair<Key, Value>[] list) : this(
            map,
            new LiveMany<KeyValuePair<Key, Value>>(list)
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">List of values</param>        
        public Solid(IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new LiveMap<Key, Value>(() =>
                new MapOf<Key, Value>(list)
            )
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">List of values</param>
        public Solid(IEnumerator<KeyValuePair<Key, Value>> list) : this(
            new LiveMany<KeyValuePair<Key, Value>>(() => list)
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">list of values to merge</param>
        public Solid(IDictionary<Key, Value> map, IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new LiveMap<Key, Value>(() =>
                new MapOf<Key, Value>(
                    new Enumerable.Joined<KeyValuePair<Key, Value>>(live: true, map, list)
                )
            )
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map"></param>
        public Solid(IDictionary<Key, Value> map) : base(
            () =>
                new Synced<Key, Value>(
                    new MapOf<Key, Value>(map)
                ),
            false
        )
        { }
    }

    public static class Solid
    {
        /// <summary>
        /// A map from the given Tuple pairs.
        /// </summary>
        /// <param name="pairs">Pairs of mappings</param>
        public static IDictionary<Key, Value> New<Key, Value>(Tuple<Key, Value>[] pairs)
            => new Solid<Key, Value>(pairs);

        /// <summary>
        /// A map from the given Tuple pairs.
        /// </summary>
        /// <param name="pairs">Pairs of mappings</param>
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerable<Tuple<Key, Value>> pairs)
            => new Solid<Key, Value>(pairs);

        /// <summary>
        /// Makes a map from the given values.
        /// </summary>
        /// <param name="list"></param>
        public static IDictionary<Key, Value> New<Key, Value>(params KeyValuePair<Key, Value>[] list)
            => new Solid<Key, Value>(list);

        /// <summary>
        /// Makes a map by merging the given values into the given dictionary.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">list of values to merge</param>
        public static IDictionary<Key, Value> New<Key, Value>(IDictionary<Key, Value> map, params KeyValuePair<Key, Value>[] list)
            => new Solid<Key, Value>(list);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">List of values</param>        
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> list)
            => new Solid<Key, Value>(list);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">List of values</param>
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerator<KeyValuePair<Key, Value>> list)
            => new Solid<Key, Value>(list);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">list of values to merge</param>
        public static IDictionary<Key, Value> New<Key, Value>(IDictionary<Key, Value> map, IEnumerable<KeyValuePair<Key, Value>> list)
            => new Solid<Key, Value>(list);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map"></param>
        public static IDictionary<Key, Value> New<Key, Value>(IDictionary<Key, Value> map)
            => new Solid<Key, Value>(map);
    }
}
