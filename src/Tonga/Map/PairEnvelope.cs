

namespace Tonga.Map
{
    /// <summary>
    /// Simplification of Kvp-class-building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class PairEnvelope : IPair
    {
        private readonly IPair origin;

        public PairEnvelope(IPair origin)
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
    public abstract class PairEnvelope<TValue> : IPair<TValue>
    {
        private readonly IPair<TValue> origin;

        /// <summary>
        /// Simplification of Kvp building
        /// </summary>
        public PairEnvelope(IPair<TValue> origin)
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
    public abstract class PairEnvelope<TKey, TValue> : IPair<TKey, TValue>
    {
        private readonly IPair<TKey, TValue> origin;

        /// <summary>
        /// Simplification of KVP building
        /// </summary>
        public PairEnvelope(IPair<TKey, TValue> origin)
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
