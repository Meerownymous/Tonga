using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Map
{
/// <summary>
    /// A map from string to typed value.
    /// </summary>
    public sealed class AsMap2<Key, Value> : MapEnvelope2<Key, Value>
    {
        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
            Key key1, Value Key,
            Key key2, Value value2,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair2._(key1, Key),
                AsPair2._(key2, value2)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5),
                AsPair2._(key6, value6)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair2._(key1, Key),
                AsPair2._(key2, value2),
                AsPair2._(key3, value3),
                AsPair2._(key4, value4),
                AsPair2._(key5, value5),
                AsPair2._(key6, value6),
                AsPair2._(key7, value7)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
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
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
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
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
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
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
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
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
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
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
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
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(
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
            Key key16, Value Key6,
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap2(KeyValuePair<Key, Value> item, params KeyValuePair<Key, Value>[] more) : this(
            Enumerable.Joined._(
                AsEnumerable._(more),
                item
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs and appends them to the given Dictionary.
        /// </summary>
        /// <param name="src">source dictionary</param>
        /// <param name="list">KeyValuePairs to append</param>
        public AsMap2(IDictionary<Key, Value> src, params KeyValuePair<Key, Value>[] list) : this(
            src,
            AsEnumerable._(list)
        )
        { }

        /// <summary>
        /// A map by merging the given KeyValuePairs to the given Dictionary.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="list"></param>
        public AsMap2(IDictionary<Key, Value> src, IEnumerable<KeyValuePair<Key, Value>> list) : this(
            Enumerable.Joined._(
                src,
                list
            )
        )
        { }

        /// <summary>
        /// A map by taking the given entries.
        /// </summary>
        /// <param name="entries">enumerator of KeyValuePairs</param>
        public AsMap2(IEnumerator<KeyValuePair<Key, Value>> entries) : this(
            AsEnumerable._(() => entries))
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        public AsMap2(IPair<Key, Value> entry, params IPair<Key, Value>[] more) : this(
            Enumerable.Joined._(
                    AsEnumerable._(entry),
                    more
            ),
            true
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="pairs">enumerable of kvps</param>
        /// <param name="rejectBuildingAllValues">if you have pairs with value functions,
        /// it is by default prevented to build all values by getting the enumerator.
        /// You can deactivate that here.</param>
        public AsMap2(IEnumerable<IPair<Key, Value>> pairs, bool rejectBuildingAllValues = true) : this(() =>
            {
                var lazy = false;
                foreach(var pair in pairs)
                {
                    if(pair.IsLazy())
                    {
                        lazy = true;
                    }
                }
                return lazy ? LazyMap._(pairs, rejectBuildingAllValues) : AsMap2._(pairs);
            }
            
        )
        { }

        /// <summary>
        /// A map from another map.
        /// </summary>
        /// <param name="dict">enumerable of entries</param>
        public AsMap2(IDictionary<Key, Value> dict) : this(() => dict)
        //caution: do not remove this ctor. It is a "proxy" ctor to prevent copying values. 
        //Because a map is also a IEnumerable of KeyValuePairs, the ctor accepting the enumerable would copy the map.
        { }

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public AsMap2(IEnumerable<KeyValuePair<Key, Value>> entries) : this(
            () =>
            {
                var temp = new Dictionary<Key, Value>();
                foreach (var entry in entries)
                {
                    temp[entry.Key] = entry.Value;
                }
                return temp; //values cannot change, so it is sticky.
            }
        )
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public AsMap2(params IMapInput<Key, Value>[] inputs) : this(
            AsEnumerable._(inputs)
        )
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public AsMap2(IEnumerable<IMapInput<Key, Value>> inputs) : this(
            () =>
            {
                IDictionary<Key, Value> dict = new Dictionary<Key, Value>();
                foreach (IMapInput<Key, Value> input in inputs)
                {
                    dict = input.Apply(dict);
                }
                return dict;
            }
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public AsMap2(Func<IDictionary<Key, Value>> input) : base(
            input
        )
        { }
    }

    public static class AsMap2
    {
        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2
        )
        => new AsMap2<Key, Value>(
            key1, value1,
            key2, value2
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3
        )
        => new AsMap2<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4
        )
        => new AsMap2<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5
        )
        => new AsMap2<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6
        )
        => new AsMap2<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7
        )
        => new AsMap2<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8
        )
        => new AsMap2<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
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
        => new AsMap2<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
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
        => new AsMap2<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
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
        => new AsMap2<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
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
        => new AsMap2<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
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
        => new AsMap2<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
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
        => new AsMap2<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
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
        => new AsMap2<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
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
        => new AsMap2<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(KeyValuePair<Key, Value> item, params KeyValuePair<Key, Value>[] more)
            => new AsMap2<Key, Value>(item, more);

        /// <summary>
        /// A map from the given KeyValuePairs and appends them to the given Dictionary.
        /// </summary>
        /// <param name="src">source dictionary</param>
        /// <param name="list">KeyValuePairs to append</param>
        public static IDictionary<Key, Value> _<Key, Value>(IDictionary<Key, Value> src, params KeyValuePair<Key, Value>[] list)
            => new AsMap2<Key, Value>(src, list);

        /// <summary>
        /// A map by merging the given KeyValuePairs to the given Dictionary.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="list"></param>
        public static IDictionary<Key, Value> _<Key, Value>(IDictionary<Key, Value> src, IEnumerable<KeyValuePair<Key, Value>> list)
            => new AsMap2<Key, Value>(src, list);

        /// <summary>
        /// A map by taking the given entries.
        /// </summary>
        /// <param name="entries">enumerator of KeyValuePairs</param>
        public static IDictionary<Key, Value> _<Key, Value>(IEnumerator<KeyValuePair<Key, Value>> entries)
            => new AsMap2<Key, Value>(entries);

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        public static IDictionary<Key, Value> _<Key, Value>(IPair<Key, Value> entry, params IPair<Key, Value>[] more)
            => new AsMap2<Key, Value>(entry, more);

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        /// <param name="rejectBuildingAllValues">if you have KVPs with value functions, it is by default prevented to build all values by getting the enumerator. You can deactivate that here.</param>
        public static IDictionary<Key, Value> _<Key, Value>(IEnumerable<IPair<Key, Value>> entries, bool rejectBuildingAllValues = true)
            => new AsMap2<Key, Value>(entries, rejectBuildingAllValues);

        /// <summary>
        /// A map from another map.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public static IDictionary<Key, Value> _<Key, Value>(IDictionary<Key, Value> entries)
            => new AsMap2<Key, Value>(entries);

        /// <summary>
        /// A map from another map.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public static IDictionary<Key, Value> _<Key, Value>(Func<IDictionary<Key, Value>> entries)
            => new AsMap2<Key, Value>(entries);

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public static IDictionary<Key, Value> _<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> entries)
            => new AsMap2<Key, Value>(entries);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public static IDictionary<Key, Value> _<Key, Value>(params IMapInput<Key, Value>[] inputs)
            => new AsMap2<Key, Value>(inputs);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public static IDictionary<Key, Value> _<Key, Value>(IEnumerable<IMapInput<Key, Value>> inputs)
            => new AsMap2<Key, Value>(inputs);


    }
}
