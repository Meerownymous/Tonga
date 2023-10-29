

namespace Tonga.Map
{
    /// <summary>
    /// Simplification of Pair-building.
    /// </summary>
    public abstract class PairEnvelope<TKey, TValue> : IPair<TKey, TValue>
    {
        private readonly IPair<TKey, TValue> origin;

        /// <summary>
        /// Simplification of Pair building
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
