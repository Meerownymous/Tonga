using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Func;
using Tonga.Scalar;

namespace Tonga.Map
{
    /// <summary>
    /// A map from string to typed value.
    /// </summary>
    public sealed class AsMap<Key, Value> : IMap<Key, Value>
    {
        private readonly object lck;
        private readonly Lazy<IDictionary<Key, IPair<Key, Value>>> map;

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(params (Key key, Value value)[] pairs
        ) : this(
            Mapped._(
                pair => AsPair._(pair.key, pair.value),
                pairs
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(IPair<Key, Value> item, params IPair<Key, Value>[] more) : this(
            Joined._(
                AsEnumerable._(more),
                item
            )
        )
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public AsMap(params IMapInput<Key, Value>[] inputs) : this(
            AsEnumerable._(inputs)
        )
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public AsMap(IEnumerable<IMapInput<Key, Value>> inputs) : this(
            () =>
            {
                IMap<Key, Value> map = new Empty<Key, Value>();
                Each._(
                    input => map = input.Merged(map),
                    inputs
                ).Invoke();
                return map.Pairs();
            }
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="pairs">map content</param>
        public AsMap(IEnumerable<IPair<Key, Value>> pairs) : this(
            () => pairs
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="pairs">map content</param>
        public AsMap(Func<IEnumerable<IPair<Key, Value>>> pairs)
        {
            this.lck = new object();
            this.map = new Lazy<IDictionary<Key, IPair<Key, Value>>>(
                () =>
                {
                    lock (this.lck)
                    {
                        var dict = new Dictionary<Key, IPair<Key, Value>>();
                        foreach (var pair in pairs())
                        {
                            dict[pair.Key()] = pair;
                        }
                        return dict;
                    }
                });
        }

        public Value this[Key key] =>
            ExceptionSwap._(
                () => this.map.Value[key].Value(),
                (ex) => new ArgumentException(ex.Message, ex)
            ).Value();

        public Func<Value> Lazy(Key key)
        {
            return () => this.map.Value[key].Value();
        }

        public ICollection<Key> Keys()
        {
            return this.map.Value.Keys;
        }

        public IEnumerable<IPair<Key, Value>> Pairs()
        {
            return this.map.Value.Values;
        }

        public IMap<Key, Value> With(IPair<Key, Value> pair)
        {
            lock (this.lck)
            {
                this.map.Value[pair.Key()] = pair;
            }
            return this;
        }
    }

    public static class AsMap
    {
        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<string, string> _(
            params string[] pairs
        )
        => new AsMap<string, string>(
            AsEnumerable._(() =>
            {
                var enumerator = AsEnumerable._(pairs).GetEnumerator();
                var current = 0;
                var result = new List<IPair<string, string>>();
                var key = string.Empty;
                var value = string.Empty;
                while (enumerator.MoveNext())
                {
                    if (++current % 2 != 0) //even
                    {
                        key = enumerator.Current;
                    }
                    else
                    {
                        value = enumerator.Current;
                        result.Add(AsPair._(key, value));
                    }
                }

                if (current % 2 != 0)
                    throw new ArgumentException(
                        "Making a map from a key-value sequence requires an even item count of the sequence. "
                        + $"Received only {current} items.");
                return result;
            })
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            params (Key key1, Value value1)[] pairs
        ) => new(pairs);

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(IPair<Key, Value> item, params IPair<Key, Value>[] more)
            => new(item, more);

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public static AsMap<Key, Value> _<Key, Value>(IEnumerable<IPair<Key, Value>> entries)
            => new(entries);

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public static AsMap<Key, Value> _<Key, Value>(Func<IEnumerable<IPair<Key, Value>>> entries)
            => new(entries);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public static AsMap<Key, Value> _<Key, Value>(params IMapInput<Key, Value>[] inputs)
            => new(inputs);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public static AsMap<Key, Value> _<Key, Value>(IEnumerable<IMapInput<Key, Value>> inputs)
            => new(inputs);
    }
}
