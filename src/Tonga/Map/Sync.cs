

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Map
{
    /// <summary>
    /// A map that is threadsafe.
    /// </summary>
    /// <typeparam name="Key">type of key</typeparam>
    /// <typeparam name="Value">type of value</typeparam>
    public sealed class Synced<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="list"></param>
        public Synced(KeyValuePair<Key, Value>[] list) : this(
            new Enumerable.AsEnumerable<KeyValuePair<Key, Value>>(list)
        )
        { }

        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">list of entries to merge</param>
        public Synced(Dictionary<Key, Value> map, KeyValuePair<Key, Value>[] list) : this(
            map,
            new Enumerable.AsEnumerable<KeyValuePair<Key, Value>>(list)
        )
        { }

        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="list">list of entries</param>
        public Synced(IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new LiveMap<Key, Value>(() =>
                new MapOf<Key, Value>(list)
            )
        )
        { }

        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="list">list of entries</param>
        public Synced(IEnumerator<KeyValuePair<Key, Value>> list) : this(
            Enumerable.AsEnumerable._(() => list)
        )
        { }

        /// <summary>
        /// A merged map that is treadsafe.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">items to merge</param>
        public Synced(IDictionary<Key, Value> map, IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new LiveMap<Key, Value>(() =>
                new MapOf<Key, Value>(
                    Enumerable.Joined.From(map, list)
                )
            )
        )
        { }

        /// <summary>
        /// A merged map that is threadsafe.
        /// </summary>
        /// <param name="map">Map to make threadsafe</param>
        public Synced(IDictionary<Key, Value> map) : base(
            () =>
            new Sync<IDictionary<Key, Value>>(() =>
                new ConcurrentDictionary<Key, Value>(map)
            ).Value(),
            false
        )
        { }
    }

    /// <summary>
    /// Makes a threadsafe map
    /// </summary>
    /// <typeparam name="Source">source value type</typeparam>
    /// <typeparam name="Key">type of key</typeparam>
    /// <typeparam name="Value">type of value</typeparam>
    public sealed class Sync<Source, Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// Makes a threadsafe map.
        /// </summary>
        /// <param name="map">source map to merge to</param>
        /// <param name="list">list of values to merge</param>
        /// <param name="key">func to get the key</param>
        /// <param name="value">func to get the value</param>
        public Sync(IDictionary<Key, Value> map, IEnumerable<Source> list, Func<Source, Key> key, Func<Source, Value> value) : this(
                map,
                list,
                item => new KeyValuePair<Key, Value>(key.Invoke(item), value.Invoke(item))
            )
        { }

        /// <summary>
        /// Makes a threadsafe map.
        /// </summary>
        /// <param name="list">list of values to merge</param>
        /// <param name="key">func to get the key</param>
        /// <param name="value">func to get the value</param>
        public Sync(IEnumerable<Source> list, Func<Source, Key> key, Func<Source, Value> value) : this(
            list,
            item => new KeyValuePair<Key, Value>(key.Invoke(item), value.Invoke(item))
        )
        { }

        /// <summary>
        /// Makes a threadsafe map.
        /// </summary>
        /// <param name="list">list of values to merge</param>
        /// <param name="entry">func to get the entry</param>
        public Sync(IEnumerable<Source> list, Func<Source, KeyValuePair<Key, Value>> entry) : this(
            new Mapped<Source, KeyValuePair<Key, Value>>(entry, list)
        )
        { }

        /// <summary>
        /// Makes a threadsafe map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="list"></param>
        /// <param name="entry"></param>
        public Sync(IDictionary<Key, Value> map, IEnumerable<Source> list, Func<Source, KeyValuePair<Key, Value>> entry) : this(
            map,
            new Mapped<Source, KeyValuePair<Key, Value>>(entry, list)
        )
        { }

        /// <summary>
        /// A merged map that is treadsafe.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">items to merge</param>
        public Sync(IDictionary<Key, Value> map, IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new LiveMap<Key, Value>(() =>
                new MapOf<Key, Value>(
                    Enumerable.Joined.From(map, list)
                )
            )
        )
        { }

        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="list">list of entries</param>
        public Sync(IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new LiveMap<Key, Value>(() =>
                new MapOf<Key, Value>(list)
            )
        )
        { }

        /// <summary>
        /// A merged map that is threadsafe.
        /// </summary>
        /// <param name="map">Map to make threadsafe</param>
        public Sync(IDictionary<Key, Value> map) : base(
            () =>
                new Sync<IDictionary<Key, Value>>(() =>
                    new ConcurrentDictionary<Key, Value>(map)
                ).Value(),
            false
        )
        { }
    }

    public static class Synced
    {
        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="list"></param>
        public static IDictionary<Key, Value> New<Key, Value>(KeyValuePair<Key, Value>[] list)
            => new Synced<Key, Value>(list);

        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">list of entries to merge</param>
        public static IDictionary<Key, Value> New<Key, Value>(Dictionary<Key, Value> map, KeyValuePair<Key, Value>[] list)
            => new Synced<Key, Value>(map, list);

        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="list">list of entries</param>
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> list)
            => new Synced<Key, Value>(list);

        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="list">list of entries</param>
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerator<KeyValuePair<Key, Value>> list)
            => new Synced<Key, Value>(list);

        /// <summary>
        /// A merged map that is treadsafe.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">items to merge</param>
        public static IDictionary<Key, Value> New<Key, Value>(IDictionary<Key, Value> map, IEnumerable<KeyValuePair<Key, Value>> list)
            => new Synced<Key, Value>(map, list);

        /// <summary>
        /// A merged map that is threadsafe.
        /// </summary>
        /// <param name="map">Map to make threadsafe</param>
        public static IDictionary<Key, Value> New<Key, Value>(IDictionary<Key, Value> map)
            => new Synced<Key, Value>(map);

        /// <summary>
        /// Makes a threadsafe map.
        /// </summary>
        /// <param name="map">source map to merge to</param>
        /// <param name="list">list of values to merge</param>
        /// <param name="key">func to get the key</param>
        /// <param name="value">func to get the value</param>
        public static IDictionary<Key, Value> New<Source, Key, Value>(IDictionary<Key, Value> map, IEnumerable<Source> list, Func<Source, Key> key, Func<Source, Value> value)
            => new Sync<Source, Key, Value>(map, list, key, value);

        /// <summary>
        /// Makes a threadsafe map.
        /// </summary>
        /// <param name="list">list of values to merge</param>
        /// <param name="key">func to get the key</param>
        /// <param name="value">func to get the value</param>
        public static IDictionary<Key, Value> New<Source, Key, Value>(IEnumerable<Source> list, Func<Source, Key> key, Func<Source, Value> value)
            => new Sync<Source, Key, Value>(list, key, value);

        /// <summary>
        /// Makes a threadsafe map.
        /// </summary>
        /// <param name="list">list of values to merge</param>
        /// <param name="entry">func to get the entry</param>
        public static IDictionary<Key, Value> New<Source, Key, Value>(IEnumerable<Source> list, Func<Source, KeyValuePair<Key, Value>> entry)
            => new Sync<Source, Key, Value>(list, entry);

        /// <summary>
        /// Makes a threadsafe map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="list"></param>
        /// <param name="entry"></param>
        public static IDictionary<Key, Value> New<Source, Key, Value>(IDictionary<Key, Value> map, IEnumerable<Source> list, Func<Source, KeyValuePair<Key, Value>> entry)
            => new Sync<Source, Key, Value>(map, list, entry);

        /// <summary>
        /// A merged map that is treadsafe.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">items to merge</param>
        public static IDictionary<Key, Value> New<Source, Key, Value>(IDictionary<Key, Value> map, IEnumerable<KeyValuePair<Key, Value>> list)
            => new Sync<Source, Key, Value>(map, list);

        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="list">list of entries</param>
        public static IDictionary<Key, Value> New<Source, Key, Value>(IEnumerable<KeyValuePair<Key, Value>> list)
            => new Sync<Source, Key, Value>(list);

        /// <summary>
        /// A merged map that is threadsafe.
        /// </summary>
        /// <param name="map">Map to make threadsafe</param>
        public static IDictionary<Key, Value> New<Source, Key, Value>(IDictionary<Key, Value> map)
            => new Sync<Source, Key, Value>(map);
    }

}
