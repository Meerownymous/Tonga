using System;
using System.Collections.Generic;
using Tonga.Collection;
using Tonga.Enumerable;

namespace Tonga.Map
{
    /// <summary>
    /// A map whose values are retrieved only when accessing them.
    /// This map is not sticky, so every retreval will result in a new computation of the value.
    /// </summary>
    public sealed class LazyLookup<Key, Value> : IMap<Key, Value>
    {
        private readonly Lazy<IDictionary<Key, Func<Value>>> map;
        private readonly InvalidOperationException rejectReadException =
            new InvalidOperationException("Writing is not supported, it's a read-only map");
        private readonly IEnumerable<IPair<Key, Value>> pairs;
        private readonly bool rejectBuildingAllValues;
        private readonly Scalar.Sticky<bool> anyValueIsLazy;

        /// <summary>
        /// A map whose values are retrieved only when accessing them.
        /// This map is not sticky, so every retreval will result in a new computation of the value.
        /// </summary>
        public LazyLookup(params IPair<Key, Value>[] kvps) : this(AsEnumerable._(kvps), true)
        { }

        /// <summary>
        /// A map whose values are retrieved only when accessing them.
        /// This map is not sticky, so every retreval will result in a new computation of the value.
        /// </summary>
        public LazyLookup(bool rejectBuildingAllValues, params IPair<Key, Value>[] kvps) : this(
            new AsEnumerable<IPair<Key, Value>>(kvps),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map whose values are retrieved only when accessing them.
        /// This map is not sticky, so every retreval will result in a new computation of the value.
        /// </summary>
        public LazyLookup(IEnumerable<IPair<Key, Value>> pairs, bool rejectBuildingAllValues = true)
        {
            //Save pairs, values might change but keys must not.
            this.pairs = Enumerable.Sticky._(pairs);
            this.rejectBuildingAllValues = rejectBuildingAllValues;
            this.map =
                new Lazy<IDictionary<Key,Func<Value>>>(() =>
                {
                    var dict = new Dictionary<Key, Func<Value>>();
                    foreach (var pair in pairs)
                    {
                        dict[pair.Key()] = () => pair.Value();
                    }
                    return dict;
                });
            this.anyValueIsLazy = Scalar.Sticky._(() =>
            {
                bool result = false;
                foreach (var kvp in pairs)
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
        public Value this[Key key] { get { return map.Value[key](); }  }

        /// <summary>
        /// Access all keys
        /// </summary>
        public ICollection<Key> Keys() => map.Value.Keys;

        /// <summary>
        /// Access all values
        /// </summary>
        public ICollection<Value> Values()
        {
            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
            {
                throw new InvalidOperationException(
                    "Cannot get all values because this is a lazy map."
                    + " Getting the values would compile all of them, which is often not intended."
                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
            }
            return
                Collection.Mapped._(
                    v => v(),
                    map.Value.Values
                );
        }

        /// <summary>
        /// The enumerator
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerable<IPair<Key, Value>> Pairs()
        {
            return this.pairs;
        }

        public Func<Value> Lazy(Key key)
        {
            return this.map.Value[key];
        }

        public IMap<Key, Value> With(IPair<Key, Value> value)
        {
            this.map.Value[value.Key()] = value.Value;
            return this;
        }
    }

    /// <summary>
    /// A dictionary whose values are retrieved only when accessing them.
    /// </summary>
    public static class LazyLookup
    {
        /// <summary>
        /// A dictionary whose values are retrieved only when accessing them.
        /// </summary>
        public static LazyLookup<Key, Value> _<Key, Value>(
            params IPair<Key, Value>[] pairs
        ) => new LazyLookup<Key, Value>(pairs);

        /// <summary>
        /// A dictionary whose values are retrieved only when accessing them.
        /// </summary>
        public static LazyLookup<Key, Value> _<Key, Value>(
            bool rejectBuildingAllValues, params IPair<Key, Value>[] pairs
        ) =>
            new LazyLookup<Key, Value>(rejectBuildingAllValues, pairs);

        /// <summary>
        /// A dictionary whose values are retrieved only when accessing them.
        /// </summary>
        public static LazyLookup<Key, Value> _<Key, Value>(
            IEnumerable<IPair<Key, Value>> pairs,
            bool rejectBuildingAllValues = true) =>
            new LazyLookup<Key, Value>(pairs, rejectBuildingAllValues);
    }
}
