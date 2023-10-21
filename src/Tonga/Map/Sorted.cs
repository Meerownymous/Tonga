

using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Map
{
    /// <summary>
    /// Sorts the given map with the given comparer
    /// </summary>
    /// <typeparam name="Key">Key Type of the Map</typeparam>
    /// <typeparam name="Value">Value Type of the Map</typeparam>
    public sealed class Sorted<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        public Sorted(IDictionary<Key, Value> dict)
            : this(dict, Comparer<Key>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public Sorted(IDictionary<Key, Value> dict, Func<Key, Key, int> compare)
            : this(dict, new SimpleComparer<Key>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public Sorted(IEnumerable<KeyValuePair<Key, Value>> pairs)
            : this(pairs, Comparer<Key>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given key compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public Sorted(IEnumerable<KeyValuePair<Key, Value>> pairs, Func<Key, Key, int> compare)
            : this(pairs, new SimpleComparer<Key>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public Sorted(IEnumerable<KeyValuePair<Key, Value>> pairs, Func<KeyValuePair<Key, Value>, KeyValuePair<Key, Value>, int> compare)
            : this(pairs, new SimpleComparer<KeyValuePair<Key, Value>>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public Sorted(IEnumerable<KeyValuePair<Key, Value>> pairs, IComparer<Key> cmp)
            : this(pairs, new KeyComparer<Key, Value>(cmp))
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public Sorted(IEnumerable<KeyValuePair<Key, Value>> pairs, IComparer<KeyValuePair<Key, Value>> cmp)
            : base(
                () =>
                {
                    var items = new List<KeyValuePair<Key, Value>>(pairs);
                    items.Sort(cmp);
                    return AsMap._(items);
                },
                false
            )
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public Sorted(IDictionary<Key, Value> dict, IComparer<Key> cmp)
            : base(
                () =>
                {
                    var keys = new List<Key>(dict.Keys);
                    keys.Sort(cmp);
                    var result = new LazyDict<Key, Value>(
                        new Mapped<Key, IPair<Key, Value>>(
                            key => AsPair._(key, () => dict[key]),
                            keys
                        )
                    );
                    return result;
                },
                false
            )
        { }
    }

    /// <summary>
    /// Sorts the given map with the given comparer
    /// </summary>
    /// <typeparam name="Value">Value Type of the Map</typeparam>
    public sealed class Sorted<Value> : MapEnvelope<Value>
    {
        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        public Sorted(IDictionary<string, Value> dict)
            : this(dict, Comparer<string>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public Sorted(IDictionary<string, Value> dict, Func<string, string, int> compare)
            : this(dict, new SimpleComparer<string>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public Sorted(IEnumerable<KeyValuePair<string, Value>> pairs)
            : this(pairs, Comparer<string>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given key compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public Sorted(IEnumerable<KeyValuePair<string, Value>> pairs, Func<string, string, int> compare)
            : this(pairs, new SimpleComparer<string>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public Sorted(IEnumerable<KeyValuePair<string, Value>> pairs, Func<KeyValuePair<string, Value>, KeyValuePair<string, Value>, int> compare)
            : this(pairs, new SimpleComparer<KeyValuePair<string, Value>>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public Sorted(IEnumerable<KeyValuePair<string, Value>> pairs, IComparer<string> cmp)
            : this(pairs, new KeyComparer<string, Value>(cmp))
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public Sorted(IEnumerable<KeyValuePair<string, Value>> pairs, IComparer<KeyValuePair<string, Value>> cmp)
            : base(
                () =>
                {
                    var items = new List<KeyValuePair<string, Value>>(pairs);
                    items.Sort(cmp);
                    return new AsMap<string, Value>(items);
                },
                false
            )
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public Sorted(IDictionary<string, Value> dict, IComparer<string> cmp)
            : base(
                () =>
                {
                    var keys = new List<string>(dict.Keys);
                    keys.Sort(cmp);
                    var result = new LazyDict<string, Value>(
                        new Mapped<string, IPair<string, Value>>(
                            key => new AsPair<string, Value>(key, () => dict[key]),
                            keys
                        )
                    );
                    return result;
                },
                false
            )
        { }
    }

    /// <summary>
    /// Sorts the given map with the given comparer
    /// </summary>
    public sealed class Sorted : MapEnvelope
    {
        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        public Sorted(IDictionary<string, string> dict)
            : this(dict, Comparer<string>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public Sorted(IDictionary<string, string> dict, Func<string, string, int> compare)
            : this(dict, new SimpleComparer<string>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public Sorted(IEnumerable<KeyValuePair<string, string>> pairs)
            : this(pairs, Comparer<string>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given key compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public Sorted(IEnumerable<KeyValuePair<string, string>> pairs, Func<string, string, int> compare)
            : this(pairs, new SimpleComparer<string>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public Sorted(IEnumerable<KeyValuePair<string, string>> pairs, Func<KeyValuePair<string, string>, KeyValuePair<string, string>, int> compare)
            : this(pairs, new SimpleComparer<KeyValuePair<string, string>>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public Sorted(IEnumerable<KeyValuePair<string, string>> pairs, IComparer<string> cmp)
            : this(pairs, new KeyComparer<string, string>(cmp))
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public Sorted(IEnumerable<KeyValuePair<string, string>> pairs, IComparer<KeyValuePair<string, string>> cmp)
            : base(
                () =>
                {
                    var items = new List<KeyValuePair<string, string>>(pairs);
                    items.Sort(cmp);
                    return new AsMap<string, string>(items);
                },
                false
            )
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public Sorted(IDictionary<string, string> dict, IComparer<string> cmp)
            : base(
                () =>
                {
                    var keys = new List<string>(dict.Keys);
                    keys.Sort(cmp);
                    var result = new LazyDict<string, string>(
                        new Mapped<string, IPair<string, string>>(
                            key => new AsPair<string, string>(key, () => dict[key]),
                            keys
                        )
                    );
                    return result;
                },
                false
            )
        { }

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        public static IDictionary<Key, Value> _<Key, Value>(IDictionary<Key, Value> dict)
            => new Sorted<Key, Value>(dict);

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public static IDictionary<Key, Value> _<Key, Value>(IDictionary<Key, Value> dict, Func<Key, Key, int> compare)
            => new Sorted<Key, Value>(dict, compare);

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public static IDictionary<Key, Value> _<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> pairs)
            => new Sorted<Key, Value>(pairs);

        /// <summary>
        /// Sorts the given map with the given key compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public static IDictionary<Key, Value> _<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> pairs, Func<Key, Key, int> compare)
            => new Sorted<Key, Value>(pairs, compare);

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public static IDictionary<Key, Value> _<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> pairs, Func<KeyValuePair<Key, Value>, KeyValuePair<Key, Value>, int> compare)
            => new Sorted<Key, Value>(pairs, compare);

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public static IDictionary<Key, Value> _<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> pairs, IComparer<Key> cmp)
            => new Sorted<Key, Value>(pairs, cmp);

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public static IDictionary<Key, Value> _<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> pairs, IComparer<KeyValuePair<Key, Value>> cmp)
            => new Sorted<Key, Value>(pairs, cmp);

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public static IDictionary<Key, Value> _<Key, Value>(IDictionary<Key, Value> dict, IComparer<Key> cmp)
            => new Sorted<Key, Value>(dict, cmp);

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        public static IDictionary<string, Value> _<Value>(IDictionary<string, Value> dict)
            => new Sorted<Value>(dict);

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public static IDictionary<string, Value> _<Value>(IDictionary<string, Value> dict, Func<string, string, int> compare)
            => new Sorted<Value>(dict, compare);

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public static IDictionary<string, Value> _<Value>(IEnumerable<KeyValuePair<string, Value>> pairs)
            => new Sorted<Value>(pairs);

        /// <summary>
        /// Sorts the given map with the given key compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public static IDictionary<string, Value> _<Value>(IEnumerable<KeyValuePair<string, Value>> pairs, Func<string, string, int> compare)
            => new Sorted<Value>(pairs, compare);

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public static IDictionary<string, Value> _<Value>(IEnumerable<KeyValuePair<string, Value>> pairs, Func<KeyValuePair<string, Value>, KeyValuePair<string, Value>, int> compare)
            => new Sorted<Value>(pairs, compare);

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public static IDictionary<string, Value> _<Value>(IEnumerable<KeyValuePair<string, Value>> pairs, IComparer<string> cmp)
            => new Sorted<Value>(pairs, cmp);

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public static IDictionary<string, Value> _<Value>(IEnumerable<KeyValuePair<string, Value>> pairs, IComparer<KeyValuePair<string, Value>> cmp)
            => new Sorted<Value>(pairs, cmp);

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public static IDictionary<string, Value> _<Value>(IDictionary<string, Value> dict, IComparer<string> cmp)
            => new Sorted<Value>(dict, cmp);
    }

    /// <summary>
    /// Simple Comparer comparing two elements
    /// </summary>
    /// <typeparam name="T">Type of the elements</typeparam>
    internal sealed class SimpleComparer<T> : IComparer<T>
    {
        private readonly Func<T, T, int> compare;

        /// <summary>
        /// Comparer from a function comparing two elements
        /// </summary>
        /// <param name="compare">Function comparing two elements</param>
        public SimpleComparer(Func<T, T, int> compare)
        {
            this.compare = compare;
        }

        public int Compare(T x, T y)
        {
            return this.compare(x, y);
        }
    }

    /// <summary>
    /// Comparer comparing two KeyValuePairs by key
    /// </summary>
    /// <typeparam name="Key">Key Type</typeparam>
    /// <typeparam name="Value">Value Type</typeparam>
    internal sealed class KeyComparer<Key, Value> : IComparer<KeyValuePair<Key, Value>>
    {
        private readonly IComparer<Key> cmp;

        /// <summary>
        /// Comparer comparing two KeyValuePairs by key
        /// </summary>
        /// <param name="cmp">Comparer compairing the key type</param>
        public KeyComparer(IComparer<Key> cmp)
        {
            this.cmp = cmp;
        }

        public int Compare(KeyValuePair<Key, Value> x, KeyValuePair<Key, Value> y)
        {
            return this.cmp.Compare(x.Key, y.Key);
        }
    }
}
