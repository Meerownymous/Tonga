using System;

namespace Tonga.Map
{
    /// <summary>
    /// Fake Pair
    /// </summary>
    public sealed class FkPair<TKey, TValue> : IPair<TKey, TValue>
    {
        private readonly Func<TKey> keyFunc;
        private readonly Func<TValue> valueFunc;
        private readonly Func<bool> isLazyFunc;

        /// <summary>
        /// Fake Kvp
        /// </summary>
        public FkPair(Func<TKey> keyFunc, Func<TValue> valueFunc, Func<bool> isLazyFunc)
        {
            this.keyFunc = keyFunc;
            this.valueFunc = valueFunc;
            this.isLazyFunc = isLazyFunc;
        }

        public TValue Value()
        {
            return this.valueFunc();
        }

        public TKey Key()
        {
            return this.keyFunc();
        }

        public bool IsLazy()
        {
            return this.isLazyFunc();
        }
    }
}
