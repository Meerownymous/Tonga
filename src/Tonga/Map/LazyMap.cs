//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Tonga.Enumerable;
//using Tonga.List;
//using Tonga.Text;

//namespace Tonga.Map
//{
//    /// <summary>
//    /// A map whose values are retrieved only when accessing them.
//    /// This map is not sticky, so every retreval will result in a new computation of the value.
//    /// </summary>
//    public sealed class LazyMap<Key, Value> : IMap<Key, Value>
//    {
//        private readonly IMap<Key, Func<Value>> map;
//        private readonly InvalidOperationException rejectReadException = new InvalidOperationException("Writing is not supported, it's a read-only map");
//        private readonly bool rejectBuildingAllValues;
//        private readonly Scalar.Sticky<bool> anyValueIsLazy;

//        /// <summary>
//        /// A map whose values are retrieved only when accessing them.
//        /// This map is not sticky, so every retreval will result in a new computation of the value.
//        /// </summary>
//        public LazyMap(params IPair<Key, Value>[] kvps) : this(AsEnumerable._(kvps), true)
//        { }

//        /// <summary>
//        /// A map whose values are retrieved only when accessing them.
//        /// This map is not sticky, so every retreval will result in a new computation of the value.
//        /// </summary>
//        public LazyMap(bool rejectBuildingAllValues, params IPair<Key, Value>[] kvps) : this(
//            new AsEnumerable<IPair<Key, Value>>(kvps),
//            rejectBuildingAllValues
//        )
//        { }

//        /// <summary>
//        /// A map whose values are retrieved only when accessing them.
//        /// This map is not sticky, so every retreval will result in a new computation of the value.
//        /// </summary>
//        public LazyMap(IEnumerable<IPair<Key, Value>> kvps, bool rejectBuildingAllValues = true)
//        {
//            this.rejectBuildingAllValues = rejectBuildingAllValues;
//            this.map =
//                AsMap._(() =>
//                {
//                    var dict = new Dictionary<Key, Func<Value>>();
//                    foreach (var kvp in kvps)
//                    {
//                        dict[kvp.Key()] = () => kvp.Value();
//                    }
//                    return dict;
//                });
//            this.anyValueIsLazy = Scalar.Sticky._(() =>
//            {
//                bool result = false;
//                foreach (var kvp in kvps)
//                {
//                    if (kvp.IsLazy())
//                    {
//                        result = true;
//                        break;
//                    }
//                }
//                return result;
//            });

//        }

//        /// <summary>
//        /// Access a value by key
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public Value this[Key key] { get { return map[key](); } set { throw this.rejectReadException; } }

//        /// <summary>
//        /// Access all keys
//        /// </summary>
//        public ICollection<Key> Keys => map.Keys;

//        /// <summary>
//        /// Access all values
//        /// </summary>
//        public ICollection<Value> Values
//        {
//            get
//            {
//                if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
//                {
//                    throw new InvalidOperationException(
//                        "Cannot get all values because this is a lazy dictionary."
//                        + " Getting the values would compile all of them, which is often not intended."
//                        + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
//                }
//                return
//                    List.Mapped._(
//                        v => v(),
//                        map.Values

//                   );
//            }
//        }


//        /// <summary>
//        /// Count entries
//        /// </summary>
//        public int Count => map.Count;

//        /// <summary>
//        /// Yes its readonly
//        /// </summary>
//        public bool IsReadOnly => true;

//        /// <summary>
//        /// Unsupported
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        public void Add(Key key, Value value)
//        {
//            throw this.rejectReadException;
//        }

//        /// <summary>
//        /// Unsupported
//        /// </summary>
//        /// <param name="item"></param>
//        public void Add(KeyValuePair<Key, Value> item)
//        {
//            throw this.rejectReadException;
//        }

//        /// <summary>
//        /// Unsupported
//        /// </summary>
//        public void Clear()
//        {
//            throw this.rejectReadException;
//        }

//        /// <summary>
//        /// Test if map contains entry
//        /// </summary>
//        /// <param name="item">item to check</param>
//        /// <returns>true if it contains</returns>
//        public bool Contains(KeyValuePair<Key, Value> item)
//        {
//            return this.map.ContainsKey(item.Key) && this.map[item.Key]().Equals(item.Value);
//        }

//        /// <summary>
//        /// Test if map contains key
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public bool ContainsKey(Key key)
//        {
//            return this.map.ContainsKey(key);
//        }

//        /// <summary>
//        /// Copy this to an array
//        /// </summary>
//        /// <param name="array">target array</param>
//        /// <param name="arrayIndex">index to start</param>
//        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
//        {
//            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
//            {
//                throw new InvalidOperationException(
//                    "Cannot copy entries because this is a lazy dictionary."
//                    + " Copying the entries would build all values."
//                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
//            }
//            if (arrayIndex > this.map.Count)
//            {
//                throw
//                    new ArgumentOutOfRangeException(
//                        new Formatted(
//                            "arrayIndex {0} is higher than the item count in the map {1}.",
//                            arrayIndex,
//                            this.map.Count
//                        ).AsString());
//            }

//            new AsList<KeyValuePair<Key, Value>>(this).CopyTo(array, arrayIndex);
//        }

//        /// <summary>
//        /// The enumerator
//        /// </summary>
//        /// <returns>The enumerator</returns>
//        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
//        {
//            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
//            {
//                throw new InvalidOperationException(
//                    "Cannot get the enumerator because this is a lazy dictionary."
//                    + " Enumerating the entries would build all values."
//                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
//            }
//            return
//                Enumerable.Mapped._(
//                    kvp => new KeyValuePair<Key, Value>(kvp.Key, kvp.Value()),
//                    this.map
//                ).GetEnumerator();
//        }

//        public Func<Value> Lazy(Key key)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<IPair<Key, Value>> Pairs()
//        {
//            throw new NotImplementedException();
//        }

//        /// <summary>
//        /// Unsupported
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public bool Remove(Key key)
//        {
//            throw this.rejectReadException;
//        }

//        /// <summary>
//        /// Unsupported
//        /// </summary>
//        /// <param name="item"></param>
//        /// <returns></returns>
//        public bool Remove(KeyValuePair<Key, Value> item)
//        {
//            throw this.rejectReadException;
//        }

//        /// <summary>
//        /// Tries to get value
//        /// </summary>
//        /// <param name="key">key</param>
//        /// <param name="value">target to store value</param>
//        /// <returns>true if success</returns>
//        public bool TryGetValue(Key key, out Value value)
//        {
//            var result = this.map.ContainsKey(key);
//            if (result)
//            {
//                value = this.map[key]();
//                result = true;
//            }
//            else
//            {
//                value = default(Value);
//            }
//            return result;
//        }

//        public IMap<Key, Value> With(IPair<Key, Value> pair)
//        {
//            throw new NotImplementedException();
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }

//        ICollection<Key> IMap<Key, Value>.Keys()
//        {
//            throw new NotImplementedException();
//        }
//    }

//    /// <summary>
//    /// A dictionary whose values are retrieved only when accessing them.
//    /// </summary>
//    public static class LazyMap2
//    {
//        /// <summary>
//        /// A dictionary whose values are retrieved only when accessing them.
//        /// </summary>
//        public static LazyMap<Key, Value> _<Key, Value>(
//            params IPair<Key, Value>[] pairs
//        ) => new LazyMap<Key, Value>(pairs);

//        /// <summary>
//        /// A dictionary whose values are retrieved only when accessing them.
//        /// </summary>
//        public static LazyMap<Key, Value> _<Key, Value>(
//            bool rejectBuildingAllValues, params IPair<Key, Value>[] pairs
//        ) =>
//            new LazyMap<Key, Value>(rejectBuildingAllValues, pairs);

//        /// <summary>
//        /// A dictionary whose values are retrieved only when accessing them.
//        /// </summary>
//        public static LazyMap<Key, Value> _<Key, Value>(
//            IEnumerable<IPair<Key, Value>> pairs,
//            bool rejectBuildingAllValues = true) =>
//            new LazyMap<Key, Value>(pairs, rejectBuildingAllValues);
//    }
//}
