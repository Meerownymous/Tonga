using Tonga.Enumerable;

namespace Tonga.Map
{
    /// <summary>
    /// A map which is empty.
    /// </summary>
    public sealed class EmptyMap : MapEnvelope
    {
        /// <summary>
        /// A map which is empty.
        /// </summary>
        public EmptyMap() : base(() => new AsMap(new None()), false)
        { }
    }

    /// <summary>
    /// A map which is empty.
    /// </summary>
    public sealed class EmptyMap<Value> : MapEnvelope<Value>
    {
        /// <summary>
        /// A map which is empty.
        /// </summary>
        public EmptyMap() : base(() => AsMap._<Value>(), false)
        { }
    }

    /// <summary>
    /// A map which is empty.
    /// </summary>
    public sealed class EmptyMap<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// A map which is empty.
        /// </summary>
        public EmptyMap() : base(() => AsMap._<Key, Value>(), false)
        { }
    }
}
