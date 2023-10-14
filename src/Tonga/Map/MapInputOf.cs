

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
        public MapInputOf(params IKvp[] kvps) : this(new ListOf<IKvp>(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(IEnumerable<IKvp> kvps) : base(kvps)
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Value> New<Value>(params IKvp<Value>[] kvps)
            => new MapInputOf<Value>(kvps);

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Value> New<Value>(IEnumerable<IKvp<Value>> kvps)
            => new MapInputOf<Value>(kvps);

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Key, Value> New<Key, Value>(params IKvp<Key, Value>[] kvps)
            => new MapInputOf<Key, Value>(kvps);

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Key, Value> New<Key, Value>(IEnumerable<IKvp<Key, Value>> kvps)
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
        public MapInputOf(params IKvp<Value>[] kvps) : this(new ListOf<IKvp<Value>>(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(IEnumerable<IKvp<Value>> kvps) : base(kvps)
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
        public MapInputOf(params IKvp<Key, Value>[] kvps) : this(new ListOf<IKvp<Key, Value>>(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(IEnumerable<IKvp<Key, Value>> kvps) : base(kvps)
        { }
    }
}
