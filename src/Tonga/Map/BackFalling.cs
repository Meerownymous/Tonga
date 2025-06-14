

using System;
using System.Collections;
using System.Collections.Generic;

using Tonga.Scalar;

namespace Tonga.Map
{
    /// <summary>
    /// Map which can return a fallback value generated by a fallback function
    /// </summary>
    /// <typeparam name="Key">Key Type of the Map</typeparam>
    /// <typeparam name="Value">Value Type of the Map</typeparam>
    public sealed class BackFalling<Key, Value> : IMap<Key, Value>
    {
        private readonly Func<IMap<Key, Value>> origin;
        private readonly Func<Key, Value> fallback;

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallbackMap">Fallback map containing missing values</param>
        public BackFalling(IMap<Key, Value> map, IMap<Key, Value> fallbackMap) : this(
            () => map, fallbackMap
                        )
        { }

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallbackMap">Fallback map containing missing values</param>
        public BackFalling(Func<IMap<Key, Value>> map, IMap<Key, Value> fallbackMap) : this(
            map, key => fallbackMap[key]
        )
        { }

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallback">Fallback generating missing values</param>
        public BackFalling(IMap<Key, Value> map, Func<Key, Value> fallback) : this(
            () => map, fallback
        )
        { }

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallback">Fallback generating missing values</param>
        public BackFalling(Func<IMap<Key, Value>> map, Func<Key, Value> fallback)
        {
            this.origin = map;
            this.fallback = fallback;
        }

        public Value this[Key key]
        {
            get
            {
                Value result;
                var map = this.origin();
                if (map.Keys().Contains(key))
                {
                    result = map[key];
                }
                else
                {
                    result = fallback(key);
                }
                return result;
            }
        }

        public ICollection<Key> Keys => this.origin().Keys();

        public Func<Value> Lazy(Key key) => this.origin().Lazy(key);

        public IEnumerable<IPair<Key, Value>> Pairs() => this.origin().Pairs();

        public IMap<Key, Value> With(IPair<Key, Value> pair) => this.origin().With(pair);

        ICollection<Key> IMap<Key, Value>.Keys() => this.origin().Keys();
    }

    public static partial class MapSmarts
    {
        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallbackMap">Fallback map containing missing values</param>
        public static IMap<Key, Value> AsBackFalling<Key, Value>(this IMap<Key, Value> map, IMap<Key, Value> fallbackMap)
            => new BackFalling<Key, Value>(map, fallbackMap);

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallbackMap">Fallback map containing missing values</param>
        public static IMap<Key, Value> AsBackFalling<Key, Value>(this Func<IMap<Key, Value>> map, IMap<Key, Value> fallbackMap)
            => new BackFalling<Key, Value>(map, fallbackMap);

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallback">Fallback generating missing values</param>
        public static IMap<Key, Value> AsBackFalling<Key, Value>(this IMap<Key, Value> map, Func<Key, Value> fallback)
            => new BackFalling<Key, Value>(map, fallback);

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallback">Fallback generating missing values</param>
        public static IMap<Key, Value> AsBackFalling<Key, Value>(this IMap<Key, Value> map, Action<Key> fallback)
            => new BackFalling<Key, Value>(map, fallback: key =>
            {
                fallback(key);
                throw new ArgumentException($"Key '{key}' does not exist in the map.");
            });

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallback">Fallback generating missing values</param>
        public static IMap<Key, Value> AsBackFalling<Key, Value>(this Func<IMap<Key, Value>> map, Func<Key, Value> fallback)
            => new BackFalling<Key, Value>(map, fallback);
    }
}
