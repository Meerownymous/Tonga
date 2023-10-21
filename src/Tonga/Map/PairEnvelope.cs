

namespace Tonga.Map
{
    /// <summary>
    /// Simplification of Kvp-class-building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class KvpEnvelope : IPair
    {
        private readonly IPair origin;

        public KvpEnvelope(IPair origin)
        {
            this.origin = origin;
        }

        public string Key()
        {
            return this.origin.Key();
        }

        public string Value()
        {
            return this.origin.Value();
        }

        public bool IsLazy()
        {
            return this.origin.IsLazy();
        }
    }

    /// <summary>
    /// Simplification of Kvp building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class KvpEnvelope<TValue> : IPair<TValue>
    {
        private readonly IPair<TValue> origin;

        /// <summary>
        /// Simplification of Kvp building
        /// </summary>
        public KvpEnvelope(IPair<TValue> origin)
        {
            this.origin = origin;
        }

        public string Key()
        {
            return this.origin.Key();
        }

        public TValue Value()
        {
            return this.origin.Value();
        }

        public bool IsLazy()
        {
            return this.origin.IsLazy();
        }
    }

    /// <summary>
    /// Simplification of Kvp-class-building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class KvpEnvelope<TKey, TValue> : IPair<TKey, TValue>
    {
        private readonly IPair<TKey, TValue> origin;

        /// <summary>
        /// Simplification of KVP building
        /// </summary>
        public KvpEnvelope(IPair<TKey, TValue> origin)
        {
            this.origin = origin;
        }

        public TKey Key()
        {
            return this.origin.Key();
        }

        public TValue Value()
        {
            return this.origin.Value();
        }

        public bool IsLazy()
        {
            return this.origin.IsLazy();
        }
    }
}
