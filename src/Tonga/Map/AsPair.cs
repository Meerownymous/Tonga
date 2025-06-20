using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Tonga.Scalar;

namespace Tonga.Map
{
    /// <summary>
    /// Key-value pair matching a key type to specified type value.
    /// </summary>
    public sealed class AsPair<TKey, TValue> : IPair<TKey, TValue>
    {
        private readonly Lazy<KeyValuePair<TKey, Func<TValue>>> entry;
        private readonly Func<TValue> value;
        private readonly bool isLazy;

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public AsPair(TKey key, Func<TValue> value) : this(
            () => new KeyValuePair<TKey, Func<TValue>>(key, value),
            true
        )
        {   }

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public AsPair(TKey key, TValue value) : this(
            () => new KeyValuePair<TKey, Func<TValue>>(key, () => value),
            false
        )
        { }

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public AsPair(IScalar<KeyValuePair<TKey, TValue>> kvp) : this(kvp.Value)
        { }

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public AsPair(Func<KeyValuePair<TKey, TValue>> kvp) : this(() =>
            {
                var simple = kvp.Invoke();
                return new KeyValuePair<TKey, Func<TValue>>(simple.Key, () => simple.Value);
            },
            true
        )
        { }

        private AsPair(Func<KeyValuePair<TKey, Func<TValue>>> kvp, bool isLazy)
        {
            this.entry = new(kvp);
            this.value = () => this.entry.Value.Value.Invoke();
            this.isLazy = isLazy;
        }

        public TKey Key() => this.entry.Value.Key;
        public TValue Value() => this.value();
        public bool IsLazy() => this.isLazy;
    }

    public static partial class MapSmarts
    {
        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TValue> AsPair<TKey, TValue>(this (TKey key, Func<TValue> value) pair)
            => new AsPair<TKey, TValue>(pair.key, pair.value);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TValue> AsPair<TKey, TValue>(this (TKey key, TValue value) pair)
            => new AsPair<TKey, TValue>(pair.key, pair.value);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TValue> AsPair<TKey, TValue>(this TKey key, Func<TValue> value)
            => new AsPair<TKey, TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TKey> AsPair<TKey>(this TKey key, Action act)
            => new AsPair<TKey, TKey>(key, () =>
            {
                act();
                throw new ArgumentException("An action has been performed which returned no value.");
            });

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TValue> AsPair<TKey, TValue>(this IScalar<KeyValuePair<TKey, TValue>> kvp)
            => new AsPair<TKey, TValue>(kvp);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TValue> AsPair<TKey, TValue>(this KeyValuePair<TKey, TValue> kvp)
            => new AsPair<TKey, TValue>(() => kvp);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TValue> AsPair<TKey, TValue>(this Func<KeyValuePair<TKey, TValue>> kvp)
            => new AsPair<TKey, TValue>(kvp);
    }
}
