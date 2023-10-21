

using System.Collections.Generic;
using Tonga.List;

namespace Tonga.Map
{
    /// <summary>
    /// MapInput from key-value pairs.
    /// Since 9.9.2019
    /// </summary>
    public sealed class MapInputOf : MapInputEnvelope
    {
        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(params IPair[] kvps) : this(new AsList<IPair>(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(IEnumerable<IPair> kvps) : base(kvps)
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Value> _<Value>(params IPair<Value>[] kvps)
            => new MapInputOf<Value>(kvps);

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Value> _<Value>(IEnumerable<IPair<Value>> kvps)
            => new MapInputOf<Value>(kvps);

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Key, Value> _<Key, Value>(params IPair<Key, Value>[] kvps)
            => new MapInputOf<Key, Value>(kvps);

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Key, Value> _<Key, Value>(IEnumerable<IPair<Key, Value>> kvps)
            => new MapInputOf<Key, Value>(kvps);
    }

    /// <summary>
    /// MapInput from key-value pairs.
    /// Since 9.9.2019
    /// </summary>
    public sealed class MapInputOf<Value> : MapInputEnvelope<Value>
    {
        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(params IPair<Value>[] kvps) : this(new AsList<IPair<Value>>(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(IEnumerable<IPair<Value>> kvps) : base(kvps)
        { }
    }

    /// <summary>
    /// MapInput from key-value pairs.
    /// Since 9.9.2019
    /// </summary>
    public sealed class MapInputOf<Key, Value> : MapInputEnvelope<Key, Value>
    {
        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(params IPair<Key, Value>[] kvps) : this(new AsList<IPair<Key, Value>>(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(IEnumerable<IPair<Key, Value>> kvps) : base(kvps)
        { }
    }
}
