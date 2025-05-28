using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Map
{
    /// <summary>
    /// A key to many values.
    /// </summary>
    public sealed class OneToMany<TKey, TValue> : PairEnvelope<TKey, IEnumerable<TValue>>
    {
        /// <summary>
        /// A key to many values.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public OneToMany(TKey key, params Func<TValue>[] many) : this(key, () =>
        {
            var lst = new List<TValue>();
            foreach (var item in many)
            {
                lst.Add(item());
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
            () => many.AsEnumerable()
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
            (key, many).AsPair()
        )
        { }
    }

    /// <summary>
    /// A key to many strings.
    /// </summary>
    public static class OneToMany
    {
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
}
