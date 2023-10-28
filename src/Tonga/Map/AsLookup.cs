using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Map
{
/// <summary>
    /// A map from string to typed value.
    /// </summary>
    public sealed class AsLookup<Key, Value> : IMap<Key, Value>
    {
        private readonly Lazy<IDictionary<Key, IPair<Key, Value>>> map;

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
            Key key1, Value Key,
            Key key2, Value value2
        ) : this(
            AsEnumerable._(
                AsPair2._(key1, Key),
                AsPair2._(key2, value2)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3
        ) : this(
            AsEnumerable._(
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4
        ) : this(
            AsEnumerable._(
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5
        ) : this(
            AsEnumerable._(
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6
        ) : this(
            AsEnumerable._(
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5),
                AsPair2._(key6, value6)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7
        ) : this(
            AsEnumerable._(
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5),
                AsPair2._(key6, value6),
                AsPair2._(key7, value7)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
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
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5),
                AsPair2._(key6, value6),
                AsPair2._(key7, value7),
                AsPair2._(key8, value8)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
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
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5),
                AsPair2._(key6, value6),
                AsPair2._(key7, value7),
                AsPair2._(key8, value8),
                AsPair2._(key9, value9)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
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
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5),
                AsPair2._(key6, value6),
                AsPair2._(key7, value7),
                AsPair2._(key8, value8),
                AsPair2._(key9, value9),
                AsPair2._(key10, Key0)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
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
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5),
                AsPair2._(key6, value6),
                AsPair2._(key7, value7),
                AsPair2._(key8, value8),
                AsPair2._(key9, value9),
                AsPair2._(key10, Key0),
                AsPair2._(key11, Key1)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
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
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5),
                AsPair2._(key6, value6),
                AsPair2._(key7, value7),
                AsPair2._(key8, value8),
                AsPair2._(key9, value9),
                AsPair2._(key10, Key0),
                AsPair2._(key11, Key1),
                AsPair2._(key12, Key2)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
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
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5),
                AsPair2._(key6, value6),
                AsPair2._(key7, value7),
                AsPair2._(key8, value8),
                AsPair2._(key9, value9),
                AsPair2._(key10, Key0),
                AsPair2._(key11, Key1),
                AsPair2._(key12, Key2),
                AsPair2._(key13, Key3)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
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
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5),
                AsPair2._(key6, value6),
                AsPair2._(key7, value7),
                AsPair2._(key8, value8),
                AsPair2._(key9, value9),
                AsPair2._(key10, Key0),
                AsPair2._(key11, Key1),
                AsPair2._(key12, Key2),
                AsPair2._(key13, Key3),
                AsPair2._(key14, Key4)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
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
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5),
                AsPair2._(key6, value6),
                AsPair2._(key7, value7),
                AsPair2._(key8, value8),
                AsPair2._(key9, value9),
                AsPair2._(key10, Key0),
                AsPair2._(key11, Key1),
                AsPair2._(key12, Key2),
                AsPair2._(key13, Key3),
                AsPair2._(key14, Key4),
                AsPair2._(key15, Key5)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(
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
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5),
                AsPair2._(key6, value6),
                AsPair2._(key7, value7),
                AsPair2._(key8, value8),
                AsPair2._(key9, value9),
                AsPair2._(key10, Key0),
                AsPair2._(key11, Key1),
                AsPair2._(key12, Key2),
                AsPair2._(key13, Key3),
                AsPair2._(key14, Key4),
                AsPair2._(key15, Key5),
                AsPair2._(key16, Key6)
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsLookup(IPair<Key, Value> item, params IPair<Key, Value>[] more) : this(
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
        public AsLookup(params ILookupInput<Key, Value>[] inputs) : this(
            AsEnumerable._(inputs)
        )
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public AsLookup(IEnumerable<ILookupInput<Key, Value>> inputs) : this(
            () =>
            {
                IMap<Key, Value> map = new EmptyLookup<Key, Value>();
                foreach (ILookupInput<Key, Value> input in inputs)
                {
                    map = input.Merged(map);
                }
                return map.Pairs();
            }
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public AsLookup(IEnumerable<IPair<Key, Value>> pairs) : this(
            () => pairs
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public AsLookup(Func<IEnumerable<IPair<Key, Value>>> pairs)
        {
            this.map = new Lazy<IDictionary<Key,IPair<Key,Value>>>(() =>
            {
                var dict = new Dictionary<Key, IPair<Key, Value>>();
                foreach(var pair in pairs())
                {
                    dict[pair.Key()] = pair;
                }
                return dict;
            });
        }

        public Value this[Key key] => this.map.Value[key].Value();

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

    public static class AsLookup
    {
        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsLookup<string, string> _(
            params string[] pairs
        )
        => new AsLookup<string, string>(
            AsEnumerable._(() =>
            {
                var enumerator = AsEnumerable._(pairs).GetEnumerator();
                var current = 0;
                string key = string.Empty;
                string value = string.Empty;
                var result = new List<IPair<string, string>>();
                while(enumerator.MoveNext())
                {
                    if(current % 2 != 0) //even
                    {
                        key = enumerator.Current;
                    }
                    else
                    {
                        value = enumerator.Current;
                        result.Add(AsPair._<string,string>(key, value));
                    }
                }
                return result;
            })
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsLookup<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2
        )
        => new AsLookup<Key, Value>(
            key1, value1,
            key2, value2
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsLookup<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3
        )
        => new AsLookup<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsLookup<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4
        )
        => new AsLookup<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsLookup<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5
        )
        => new AsLookup<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static AsLookup<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6
        )
        => new AsLookup<Key, Value>(
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
        public static AsLookup<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7
        )
        => new AsLookup<Key, Value>(
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
        public static AsLookup<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8
        )
        => new AsLookup<Key, Value>(
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
        public static AsLookup<Key, Value> _<Key, Value>(
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
        => new AsLookup<Key, Value>(
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
        public static AsLookup<Key, Value> _<Key, Value>(
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
        => new AsLookup<Key, Value>(
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
        public static AsLookup<Key, Value> _<Key, Value>(
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
        => new AsLookup<Key, Value>(
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
        public static AsLookup<Key, Value> _<Key, Value>(
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
        => new AsLookup<Key, Value>(
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
        public static AsLookup<Key, Value> _<Key, Value>(
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
        => new AsLookup<Key, Value>(
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
        public static AsLookup<Key, Value> _<Key, Value>(
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
        => new AsLookup<Key, Value>(
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
        public static AsLookup<Key, Value> _<Key, Value>(
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
        => new AsLookup<Key, Value>(
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
        public static AsLookup<Key, Value> _<Key, Value>(
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
        => new AsLookup<Key, Value>(
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
        public static AsLookup<Key, Value> _<Key, Value>(IPair<Key, Value> item, params IPair<Key, Value>[] more)
            => new AsLookup<Key, Value>(item, more);

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public static AsLookup<Key, Value> _<Key, Value>(IEnumerable<IPair<Key, Value>> entries)
            => new AsLookup<Key, Value>(entries);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public static AsLookup<Key, Value> _<Key, Value>(params ILookupInput<Key, Value>[] inputs)
            => new AsLookup<Key, Value>(inputs);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public static AsLookup<Key, Value> _<Key, Value>(IEnumerable<ILookupInput<Key, Value>> inputs)
            => new AsLookup<Key, Value>(inputs);


    }
}
