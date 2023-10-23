

using System;
using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.Map
{
    /// <summary>
    /// Key-value pair made of strings.
    /// Since 9.9.2019
    /// </summary>
    public sealed class AsPair : IPair
    {
        private readonly Sticky<KeyValuePair<string, Func<string>>> entry;
        private readonly Sticky<string> value;
        private readonly bool isLazy;

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public AsPair(IText key, Func<string> value) : this(key.AsString(), value)
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public AsPair(IText key, string value) : this(key.AsString(), value)
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public AsPair(string key, Func<string> value) : this(
            () => new KeyValuePair<string, Func<string>>(key, value),
            true
        )
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public AsPair(string key, string value) : this(
            () => new KeyValuePair<string, Func<string>>(key, () => value),
            false
        )
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public AsPair(IScalar<KeyValuePair<string, string>> kvp)
            : this(kvp.Value)
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public AsPair(Func<KeyValuePair<string, string>> kvp) : this(() =>
            {
                var simple = kvp.Invoke();
                return new KeyValuePair<string, Func<string>>(simple.Key, () => simple.Value);
            },
            true
        )
        { }

        private AsPair(Func<KeyValuePair<string, Func<string>>> pair, bool isLazy)
        {
            this.entry = Scalar.Sticky._(pair.Invoke);
            this.value = Scalar.Sticky._(() => this.entry.Value().Value.Invoke());
            this.isLazy = isLazy;
        }

        public string Key()
        {
            return this.entry.Value().Key;
        }

        public string Value()
        {
            return this.value.Value();
        }

        public bool IsLazy()
        {
            return this.isLazy;
        }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public static IPair<TValue> _<TValue>(IText key, Func<TValue> value)
            => new AsPair<TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public static IPair<TValue> _<TValue>(IText key, TValue value)
            => new AsPair<TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public static IPair<TValue> _<TValue>(string key, Func<TValue> value)
            => new AsPair<TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public static IPair<TValue> _<TValue>(string key, TValue value)
            => new AsPair<TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public static IPair<TValue> _<TValue>(IScalar<KeyValuePair<string, TValue>> kvp)
            => new AsPair<TValue>(kvp);

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public static IPair<TValue> _<TValue>(Func<KeyValuePair<string, TValue>> kvp)
            => new AsPair<TValue>(kvp);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TValue> _<TKey, TValue>(TKey key, Func<TValue> value)
            => new AsPair<TKey, TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TValue> _<TKey, TValue>(TKey key, TValue value)
            => new AsPair<TKey, TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TValue> _<TKey, TValue>(IScalar<KeyValuePair<TKey, TValue>> kvp)
            => new AsPair<TKey, TValue>(kvp);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IPair<TKey, TValue> _<TKey, TValue>(Func<KeyValuePair<TKey, TValue>> kvp)
            => new AsPair<TKey, TValue>(kvp);
    }

    /// <summary>
    /// Key-value pair matching a string to specified type value.
    /// </summary>
    public sealed class AsPair<TValue> : IPair<TValue>
    {
        private readonly Sticky<KeyValuePair<string, Func<TValue>>> entry;
        private readonly Sticky<TValue> value;
        private readonly bool isLazy;

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public AsPair(IText key, Func<TValue> value)
            : this(key.AsString(), value)
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public AsPair(IText key, TValue value)
            : this(key.AsString(), value)
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public AsPair(string key, Func<TValue> value) : this(
            () => new KeyValuePair<string, Func<TValue>>(key, value),
            true
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public AsPair(string key, TValue value) : this(
            () => new KeyValuePair<string, Func<TValue>>(key, () => value),
            false
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public AsPair(IScalar<KeyValuePair<string, TValue>> kvp) : this(kvp.Value)
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public AsPair(Func<KeyValuePair<string, TValue>> kvp) : this(() =>
            {
                var simple = kvp.Invoke();
                return new KeyValuePair<string, Func<TValue>>(simple.Key, () => simple.Value);
            }, true)
        { }

        private AsPair(Func<KeyValuePair<string, Func<TValue>>> kvp, bool isLazy)
        {
            this.entry = Scalar.Sticky._(kvp.Invoke);
            this.value = Scalar.Sticky._(() => this.entry.Value().Value.Invoke());
            this.isLazy = isLazy;
        }

        public string Key()
        {
            return this.entry.Value().Key;
        }

        public TValue Value()
        {
            return this.value.Value();
        }

        public bool IsLazy()
        {
            return this.isLazy;
        }
    }

    /// <summary>
    /// Key-value pair matching a key type to specified type value.
    /// </summary>
    public sealed class AsPair<TKey, TValue> : IPair<TKey, TValue>
    {
        private readonly IScalar<KeyValuePair<TKey, Func<TValue>>> entry;
        private readonly IScalar<TValue> value;
        private readonly bool isLazy;

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public AsPair(TKey key, Func<TValue> value) : this(
            () => new KeyValuePair<TKey, Func<TValue>>(key, value),
            true
        )
        { }

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
        public AsPair(Func<KeyValuePair<TKey, TValue>> kvp)
            : this(() =>
            {
                var simple = kvp.Invoke();
                return new KeyValuePair<TKey, Func<TValue>>(simple.Key, () => simple.Value);
            }, true)
        { }

        private AsPair(Func<KeyValuePair<TKey, Func<TValue>>> kvp, bool isLazy)
        {
            this.entry = Scalar.Sticky._(kvp.Invoke);
            this.value = Scalar.Sticky._(() => this.entry.Value().Value.Invoke());
            this.isLazy = isLazy;
        }

        public TKey Key()
        {
            return this.entry.Value().Key;
        }

        public TValue Value()
        {
            return this.value.Value();
        }

        public bool IsLazy()
        {
            return this.isLazy;
        }
    }
}
