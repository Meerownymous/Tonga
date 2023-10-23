

using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Map
{
    /// <summary>
    /// A map from string to string.
    /// </summary>
    public sealed class AsMap : MapEnvelope
    {
        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(KeyValuePair<string, string> entry, params KeyValuePair<string, string>[] more) : this(
            Enumerable.Joined._(
                AsEnumerable._(more),
                entry
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs and appends them to the given Dictionary.
        /// </summary>
        /// <param name="src">source dictionary</param>
        /// <param name="list">KeyValuePairs to append</param>
        public AsMap(IDictionary<string, string> src, params KeyValuePair<string, string>[] list) : this(
            src,
            AsEnumerable._(list))
        { }

        /// <summary>
        /// A map by merging the given KeyValuePairs to the given Dictionary.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="list"></param>
        public AsMap(IDictionary<string, string> src, IEnumerable<KeyValuePair<string, string>> list) : this(
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
        public AsMap(IEnumerator<KeyValuePair<string, string>> entries) : this(
            AsEnumerable._(() => entries))
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        public AsMap(IPair entry, params IPair[] more) : base(() =>
            new LazyMap(
                Enumerable.Joined._(
                    AsEnumerable._(entry),
                    more
                )
            ),
            false
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// Rejects building of all values
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public AsMap(IEnumerable<IPair> entries) : this(
            entries, true
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        /// <param name="rejectBuildingAllValues">if you have KVPs with value functions, it is by default prevented to build all values by getting the enumerator. You can deactivate that here.</param>
        public AsMap(IEnumerable<IPair> entries, bool rejectBuildingAllValues) : this(
            new LazyMap(entries, rejectBuildingAllValues)
        )
        { }

        /// <summary>
        /// A map from another map.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public AsMap(IDictionary<string, string> entries) : base(() => entries, false)
        //caution: do not remove this ctor. It is a "proxy" ctor to prevent copying values. 
        //Because a map is also a IEnumerable of KeyValuePairs, the ctor accepting the enumerable would copy the map.
        { }

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public AsMap(IEnumerable<KeyValuePair<string, string>> entries) : this(
            () =>
            {
                var temp = new Dictionary<string, string>();
                foreach (var entry in entries)
                {
                    temp[entry.Key] = entry.Value;
                }
                return temp;
            }
        )
        { }

        /// <summary>
        /// A map from string to string.
        /// </summary>
        public AsMap(params string[] pairSequence) : this(
            AsEnumerable._(pairSequence)
        )
        { }

        /// <summary>
        /// A map from string to string.
        /// </summary>
        public AsMap(string key, string value, params string[] additional) : this(
            Enumerable.Joined._(
                AsEnumerable._(key, value),
                additional
            )
        )
        { }

        /// <summary>
        /// A map from string to string.
        /// </summary>
        /// <param name="pairSequence">Pairs as a sequence, ordered like this: key-1, value-1, ... key-n, value-n</param>
        public AsMap(IEnumerable<string> pairSequence) : this(
            () =>
            {
                var idx = -1;
                var enumerator = pairSequence.GetEnumerator();
                var key = string.Empty;
                var result = new Dictionary<string, string>();
                while (enumerator.MoveNext())
                {
                    idx++;
                    if (idx % 2 == 0)
                    {
                        key = enumerator.Current;
                    }
                    else
                    {
                        result.Add(key, enumerator.Current);
                    }
                }

                if (idx % 2 != 1 && idx != -1)
                {
                    throw new ArgumentException($"Cannot build a map because an even number of strings is needed, and the provided ones count {idx + 1}");
                }
                return result;
            }
        )
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public AsMap(params IMapInput[] inputs) : this(AsEnumerable._(inputs))
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public AsMap(IEnumerable<IMapInput> inputs) : this(
            () =>
            {
                IDictionary<string, string> dict = new LazyMap();
                foreach (IMapInput input in inputs)
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
        public AsMap(Func<IDictionary<string, string>> input) : base(
            input, false
        )
        { }

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2
        )
        => AsMap._(
            key1, value1,
            key2, value2
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3
        )
        => AsMap._(
            key1, value1,
            key2, value2,
            key3, value3
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4
        )
        => AsMap._(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5
        )
        => AsMap._(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6
        )
        => AsMap._(
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
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7
        )
        => AsMap._(
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
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8
        )
        => AsMap._(
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
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9
        )
        => AsMap._(
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
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10
        )
        => AsMap._(
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
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11
        )
        => AsMap._(
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
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12
        )
        => AsMap._(
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
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13
        )
        => AsMap._(
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
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13,
            string key14, Value value14
        )
        => AsMap._(
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
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13,
            string key14, Value value14,
            string key15, Value value15
        )
        => AsMap._(
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
        public static IDictionary<string, Value> _<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13,
            string key14, Value value14,
            string key15, Value value15,
            string key16, Value value16
        )
        => AsMap._(
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
        public static IDictionary<string, Value> _<Value>(KeyValuePair<string, Value> entry, params KeyValuePair<string, Value>[] more)
            => new AsMap<Value>(entry, more);

        /// <summary>
        /// A map by taking the given entries.
        /// </summary>
        /// <param name="entries">enumerator of KeyValuePairs</param>
        public static IDictionary<string, Value> _<Value>(IEnumerator<KeyValuePair<string, Value>> entries)
            => new AsMap<Value>(entries);

        /// <summary>
        /// A map from the given IKvps.
        /// </summary>
        public static IDictionary<string, Value> _<Value>(IPair<Value> entry, params IPair<Value>[] more)
            => new AsMap<Value>(entry, more);

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        /// <param name="rejectBuildingAllValues">if you have KVPs with value functions, it is by default prevented to build all values by getting the enumerator. You can deactivate that here.</param>
        public static IDictionary<string, Value> _<Value>(IEnumerable<IPair<Value>> entries, bool rejectBuildingAllValues = true)
            => new AsMap<Value>(entries, rejectBuildingAllValues);

        /// <summary>
        /// A map from another map.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public static IDictionary<string, Value> _<Value>(IDictionary<string, Value> entries)
            => new AsMap<Value>(entries);

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public static IDictionary<string, Value> _<Value>(IEnumerable<KeyValuePair<string, Value>> entries)
            => new AsMap<Value>(entries);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public static IDictionary<string, Value> _<Value>(params IMapInput<Value>[] inputs)
            => new AsMap<Value>(inputs);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public static IDictionary<string, Value> _<Value>(IEnumerable<IMapInput<Value>> inputs)
            => new AsMap<Value>(inputs);

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public static IDictionary<string, Value> _<Value>(Func<IDictionary<string, Value>> input)
            => new AsMap<Value>(input);

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> _<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(
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
        public static IDictionary<Key, Value> _<Key, Value>(KeyValuePair<Key, Value> item, params KeyValuePair<Key, Value>[] more)
            => new AsMap<Key, Value>(item, more);

        /// <summary>
        /// A map from the given KeyValuePairs and appends them to the given Dictionary.
        /// </summary>
        /// <param name="src">source dictionary</param>
        /// <param name="list">KeyValuePairs to append</param>
        public static IDictionary<Key, Value> _<Key, Value>(IDictionary<Key, Value> src, params KeyValuePair<Key, Value>[] list)
            => new AsMap<Key, Value>(src, list);

        /// <summary>
        /// A map by merging the given KeyValuePairs to the given Dictionary.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="list"></param>
        public static IDictionary<Key, Value> _<Key, Value>(IDictionary<Key, Value> src, IEnumerable<KeyValuePair<Key, Value>> list)
            => new AsMap<Key, Value>(src, list);

        /// <summary>
        /// A map by taking the given entries.
        /// </summary>
        /// <param name="entries">enumerator of KeyValuePairs</param>
        public static IDictionary<Key, Value> _<Key, Value>(IEnumerator<KeyValuePair<Key, Value>> entries)
            => new AsMap<Key, Value>(entries);

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        public static IDictionary<Key, Value> _<Key, Value>(IPair<Key, Value> entry, params IPair<Key, Value>[] more)
            => new AsMap<Key, Value>(entry, more);

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        /// <param name="rejectBuildingAllValues">if you have KVPs with value functions, it is by default prevented to build all values by getting the enumerator. You can deactivate that here.</param>
        public static IDictionary<Key, Value> _<Key, Value>(IEnumerable<IPair<Key, Value>> entries, bool rejectBuildingAllValues = true)
            => new AsMap<Key, Value>(entries, rejectBuildingAllValues);

        /// <summary>
        /// A map from another map.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public static IDictionary<Key, Value> _<Key, Value>(IDictionary<Key, Value> entries)
            => new AsMap<Key, Value>(entries);

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public static IDictionary<Key, Value> _<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> entries)
            => new AsMap<Key, Value>(entries);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public static IDictionary<Key, Value> _<Key, Value>(params IMapInput<Key, Value>[] inputs)
            => new AsMap<Key, Value>(inputs);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public static IDictionary<Key, Value> _<Key, Value>(IEnumerable<IMapInput<Key, Value>> inputs)
            => new AsMap<Key, Value>(inputs);

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public static IDictionary<Key, Value> _<Key, Value>(Func<IDictionary<Key, Value>> input)
            => new AsMap<Key, Value>(input);
    }

    /// <summary>
    /// A map from string to typed value.
    /// </summary>
    public sealed class AsMap<Value> : MapEnvelope<Value>
    {
        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2),
                AsPair._(key3, value3)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9),
                AsPair._(key10, value10)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9),
                AsPair._(key10, value10),
                AsPair._(key11, value11)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9),
                AsPair._(key10, value10),
                AsPair._(key11, value11),
                AsPair._(key12, value12)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9),
                AsPair._(key10, value10),
                AsPair._(key11, value11),
                AsPair._(key12, value12),
                AsPair._(key13, value13)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13,
            string key14, Value value14,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9),
                AsPair._(key10, value10),
                AsPair._(key11, value11),
                AsPair._(key12, value12),
                AsPair._(key13, value13),
                AsPair._(key14, value14)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13,
            string key14, Value value14,
            string key15, Value value15,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9),
                AsPair._(key10, value10),
                AsPair._(key11, value11),
                AsPair._(key12, value12),
                AsPair._(key13, value13),
                AsPair._(key14, value14),
                AsPair._(key15, value15)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13,
            string key14, Value value14,
            string key15, Value value15,
            string key16, Value value16,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, value1),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7),
                AsPair._(key8, value8),
                AsPair._(key9, value9),
                AsPair._(key10, value10),
                AsPair._(key11, value11),
                AsPair._(key12, value12),
                AsPair._(key13, value13),
                AsPair._(key14, value14),
                AsPair._(key15, value15),
                AsPair._(key16, value16)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(KeyValuePair<string, Value> entry, params KeyValuePair<string, Value>[] more) : this(
            Enumerable.Joined._(
                AsEnumerable._(more),
                entry
            )
        )
        { }

        /// <summary>
        /// A map by taking the given entries.
        /// </summary>
        /// <param name="entries">enumerator of KeyValuePairs</param>
        public AsMap(IEnumerator<KeyValuePair<string, Value>> entries) : this(
            AsEnumerable._(() => entries))
        { }

        /// <summary>
        /// A map from the given IKvps.
        /// </summary>
        public AsMap(IPair<Value> entry, params IPair<Value>[] more) : base(() =>
            new LazyDict<Value>(
                Enumerable.Joined._(
                    Enumerable.Single._(entry),
                    more
                )
            ),
            false
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        /// <param name="rejectBuildingAllValues">if you have KVPs with value functions, it is by default prevented to build all values by getting the enumerator. You can deactivate that here.</param>
        public AsMap(IEnumerable<IPair<Value>> entries, bool rejectBuildingAllValues = true) : this(
            new LazyDict<Value>(entries, rejectBuildingAllValues)
        )
        { }

        /// <summary>
        /// A map from another map.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public AsMap(IDictionary<string, Value> entries) : base(() => entries, false)
        //caution: do not remove this ctor. It is a "proxy" ctor to prevent copying values. 
        //Because a map is also a IEnumerable of KeyValuePairs, the ctor accepting the enumerable would copy the map.
        { }

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public AsMap(IEnumerable<KeyValuePair<string, Value>> entries) : this(
            () =>
            {
                var temp = new Dictionary<string, Value>();
                foreach (var entry in entries)
                {
                    temp[entry.Key] = entry.Value;
                }
                return temp;
            }
        )
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public AsMap(params IMapInput<Value>[] inputs) : this(
            AsEnumerable._(inputs)
        )
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public AsMap(IEnumerable<IMapInput<Value>> inputs) : this(
            () =>
            {
                IDictionary<string, Value> dict = new LazyDict<Value>();
                foreach (IMapInput<Value> input in inputs)
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
        public AsMap(Func<IDictionary<string, Value>> input) : base(
            input, false
        )
        { }

    }

    /// <summary>
    /// A map from string to typed value.
    /// </summary>
    public sealed class AsMap<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3)
            ),
            rejectBuildingAllValues
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
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4)
            ),
            rejectBuildingAllValues
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
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5)
            ),
            rejectBuildingAllValues
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
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6)
            ),
            rejectBuildingAllValues
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
            bool rejectBuildingAllValues = true
        ) : this(
            AsEnumerable._(
                AsPair._(key1, Key),
                AsPair._(key2, value2),
                AsPair._(key3, value3),
                AsPair._(key4, value4),
                AsPair._(key5, value5),
                AsPair._(key6, value6),
                AsPair._(key7, value7)
            ),
            rejectBuildingAllValues
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
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
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
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
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
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
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
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
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
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
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
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
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
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
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
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
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
            Key key16, Value Key6,
            bool rejectBuildingAllValues = true
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
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public AsMap(KeyValuePair<Key, Value> item, params KeyValuePair<Key, Value>[] more) : this(
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
        public AsMap(IDictionary<Key, Value> src, params KeyValuePair<Key, Value>[] list) : this(
            src,
            AsEnumerable._(list))
        { }

        /// <summary>
        /// A map by merging the given KeyValuePairs to the given Dictionary.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="list"></param>
        public AsMap(IDictionary<Key, Value> src, IEnumerable<KeyValuePair<Key, Value>> list) : this(
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
        public AsMap(IEnumerator<KeyValuePair<Key, Value>> entries) : this(
            AsEnumerable._(() => entries))
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        public AsMap(IPair<Key, Value> entry, params IPair<Key, Value>[] more) : base(() =>
             new LazyMap<Key, Value>(
                 Enumerable.Joined._(
                     AsEnumerable._(entry),
                     more
                 )
             ),
            false
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        /// <param name="rejectBuildingAllValues">if you have KVPs with value functions, it is by default prevented to build all values by getting the enumerator. You can deactivate that here.</param>
        public AsMap(IEnumerable<IPair<Key, Value>> entries, bool rejectBuildingAllValues = true) : this(
            new LazyMap<Key, Value>(entries, rejectBuildingAllValues)
        )
        { }

        /// <summary>
        /// A map from another map.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public AsMap(IDictionary<Key, Value> entries) : base(() => entries, false)
        //caution: do not remove this ctor. It is a "proxy" ctor to prevent copying values. 
        //Because a map is also a IEnumerable of KeyValuePairs, the ctor accepting the enumerable would copy the map.
        { }

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public AsMap(IEnumerable<KeyValuePair<Key, Value>> entries) : this(
            () =>
            {
                var temp = new Dictionary<Key, Value>();
                foreach (var entry in entries)
                {
                    temp[entry.Key] = entry.Value;
                }
                return temp;
            }
        )
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public AsMap(params IMapInput<Key, Value>[] inputs) : this(new AsEnumerable<IMapInput<Key, Value>>(inputs))
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public AsMap(IEnumerable<IMapInput<Key, Value>> inputs) : this(
            () =>
            {
                IDictionary<Key, Value> dict = new LazyMap<Key, Value>();
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
        public AsMap(Func<IDictionary<Key, Value>> input) : base(
            input, false
        )
        { }
    }
}
