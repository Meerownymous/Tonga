

using System;
using System.Collections.Generic;

namespace Tonga.Map
{
    /// <summary>
    /// A map from string to string.
    /// You must understand, that this map will build every time when any method is called.
    /// If you do not want this, use <see cref="AsMap"/>
    /// </summary>
    public sealed class LiveMap : MapEnvelope
    {
        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public LiveMap(System.Func<IEnumerable<IPair>> entries) : this(() =>
            new LazyDict(entries(), true)
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public LiveMap(System.Func<IDictionary<string, string>> input) : base(input, true)
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public static IDictionary<string, Value> _<Value>(System.Func<IEnumerable<IPair<Value>>> entries)
            => new LiveMap<Value>(entries);

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public static IDictionary<string, Value> _<Value>(System.Func<IDictionary<string, Value>> input)
            => new LiveMap<Value>(input);

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public static IDictionary<Key, Value> _<Key, Value>(System.Func<IEnumerable<IPair<Key, Value>>> entries, bool rejectBuildingAllValues = true)
            => new LiveMap<Key, Value>(entries, rejectBuildingAllValues);

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public static IDictionary<Key, Value> _<Key, Value>(System.Func<IDictionary<Key, Value>> input)
            => new LiveMap<Key, Value>(input);
    }

    /// <summary>
    /// A map from string to typed value.
    /// You must understand, that this map will build every time when any method is called.
    /// If you do not want this, use <see cref="AsMap"/>
    /// </summary>
    public sealed class LiveMap<Value> : MapEnvelope<Value>
    {
        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public LiveMap(System.Func<IEnumerable<IPair<Value>>> entries) : this(() =>
            new LazyDict<Value>(entries(), true)
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public LiveMap(System.Func<IDictionary<string, Value>> input) : base(input, true)
        { }
    }

    /// <summary>
    /// A map from one type to another.
    /// You must understand, that this map will build every time when any method is called.
    /// If you do not want this, use <see cref="AsMap"/>
    /// </summary>
    public sealed class LiveMap<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public LiveMap(System.Func<IEnumerable<IPair<Key, Value>>> entries, bool rejectBuildingAllValues = true) : this(() =>
            new LazyDict<Key, Value>(entries(), rejectBuildingAllValues)
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public LiveMap(System.Func<IDictionary<Key, Value>> input) : base(input, true)
        { }
    }
}
