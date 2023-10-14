

using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Map
{
    /// <summary>
    /// A key to many strings.
    /// Since 1.9.2019
    /// </summary>
    public sealed class KvpOfMany : KvpEnvelope<IEnumerable<string>>
    {
        /// <summary>
        /// A key to many values.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KvpOfMany(string key, params Func<string>[] many) : this(key, () =>
            {
                var lst = new List<string>();
                for (var i = 0; i < many.Length; i++)
                {
                    lst.Add(many[i]());
                }
                return lst;
            })
        { }

        /// <summary>
        /// A key to many values.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KvpOfMany(string key, params string[] values) : this(key, () => Params.Of(values))
        { }

        /// <summary>
        /// A key to many values.
        /// </summary>
        public KvpOfMany(string key, IEnumerable<string> values) : this(key, () => values)
        { }

        /// <summary>
        /// A key to many strings.
        /// The function is executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KvpOfMany(string key, Func<IEnumerable<string>> values) : base(
            new KvpOf<IEnumerable<string>>(key, values)
        )
        { }

        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public static IKvp<IEnumerable<TValue>> New<TValue>(string key, params Func<TValue>[] values)
            => new KvpOfMany<TValue>(key, values);

        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public static IKvp<IEnumerable<TValue>> New<TValue>(string key, params TValue[] values)
            => new KvpOfMany<TValue>(key, values);

        /// <summary>
        /// A key to many strings.
        /// </summary>
        public static IKvp<IEnumerable<TValue>> New<TValue>(string key, IEnumerable<TValue> values)
            => new KvpOfMany<TValue>(key, values);

        /// <summary>
        /// A key to many values.
        /// </summary>
        public static IKvp<IEnumerable<TValue>> New<TValue>(string key, Func<IEnumerable<TValue>> values)
            => new KvpOfMany<TValue>(key, values);

        /// <summary>
        /// A key to many values.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public static IKvp<TKey, IEnumerable<TValue>> New<TKey, TValue>(TKey key, params Func<TValue>[] many)
            => new KeyToValues<TKey, TValue>(key, many);

        /// <summary>
        /// A key to many values.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public static IKvp<TKey, IEnumerable<TValue>> New<TKey, TValue>(TKey key, params TValue[] many)
            => new KeyToValues<TKey, TValue>(key, many);

        /// <summary>
        /// A key to many values.
        /// </summary>
        public static IKvp<TKey, IEnumerable<TValue>> New<TKey, TValue>(TKey key, IEnumerable<TValue> many)
            => new KeyToValues<TKey, TValue>(key, many);

        /// <summary>
        /// A key to many values.
        /// </summary>
        public static IKvp<TKey, IEnumerable<TValue>> New<TKey, TValue>(TKey key, Func<IEnumerable<TValue>> many)
            => new KeyToValues<TKey, TValue>(key, many);
    }

    /// <summary>
    /// A key to many values.
    /// </summary>
    public sealed class KvpOfMany<TValue> : KvpEnvelope<IEnumerable<TValue>>
    {
        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KvpOfMany(string key, params Func<TValue>[] values) : this(key, () =>
            {
                var lst = new List<TValue>();
                for (var i = 0; i < values.Length; i++)
                {
                    lst.Add(values[i]());
                }
                return lst;
            }
        )
        { }

        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KvpOfMany(string key, params TValue[] values) : this(key, () =>
            {
                var lst = new List<TValue>();
                for (var i = 0; i < values.Length; i++)
                {
                    lst.Add(values[i]);
                }
                return lst;
            }
        )
        { }

        /// <summary>
        /// A key to many strings.
        /// </summary>
        public KvpOfMany(string key, IEnumerable<TValue> values) : this(key, () => values)
        { }

        /// <summary>
        /// A key to many values.
        /// </summary>
        public KvpOfMany(string key, Func<IEnumerable<TValue>> values) : base(
            new KvpOf<IEnumerable<TValue>>(key, values)
        )
        { }
    }

    /// <summary>
    /// A key to many values.
    /// </summary>
    public sealed class KeyToValues<TKey, TValue> : KvpEnvelope<TKey, IEnumerable<TValue>>
    {
        /// <summary>
        /// A key to many values.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KeyToValues(TKey key, params Func<TValue>[] many) : this(key, () =>
        {
            var lst = new List<TValue>();
            for (var i = 0; i < many.Length; i++)
            {
                lst.Add(many[i]());
            }
            return lst;
        }
        )
        { }

        /// <summary>
        /// A key to many values.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KeyToValues(TKey key, params TValue[] many) : this(key, () =>
        {
            var lst = new List<TValue>();
            for (var i = 0; i < many.Length; i++)
            {
                lst.Add(many[i]);
            }
            return lst;
        }
        )
        { }

        /// <summary>
        /// A key to many values.
        /// </summary>
        public KeyToValues(TKey key, IEnumerable<TValue> many) : this(key, () => many)
        { }

        /// <summary>
        /// A key to many values.
        /// </summary>
        public KeyToValues(TKey key, Func<IEnumerable<TValue>> many) : base(
            new KvpOf<TKey, IEnumerable<TValue>>(key, many)
        )
        { }
    }
}
