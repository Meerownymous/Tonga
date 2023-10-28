

using System;
using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.Map
{

    /// <summary>
    /// Key-value pair matching a key type to specified type value.
    /// </summary>
    public sealed class AsPair2<TKey, TValue> : IPair<TKey, TValue>
    {
        private readonly IScalar<KeyValuePair<TKey, Func<TValue>>> entry;
        private readonly Func<TValue> value;
        private readonly bool isLazy;

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public AsPair2(TKey key, Func<TValue> value) : this(
            () => new KeyValuePair<TKey, Func<TValue>>(key, value),
            true
        )
        { }

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public AsPair2(TKey key, TValue value) : this(
            () => new KeyValuePair<TKey, Func<TValue>>(key, () => value),
            false
        )
        { }

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public AsPair2(IScalar<KeyValuePair<TKey, TValue>> kvp) : this(kvp.Value)
        { }

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public AsPair2(Func<KeyValuePair<TKey, TValue>> kvp)
            : this(() =>
            {
                var simple = kvp.Invoke();
                return new KeyValuePair<TKey, Func<TValue>>(simple.Key, () => simple.Value);
            }, true)
        { }

        private AsPair2(Func<KeyValuePair<TKey, Func<TValue>>> kvp, bool isLazy)
        {
            this.entry = Scalar.Sticky._(kvp.Invoke);
            this.value = () => this.entry.Value().Value.Invoke();
            this.isLazy = isLazy;
        }

        public TKey Key()
        {
            return this.entry.Value().Key;
        }

        public TValue Value()
        {
            return this.value();
        }

        public bool IsLazy()
        {
            return this.isLazy;
        }
    }

    public static class AsPair2
    {
        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TValue> _<TKey, TValue>(TKey key, Func<TValue> value)
            => new AsPair2<TKey, TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TValue> _<TKey, TValue>(TKey key, TValue value)
            => new AsPair2<TKey, TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TValue> _<TKey, TValue>(IScalar<KeyValuePair<TKey, TValue>> kvp)
            => new AsPair2<TKey, TValue>(kvp);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TValue> _<TKey, TValue>(Func<KeyValuePair<TKey, TValue>> kvp)
            => new AsPair2<TKey, TValue>(kvp);
    }
}
