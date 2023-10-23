

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
        public KvpOfMany(string key, params string[] values) : this(key, () => AsEnumerable._(values))
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
            new AsPair<IEnumerable<string>>(key, values)
        )
        { }

        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public static IPair<IEnumerable<TValue>> _<TValue>(string key, params Func<TValue>[] values)
            => new OneToMany<TValue>(key, values);

        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public static IPair<IEnumerable<TValue>> _<TValue>(string key, params TValue[] values)
            => new OneToMany<TValue>(key, values);

        /// <summary>
        /// A key to many strings.
        /// </summary>
        public static IPair<IEnumerable<TValue>> _<TValue>(string key, IEnumerable<TValue> values)
            => new OneToMany<TValue>(key, values);

        /// <summary>
        /// A key to many values.
        /// </summary>
        public static IPair<IEnumerable<TValue>> _<TValue>(string key, Func<IEnumerable<TValue>> values)
            => new OneToMany<TValue>(key, values);

        /// <summary>
        /// A key to many values.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public static IPair<TKey, IEnumerable<TValue>> _<TKey, TValue>(TKey key, params Func<TValue>[] many)
            => new OneToMany<TKey, TValue>(key, many);

        /// <summary>
        /// A key to many values.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public static IPair<TKey, IEnumerable<TValue>> _<TKey, TValue>(TKey key, params TValue[] many)
            => new OneToMany<TKey, TValue>(key, many);

        /// <summary>
        /// A key to many values.
        /// </summary>
        public static IPair<TKey, IEnumerable<TValue>> _<TKey, TValue>(TKey key, IEnumerable<TValue> many)
            => new OneToMany<TKey, TValue>(key, many);

        /// <summary>
        /// A key to many values.
        /// </summary>
        public static IPair<TKey, IEnumerable<TValue>> _<TKey, TValue>(TKey key, Func<IEnumerable<TValue>> many)
            => new OneToMany<TKey, TValue>(key, many);
    }

    /// <summary>
    /// A key to many values.
    /// </summary>
    public sealed class OneToMany<TValue> : KvpEnvelope<IEnumerable<TValue>>
    {
        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public OneToMany(string key, params Func<TValue>[] values) : this(key, () =>
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
        public OneToMany(string key, params TValue[] values) : this(key, () =>
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
        public OneToMany(string key, IEnumerable<TValue> values) : this(key, () => values)
        { }

        /// <summary>
        /// A key to many values.
        /// </summary>
        public OneToMany(string key, Func<IEnumerable<TValue>> values) : base(
            AsPair._(key, values)
        )
        { }
    }

    /// <summary>
    /// A key to many values.
    /// </summary>
    public sealed class OneToMany<TKey, TValue> : KvpEnvelope<TKey, IEnumerable<TValue>>
    {
        /// <summary>
        /// A key to many values.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public OneToMany(TKey key, params Func<TValue>[] many) : this(key, () =>
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
        public OneToMany(TKey key, params TValue[] many) : this(
            key,
            () => AsEnumerable._(many)
        )
        { }

        /// <summary>
        /// A key to many values.
        /// </summary>
        public OneToMany(TKey key, IEnumerable<TValue> many) : this(
            key, () => many
        )
        { }

        /// <summary>
        /// A key to many values.
        /// </summary>
        public OneToMany(TKey key, Func<IEnumerable<TValue>> many) : base(
            AsPair._(key, many)
        )
        { }
    }
}
