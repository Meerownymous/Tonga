

using System;
using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.Map
{
    /// <summary>
    /// Key-value pair made of strings.
    /// Since 9.9.2019
    /// </summary>
    public sealed class KvpOf : IKvp
    {
        private readonly AsScalar<KeyValuePair<string, System.Func<string>>> entry;
        private readonly AsScalar<string> value;
        private readonly bool isLazy;

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public KvpOf(IText key, System.Func<string> value)
            : this(key.AsString(), value)
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public KvpOf(IText key, string value)
            : this(key.AsString(), value)
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public KvpOf(string key, System.Func<string> value) : this(
            () => new KeyValuePair<string, System.Func<string>>(key, value),
            true
        )
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public KvpOf(string key, string value) : this(
            () => new KeyValuePair<string, System.Func<string>>(key, () => value),
            false
        )
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public KvpOf(IScalar<KeyValuePair<string, string>> kvp)
            : this(() => kvp.Value())
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public KvpOf(System.Func<KeyValuePair<string, string>> kvp)
            : this(() =>
            {
                var simple = kvp.Invoke();
                return new KeyValuePair<string, System.Func<string>>(simple.Key, () => simple.Value);
            }, true)
        { }

        private KvpOf(System.Func<KeyValuePair<string, System.Func<string>>> kvp, bool isLazy)
        {
            this.entry =
                new AsScalar<KeyValuePair<string, System.Func<string>>>(
                    () => kvp.Invoke()
                );
            this.value = new AsScalar<string>((System.Func<string>)(() => this.entry.Value().Value.Invoke()));
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
        public static IKvp<TValue> New<TValue>(IText key, System.Func<TValue> value)
            => new KvpOf<TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public static IKvp<TValue> New<TValue>(IText key, TValue value)
            => new KvpOf<TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public static IKvp<TValue> New<TValue>(string key, System.Func<TValue> value)
            => new KvpOf<TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public static IKvp<TValue> New<TValue>(string key, TValue value)
            => new KvpOf<TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public static IKvp<TValue> New<TValue>(IScalar<KeyValuePair<string, TValue>> kvp)
            => new KvpOf<TValue>(kvp);

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public static IKvp<TValue> New<TValue>(System.Func<KeyValuePair<string, TValue>> kvp)
            => new KvpOf<TValue>(kvp);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IKvp<TKey, TValue> New<TKey, TValue>(TKey key, System.Func<TValue> value)
            => new KvpOf<TKey, TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IKvp<TKey, TValue> New<TKey, TValue>(TKey key, TValue value)
            => new KvpOf<TKey, TValue>(key, value);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IKvp<TKey, TValue> New<TKey, TValue>(IScalar<KeyValuePair<TKey, TValue>> kvp)
            => new KvpOf<TKey, TValue>(kvp);

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public static IKvp<TKey, TValue> New<TKey, TValue>(System.Func<KeyValuePair<TKey, TValue>> kvp)
            => new KvpOf<TKey, TValue>(kvp);
    }

    /// <summary>
    /// Key-value pair matching a string to specified type value.
    /// </summary>
    public sealed class KvpOf<TValue> : IKvp<TValue>
    {
        private readonly Sticky<KeyValuePair<string, Func<TValue>>> entry;
        private readonly Sticky<TValue> value;
        private readonly bool isLazy;

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(IText key, System.Func<TValue> value)
            : this(key.AsString(), value)
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(IText key, TValue value)
            : this(key.AsString(), value)
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(string key, System.Func<TValue> value) : this(
            () => new KeyValuePair<string, System.Func<TValue>>(key, value),
            true
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(string key, TValue value) : this(
            () => new KeyValuePair<string, System.Func<TValue>>(key, () => value),
            false
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(IScalar<KeyValuePair<string, TValue>> kvp)
            : this(() => kvp.Value())
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(System.Func<KeyValuePair<string, TValue>> kvp)
            : this(() =>
            {
                var simple = kvp.Invoke();
                return new KeyValuePair<string, System.Func<TValue>>(simple.Key, () => simple.Value);
            }, true)
        { }

        private KvpOf(System.Func<KeyValuePair<string, System.Func<TValue>>> kvp, bool isLazy)
        {
            this.entry = Sticky._(kvp.Invoke);
            this.value = Sticky._((() => this.entry.Value().Value.Invoke()));
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
    public sealed class KvpOf<TKey, TValue> : IKvp<TKey, TValue>
    {
        private readonly IScalar<KeyValuePair<TKey, Func<TValue>>> entry;
        private readonly IScalar<TValue> value;
        private readonly bool isLazy;

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public KvpOf(TKey key, Func<TValue> value) : this(
            () => new KeyValuePair<TKey, Func<TValue>>(key, value),
            true
        )
        { }

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public KvpOf(TKey key, TValue value) : this(
            () => new KeyValuePair<TKey, Func<TValue>>(key, () => value),
            false
        )
        { }

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public KvpOf(IScalar<KeyValuePair<TKey, TValue>> kvp)
            : this(kvp.Value)
        { }

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public KvpOf(Func<KeyValuePair<TKey, TValue>> kvp)
            : this(() =>
            {
                var simple = kvp.Invoke();
                return new KeyValuePair<TKey, Func<TValue>>(simple.Key, () => simple.Value);
            }, true)
        { }

        private KvpOf(System.Func<KeyValuePair<TKey, System.Func<TValue>>> kvp, bool isLazy)
        {
            this.entry = Sticky._(kvp.Invoke);
            this.value = Sticky._(() => this.entry.Value().Value.Invoke());
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
