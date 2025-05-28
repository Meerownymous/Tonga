using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Map
{
    /// <summary>
    /// LookupInput from pairs.
    /// </summary>
    public sealed class AsMapInput<Key, Value>(IEnumerable<IPair<Key, Value>> kvps) : IMapInput<Key, Value>
    {
        /// <summary>
        /// LookupInput from pairs.
        /// </summary>
        public AsMapInput(params IPair<Key, Value>[] pairs) : this(pairs.AsEnumerable())
        { }

        public IMap<Key, Value> Merged(IMap<Key, Value> map)
        {
            var result = map;
            foreach (var pair in kvps)
                result = result.With(pair);
            return result;
        }

        public IMapInput<Key, Value> Self() => this;
    }

    /// <summary>
    /// LookupInput from pairs.
    /// </summary>
    public static partial class MapSmarts
    {
        /// <summary>
        /// LookupInput from pairs.
        /// </summary>
        public static IMapInput<Key, Value> AsMapInput<Key, Value>(this IPair<Key, Value>[] pairs) =>
            new AsMapInput<Key, Value>(pairs);

        /// <summary>
        /// LookupInput from pairs.
        /// </summary>
        public static IMapInput<Key, Value> AsMapInput<Key, Value>(this IEnumerable<IPair<Key, Value>> pairs) =>
            new AsMapInput<Key, Value>(pairs);
    }
}
