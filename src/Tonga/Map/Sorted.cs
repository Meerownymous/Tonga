

using System;
using System.Collections.Generic;

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
        /// Sorts the given map with the default comparator of the key
        /// </summary>
        /// <param name="map">Map to be sorted</param>
        public Sorted(IMap<Key, Value> map) : this(
            map, Comparer<Key>.Default
        )
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="map">Map to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public Sorted(IMap<Key, Value> map, Func<Key, Key, int> compare) : this(
            map, new SimpleComparer<Key>(compare)
        )
        { }

        /// <summary>
        /// Sorts the given map with the default comparator of the key
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
                new AsMap<Key,Value>(
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
        /// <param name="map">Map to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public Sorted(IMap<Key, Value> map, IComparer<Key> cmp) : base(
            new AsMap<Key,Value>(() =>
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
    internal sealed class SimpleComparer<T>(Func<T, T, int> compare) : IComparer<T>
    {
        public int Compare(T x, T y) => compare(x, y);
    }

    /// <summary>
    /// Comparer comparing two KeyValuePairs by key
    /// </summary>
    /// <typeparam name="Key">Key Type</typeparam>
    /// <typeparam name="Value">Value Type</typeparam>
    internal sealed class KeyComparer<Key, Value>(IComparer<Key> cmp) : IComparer<IPair<Key, Value>>
    {
        public int Compare(IPair<Key, Value> x, IPair<Key, Value> y) =>
            cmp.Compare(x.Key(), y.Key());
    }

    /// <summary>
    /// Sorts the given map with the given comparer
    /// </summary>
    public static partial class MapSmarts
    {
        /// <summary>
        /// Sorts the given map with the default comparator of the key
        /// </summary>
        /// <param name="map">Map to be sorted</param>
        public static IMap<Key, Value> AsSorted<Key, Value>(this IMap<Key, Value> map)
            => new Sorted<Key, Value>(map);

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public static IMap<Key, Value> AsSorted<Key, Value>(this IMap<Key, Value> dict, Func<Key, Key, int> compare)
            => new Sorted<Key, Value>(dict, compare);

        /// <summary>
        /// Sorts the given map with the default comparator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public static IMap<Key, Value> AsSorted<Key, Value>(this IEnumerable<IPair<Key, Value>> pairs)
            => new Sorted<Key, Value>(pairs);

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="map">Map to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public static IMap<Key, Value> AsSorted<Key, Value>(this IMap<Key, Value> map, IComparer<Key> cmp)
            => new Sorted<Key, Value>(map, cmp);
    }
}
