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
        private readonly Lazy<IDictionary<Key, IPair<Key, Value>>> map;

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value Key0
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9),
                AsPair._(key10, Key0)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value Key0,
            Key key11, Value Key1
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9),
                AsPair._(key10, Key0),
                AsPair._(key11, Key1)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value Key0,
            Key key11, Value Key1,
            Key key12, Value Key2
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9),
                AsPair._(key10, Key0),
                AsPair._(key11, Key1),
                AsPair._(key12, Key2)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value Key0,
            Key key11, Value Key1,
            Key key12, Value Key2,
            Key key13, Value Key3
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9),
                AsPair._(key10, Key0),
                AsPair._(key11, Key1),
                AsPair._(key12, Key2),
                AsPair._(key13, Key3)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value Key0,
            Key key11, Value Key1,
            Key key12, Value Key2,
            Key key13, Value Key3,
            Key key14, Value Key4
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9),
                AsPair._(key10, Key0),
                AsPair._(key11, Key1),
                AsPair._(key12, Key2),
                AsPair._(key13, Key3),
                AsPair._(key14, Key4)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value Key0,
            Key key11, Value Key1,
            Key key12, Value Key2,
            Key key13, Value Key3,
            Key key14, Value Key4,
            Key key15, Value Key5
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9),
                AsPair._(key10, Key0),
                AsPair._(key11, Key1),
                AsPair._(key12, Key2),
                AsPair._(key13, Key3),
                AsPair._(key14, Key4),
                AsPair._(key15, Key5)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value Key0,
            Key key11, Value Key1,
            Key key12, Value Key2,
            Key key13, Value Key3,
            Key key14, Value Key4,
            Key key15, Value Key5,
            Key key16, Value Key6
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9),
                AsPair._(key10, Key0),
                AsPair._(key11, Key1),
                AsPair._(key12, Key2),
                AsPair._(key13, Key3),
                AsPair._(key14, Key4),
                AsPair._(key15, Key5),
                AsPair._(key16, Key6)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(IPair<Key, Value> item, params IPair<Key, Value>[] more) : this(
            Enumerable.Joined._(
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
        /// <param name="input">input dictionary</param>
        public AsMap(IEnumerable<IPair<Key, Value>> pairs) : this(
            () => pairs
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public AsMap(Func<IEnumerable<IPair<Key, Value>>> pairs)
        {
            this.map = new Lazy<IDictionary<Key, IPair<Key, Value>>>(
                () =>
            {
                var dict = new Dictionary<Key, IPair<Key, Value>>();
                foreach (var pair in pairs())
                {
                    dict[pair.Key()] = pair;
                }
                return dict;
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
            this.map.Value[pair.Key()] = pair;
            return this;
        }
    }

    public static class AsMap
    {
        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<string, string> _(
            string key, string value, params string[] pairs
        )
        => new AsMap<string, string>(
            AsEnumerable._(() =>
            {
                var enumerator = AsEnumerable._(pairs).GetEnumerator();
                var current = 0;
                var result = new List<IPair<string, string>>();
                result.Add(AsPair._(key, value));
                while(enumerator.MoveNext())
                {
                    if(++current % 2 != 0) //even
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
            Key key1, Value value1,
            Key key2, Value value2
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value value10
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value value10,
            Key key11, Value value11
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value value10,
            Key key11, Value value11,
            Key key12, Value value12
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value value10,
            Key key11, Value value11,
            Key key12, Value value12,
            Key key13, Value value13
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12,
            key13, value13
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value value10,
            Key key11, Value value11,
            Key key12, Value value12,
            Key key13, Value value13,
            Key key14, Value value14
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12,
            key13, value13,
            key14, value14
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value value10,
            Key key11, Value value11,
            Key key12, Value value12,
            Key key13, Value value13,
            Key key14, Value value14,
            Key key15, Value value15
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12,
            key13, value13,
            key14, value14,
            key15, value15
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value value10,
            Key key11, Value value11,
            Key key12, Value value12,
            Key key13, Value value13,
            Key key14, Value value14,
            Key key15, Value value15,
            Key key16, Value value16
        )
        => new AsMap<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12,
            key13, value13,
            key14, value14,
            key15, value15,
            key16, value16
        );

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public static AsMap<Key, Value> _<Key, Value>(IPair<Key, Value> item, params IPair<Key, Value>[] more)
            => new AsMap<Key, Value>(item, more);

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public static AsMap<Key, Value> _<Key, Value>(IEnumerable<IPair<Key, Value>> entries)
            => new AsMap<Key, Value>(entries);

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public static AsMap<Key, Value> _<Key, Value>(Func<IEnumerable<IPair<Key, Value>>> entries)
            => new AsMap<Key, Value>(entries);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public static AsMap<Key, Value> _<Key, Value>(params IMapInput<Key, Value>[] inputs)
            => new AsMap<Key, Value>(inputs);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public static AsMap<Key, Value> _<Key, Value>(IEnumerable<IMapInput<Key, Value>> inputs)
            => new AsMap<Key, Value>(inputs);


    }
}
