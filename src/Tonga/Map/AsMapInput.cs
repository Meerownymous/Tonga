

using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.List;

namespace Tonga.Map
{
    /// <summary>
    /// MapInput from key-value pairs.
    /// Since 9.9.2019
    /// </summary>
    public sealed class AsMapInput : MapInputEnvelope
    {
        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public AsMapInput(params IPair[] kvps) : this(AsEnumerable._(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public AsMapInput(IEnumerable<IPair> kvps) : base(kvps)
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static AsMapInput<Value> _<Value>(params IPair<Value>[] kvps)
            => new AsMapInput<Value>(kvps);

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Value> _<Value>(IEnumerable<IPair<Value>> kvps)
            => new AsMapInput<Value>(kvps);

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Key, Value> _<Key, Value>(params IPair<Key, Value>[] kvps)
            => new AsMapInput<Key, Value>(kvps);

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Key, Value> _<Key, Value>(IEnumerable<IPair<Key, Value>> kvps)
            => new AsMapInput<Key, Value>(kvps);
    }

    /// <summary>
    /// MapInput from key-value pairs.
    /// Since 9.9.2019
    /// </summary>
    public sealed class AsMapInput<Value> : MapInputEnvelope<Value>
    {
        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public AsMapInput(params IPair<Value>[] kvps) : this(new AsEnumerable<IPair<Value>>(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public AsMapInput(IEnumerable<IPair<Value>> kvps) : base(kvps)
        { }
    }

    /// <summary>
    /// MapInput from key-value pairs.
    /// Since 9.9.2019
    /// </summary>
    public sealed class AsMapInput<Key, Value> : MapInputEnvelope<Key, Value>
    {
        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public AsMapInput(params IPair<Key, Value>[] kvps) : this(new AsEnumerable<IPair<Key, Value>>(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public AsMapInput(IEnumerable<IPair<Key, Value>> kvps) : base(kvps)
        { }
    }
}
