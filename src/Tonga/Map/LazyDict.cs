

using System;
using System.Collections;
using System.Collections.Generic;
using Tonga.Enumerable;

using Tonga.List;
using Tonga.Scalar;
using Tonga.Text;

namespace Tonga.Map
{
    /// <summary>
    /// A dictionary whose values are retrieved only when accessing them.
    /// </summary>
    public sealed class LazyDict : IDictionary<string, string>
    {
        private readonly IDictionary<string, Scalar.Sticky<string>> map;
        private readonly InvalidOperationException rejectReadException = new InvalidOperationException("Writing is not supported, it's a read-only map");
        private readonly bool rejectBuildingAllValues;
        private readonly Scalar.Sticky<bool> anyValueIsLazy;

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(params IKvp[] kvps) : this(AsEnumerable._(kvps), true)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(bool rejectBuildingAllKeys, params IKvp[] kvps) : this(
            AsEnumerable._(kvps),
            rejectBuildingAllKeys
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(IEnumerable<IKvp> kvps, bool rejectBuildingAllValues = true)
        {
            this.rejectBuildingAllValues = rejectBuildingAllValues;
            this.map =
                new MapOf<Scalar.Sticky<string>>(() =>
                {
                    var dict = new Dictionary<string, Scalar.Sticky<string>>();
                    foreach (var kvp in kvps)
                    {
                        dict[kvp.Key()] = Scalar.Sticky._(() => kvp.Value());
                    }
                    return dict;
                });
            this.anyValueIsLazy = Scalar.Sticky._(() =>
            {
                bool result = false;
                foreach (var kvp in kvps)
                {
                    if (kvp.IsLazy())
                    {
                        result = true;
                        break;
                    }
                }
                return result;
            });
        }

        /// <summary>
        /// Access a value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key] { get { return map[key].Value(); } set { throw this.rejectReadException; } }

        /// <summary>
        /// Access all keys
        /// </summary>
        public ICollection<string> Keys => map.Keys;

        /// <summary>
        /// Access all values
        /// </summary>
        public ICollection<string> Values
        {
            get
            {
                if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
                {
                    throw new InvalidOperationException(
                        "Cannot get values because this is a lazy dictionary."
                        + " Getting the values would build all keys."
                        + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
                }
                return
                    List.Sticky._(
                        List.Mapped._(
                            v => v.Value(),
                            map.Values
                        )
                    );
            }
        }


        /// <summary>
        /// Count entries
        /// </summary>
        public int Count => map.Count;

        /// <summary>
        /// Yes its readonly
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, string value)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<string, string> item)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        public void Clear()
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Test if map contains entry
        /// </summary>
        /// <param name="item">item to check</param>
        /// <returns>true if it contains</returns>
        public bool Contains(KeyValuePair<string, string> item)
        {
            return this.map.ContainsKey(item.Key) && this.map[item.Key].Value().Equals(item.Value);
        }

        /// <summary>
        /// Test if map contains key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return this.map.ContainsKey(key);
        }

        /// <summary>
        /// Copy this to an array
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">index to start</param>
        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
            {
                throw new InvalidOperationException("Cannot copy entries because this is a lazy dictionary."
                    + " Copying the entries would build all values."
                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
            }
            if (arrayIndex > this.map.Count)
            {
                throw
                    new ArgumentOutOfRangeException(
                        new Formatted(
                            "arrayIndex {0} is higher than the item count in the map {1}.",
                            arrayIndex,
                            this.map.Count
                        ).AsString());
            }

            new AsList<KeyValuePair<string, string>>(this).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// The enumerator
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
            {
                throw new InvalidOperationException(
                    "Cannot get the enumerator because this is a lazy dictionary."
                    + " Enumerating the entries would build all values."
                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
            }
            return
                Enumerable.Mapped._(
                    kvp => new KeyValuePair<string, string>(kvp.Key, kvp.Value.Value()),
                    this.map
                ).GetEnumerator();
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<string, string> item)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Tries to get value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">target to store value</param>
        /// <returns>true if success</returns>
        public bool TryGetValue(string key, out string value)
        {
            var result = this.map.ContainsKey(key);
            if (result)
            {
                value = this.map[key].Value();
                result = true;
            }
            else
            {
                value = string.Empty;
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// ctor
        /// </summary>
        public static LazyDict<Value> New<Value>(params IKvp<Value>[] kvps)
            => new LazyDict<Value>(kvps);

        /// <summary>
        /// ctor
        /// </summary>
        public static LazyDict<Value> New<Value>(bool rejectBuildingAllValues, params IKvp<Value>[] kvps)
            => new LazyDict<Value>(kvps);

        /// <summary>
        /// ctor
        /// </summary>
        public static LazyDict<Value> New<Value>(IEnumerable<IKvp<Value>> kvps, bool rejectBuildingAllValues = true)
            => new LazyDict<Value>(kvps, rejectBuildingAllValues);

        /// <summary>
        /// ctor
        /// </summary>
        public static LazyDict<Key, Value> New<Key, Value>(params IKvp<Key, Value>[] kvps)
            => new LazyDict<Key, Value>(kvps);

        /// <summary>
        /// ctor
        /// </summary>
        public static LazyDict<Key, Value> New<Key, Value>(bool rejectBuildingAllValues, params IKvp<Key, Value>[] kvps)
            => new LazyDict<Key, Value>(rejectBuildingAllValues, kvps);

        /// <summary>
        /// ctor
        /// </summary>
        public static LazyDict<Key, Value> New<Key, Value>(IEnumerable<IKvp<Key, Value>> kvps, bool rejectBuildingAllValues = true)
            => new LazyDict<Key, Value>(kvps, rejectBuildingAllValues);
    }

    /// <summary>
    /// A dictionary whose values are retrieved only when accessing them.
    /// </summary>
    public sealed class LazyDict<Value> : IDictionary<string, Value>
    {
        private readonly IDictionary<string, Scalar.Sticky<Value>> map;
        private readonly InvalidOperationException rejectReadException = new InvalidOperationException("Writing is not supported, it's a read-only map");
        private readonly bool rejectBuildingAllValues;
        private readonly Scalar.Sticky<bool> anyValueIsLazy;

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(params IKvp<Value>[] kvps) : this(new Enumerable.AsEnumerable<IKvp<Value>>(kvps), true)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(bool rejectBuildingAllValues, params IKvp<Value>[] kvps) : this(
            AsEnumerable._(kvps),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(IEnumerable<IKvp<Value>> kvps, bool rejectBuildingAllValues = true)
        {
            this.rejectBuildingAllValues = rejectBuildingAllValues;
            this.map =
                new MapOf<Scalar.Sticky<Value>>(() =>
                {
                    var dict = new Dictionary<string, Scalar.Sticky<Value>>();
                    foreach (var kvp in kvps)
                    {
                        dict[kvp.Key()] = Scalar.Sticky._(() => kvp.Value());
                    }
                    return dict;
                });
            this.anyValueIsLazy = Scalar.Sticky._(() =>
            {
                bool result = false;
                foreach (var kvp in kvps)
                {
                    if (kvp.IsLazy())
                    {
                        result = true;
                        break;
                    }
                }
                return result;
            });
        }

        /// <summary>
        /// Access a value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Value this[string key] { get { return map[key].Value(); } set { throw this.rejectReadException; } }

        /// <summary>
        /// Access all keys
        /// </summary>
        public ICollection<string> Keys => map.Keys;

        /// <summary>
        /// Access all values
        /// </summary>
        public ICollection<Value> Values
        {
            get
            {
                if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
                {
                    throw new InvalidOperationException(
                        "Cannot get values because this is a lazy dictionary."
                        + " Getting the values would build all keys."
                        + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
                }
                return
                    List.Mapped._(
                        v => v.Value(),
                        map.Values
                   );
            }
        }

        /// <summary>
        /// Count entries
        /// </summary>
        public int Count => map.Count;

        /// <summary>
        /// Yes its readonly
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, Value value)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<string, Value> item)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        public void Clear()
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Test if map contains entry
        /// </summary>
        /// <param name="item">item to check</param>
        /// <returns>true if it contains</returns>
        public bool Contains(KeyValuePair<string, Value> item)
        {
            return this.map.ContainsKey(item.Key) && this.map[item.Key].Value().Equals(item.Value);
        }

        /// <summary>
        /// Test if map contains key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return this.map.ContainsKey(key);
        }

        /// <summary>
        /// Copy this to an array
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">index to start</param>
        public void CopyTo(KeyValuePair<string, Value>[] array, int arrayIndex)
        {
            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
            {
                throw new InvalidOperationException("Cannot copy entries because this is a lazy dictionary."
                    + " Copying the entries would build all values."
                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
            }
            if (arrayIndex > this.map.Count)
            {
                throw
                    new ArgumentOutOfRangeException(
                        new Formatted(
                            "arrayIndex {0} is higher than the item count in the map {1}.",
                            arrayIndex,
                            this.map.Count
                        ).AsString());
            }

            new AsList<KeyValuePair<string, Value>>(this).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// The enumerator
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<KeyValuePair<string, Value>> GetEnumerator()
        {
            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
            {
                throw new InvalidOperationException(
                    "Cannot get the enumerator because this is a lazy dictionary."
                    + " Enumerating the entries would build all values."
                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
            }
            return
                Enumerable.Mapped._(
                    kvp => new KeyValuePair<string, Value>(kvp.Key, kvp.Value.Value()),
                    this.map
                ).GetEnumerator();
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<string, Value> item)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Tries to get value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">target to store value</param>
        /// <returns>true if success</returns>
        public bool TryGetValue(string key, out Value value)
        {
            var result = this.map.ContainsKey(key);
            if (result)
            {
                value = this.map[key].Value();
                result = true;
            }
            else
            {
                value = default(Value);
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// A dictionary whose values are retrieved only when accessing them.
    /// </summary>
    public sealed class LazyDict<Key, Value> : IDictionary<Key, Value>
    {
        private readonly IDictionary<Key, Scalar.Sticky<Value>> map;
        private readonly InvalidOperationException rejectReadException = new InvalidOperationException("Writing is not supported, it's a read-only map");
        private readonly bool rejectBuildingAllValues;
        private readonly Scalar.Sticky<bool> anyValueIsLazy;

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(params IKvp<Key, Value>[] kvps) : this(Enumerable.AsEnumerable._(kvps), true)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(bool rejectBuildingAllValues, params IKvp<Key, Value>[] kvps) : this(new Enumerable.AsEnumerable<IKvp<Key, Value>>(kvps), rejectBuildingAllValues)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(IEnumerable<IKvp<Key, Value>> kvps, bool rejectBuildingAllValues = true)
        {
            this.rejectBuildingAllValues = rejectBuildingAllValues;
            this.map =
                new MapOf<Key, Scalar.Sticky<Value>>(() =>
                {
                    var dict = new Dictionary<Key, Scalar.Sticky<Value>>();
                    foreach (var kvp in kvps)
                    {
                        dict[kvp.Key()] = Scalar.Sticky._(() => kvp.Value());
                    }
                    return dict;
                });
            this.anyValueIsLazy = Scalar.Sticky._(() =>
            {
                bool result = false;
                foreach (var kvp in kvps)
                {
                    if (kvp.IsLazy())
                    {
                        result = true;
                        break;
                    }
                }
                return result;
            });

        }

        /// <summary>
        /// Access a value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Value this[Key key] { get { return map[key].Value(); } set { throw this.rejectReadException; } }

        /// <summary>
        /// Access all keys
        /// </summary>
        public ICollection<Key> Keys => map.Keys;

        /// <summary>
        /// Access all values
        /// </summary>
        public ICollection<Value> Values
        {
            get
            {
                if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
                {
                    throw new InvalidOperationException(
                        "Cannot get all values because this is a lazy dictionary."
                        + " Getting the values would build all keys."
                        + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
                }
                return
                    List.Mapped._(
                        v => v.Value(),
                        map.Values

                   );
            }
        }


        /// <summary>
        /// Count entries
        /// </summary>
        public int Count => map.Count;

        /// <summary>
        /// Yes its readonly
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(Key key, Value value)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<Key, Value> item)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        public void Clear()
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Test if map contains entry
        /// </summary>
        /// <param name="item">item to check</param>
        /// <returns>true if it contains</returns>
        public bool Contains(KeyValuePair<Key, Value> item)
        {
            return this.map.ContainsKey(item.Key) && this.map[item.Key].Value().Equals(item.Value);
        }

        /// <summary>
        /// Test if map contains key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(Key key)
        {
            return this.map.ContainsKey(key);
        }

        /// <summary>
        /// Copy this to an array
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">index to start</param>
        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
            {
                throw new InvalidOperationException(
                    "Cannot copy entries because this is a lazy dictionary."
                    + " Copying the entries would build all values."
                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
            }
            if (arrayIndex > this.map.Count)
            {
                throw
                    new ArgumentOutOfRangeException(
                        new Formatted(
                            "arrayIndex {0} is higher than the item count in the map {1}.",
                            arrayIndex,
                            this.map.Count
                        ).AsString());
            }

            new AsList<KeyValuePair<Key, Value>>(this).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// The enumerator
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
            {
                throw new InvalidOperationException(
                    "Cannot get the enumerator because this is a lazy dictionary."
                    + " Enumerating the entries would build all values."
                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
            }
            return
                Enumerable.Mapped._(
                    kvp => new KeyValuePair<Key, Value>(kvp.Key, kvp.Value.Value()),
                    this.map
                ).GetEnumerator();
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(Key key)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<Key, Value> item)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Tries to get value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">target to store value</param>
        /// <returns>true if success</returns>
        public bool TryGetValue(Key key, out Value value)
        {
            var result = this.map.ContainsKey(key);
            if (result)
            {
                value = this.map[key].Value();
                result = true;
            }
            else
            {
                value = default(Value);
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
