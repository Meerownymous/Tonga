

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
        /// <param name="map">Map to be sorted</param>
        public Sorted(IMap<Key, Value> map) : this(
            map, Comparer<Key>.Default
        )
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public Sorted(IMap<Key, Value> map, Func<Key, Key, int> compare) : this(
            map, new SimpleComparer<Key>(compare)
        )
        { }

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public Sorted(IEnumerable<IPair<Key, Value>> pairs) : this(
            pairs, Comparer<Key>.Default
        )
        { }

        /// <summary>
        /// Sorts the given map with the given key compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public Sorted(
            IEnumerable<IPair<Key, Value>> pairs,
            Func<Key, Key, int> compare
        ) : this(
            pairs,
            new SimpleComparer<Key>(compare)
        )
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public Sorted(
            IEnumerable<IPair<Key, Value>> pairs,
            Func<IPair<Key, Value>, IPair<Key, Value>, int> compare
        ) : this(
            pairs,
            new SimpleComparer<IPair<Key, Value>>(compare)
        )
        { }

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public Sorted(
            IEnumerable<IPair<Key, Value>> pairs,
            IComparer<Key> cmp
        ) : this(
            pairs,
            new KeyComparer<Key, Value>(cmp)
        )
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public Sorted(
            IEnumerable<IPair<Key, Value>> pairs,
            IComparer<IPair<Key, Value>> cmp
        ) : base(
                AsMap._(
                    () =>
                    {
                        var items = new List<IPair<Key, Value>>(pairs);
                        items.Sort(cmp);
                        return items;
                    })
            )
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public Sorted(IMap<Key, Value> map, IComparer<Key> cmp) : base(
            AsMap._(() =>
            {
                var items = new List<IPair<Key,Value>>(map.Pairs());
                items.Sort(new KeyComparer<Key,Value>(cmp));
                return items;
            })
        )
        { }
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
    internal sealed class KeyComparer<Key, Value> : IComparer<IPair<Key, Value>>
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

        public int Compare(IPair<Key, Value> x, IPair<Key, Value> y)
        {
            return this.cmp.Compare(x.Key(), y.Key());
        }
    }

    /// <summary>
    /// Sorts the given map with the given comparer
    /// </summary>
    public static class Sorted
    {
        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        public static IMap<Key, Value> _<Key, Value>(IMap<Key, Value> dict)
            => new Sorted<Key, Value>(dict);

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public static IMap<Key, Value> _<Key, Value>(IMap<Key, Value> dict, Func<Key, Key, int> compare)
            => new Sorted<Key, Value>(dict, compare);

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public static IMap<Key, Value> _<Key, Value>(IEnumerable<IPair<Key, Value>> pairs)
            => new Sorted<Key, Value>(pairs);

        /// <summary>
        /// Sorts the given map with the given key compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public static IMap<Key, Value> _<Key, Value>(
            IEnumerable<IPair<Key, Value>> pairs,
            Func<Key, Key, int> compare)
            => new Sorted<Key, Value>(pairs, compare);

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public static IMap<Key, Value> _<Key, Value>(
            IEnumerable<IPair<Key, Value>> pairs,
            Func<IPair<Key, Value>, IPair<Key, Value>, int> compare)
            => new Sorted<Key, Value>(pairs, compare);

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public static IMap<Key, Value> _<Key, Value>(IEnumerable<IPair<Key, Value>> pairs, IComparer<Key> cmp)
            => new Sorted<Key, Value>(pairs, cmp);

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public static IMap<Key, Value> _<Key, Value>(IEnumerable<IPair<Key, Value>> pairs,
            IComparer<IPair<Key, Value>> cmp)
            => new Sorted<Key, Value>(pairs, cmp);

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="map">Map to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public static IMap<Key, Value> _<Key, Value>(IMap<Key, Value> map, IComparer<Key> cmp)
            => new Sorted<Key, Value>(map, cmp);
    }
}
