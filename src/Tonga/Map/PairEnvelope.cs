namespace Tonga.Map
{
    /// <summary>
    /// Simplification of Pair-building.
    /// </summary>
    public abstract class PairEnvelope<TKey, TValue>(IPair<TKey, TValue> origin) : IPair<TKey, TValue>
    {
        public TKey Key() => origin.Key();
        public TValue Value() => origin.Value();
        public bool IsLazy() => origin.IsLazy();
    }
}
