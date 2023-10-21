

using System;

namespace Tonga.Map
{
    /// <summary>
    /// Fake Kvp
    /// </summary>
    public sealed class FkKvp<TKey, TValue> : IPair<TKey, TValue>
    {
        private readonly System.Func<TKey> keyFunc;
        private readonly System.Func<TValue> valueFunc;
        private readonly System.Func<bool> isLazyFunc;

        /// <summary>
        /// Fake Kvp
        /// </summary>
        public FkKvp(System.Func<TKey> keyFunc, System.Func<TValue> valueFunc, System.Func<bool> isLazyFunc)
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
