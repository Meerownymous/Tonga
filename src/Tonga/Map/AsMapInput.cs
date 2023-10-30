

using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Func;
using Tonga.List;
using Tonga.Scalar;

namespace Tonga.Map
{
    /// <summary>
    /// LookupInput from pairs.
    /// </summary>
    public sealed class AsMapInput<Key, Value> : IMapInput<Key, Value>
    {
        private readonly IEnumerable<IPair<Key, Value>> pairs;

        /// <summary>
        /// LookupInput from pairs.
        /// </summary>
        public AsMapInput(params IPair<Key, Value>[] pairs) : this(AsEnumerable._(pairs))
        { }

        /// <summary>
        /// LookupInput from pairs.
        /// </summary>
        public AsMapInput(IEnumerable<IPair<Key, Value>> kvps)
        {
            this.pairs = kvps;
        }

        public IMap<Key, Value> Merged(IMap<Key, Value> map)
        {
            var result = map;
            Each._(
                pair => result = result.With(pair),
                this.pairs
            ).Invoke();
            return result;
        }

        public IMapInput<Key, Value> Self()
        {
            return this;
        }
    }

    /// <summary>
    /// LookupInput from pairs.
    /// </summary>
    public static class AsMapInput
    {
        /// <summary>
        /// LookupInput from pairs.
        /// </summary>
        public static AsMapInput<Key, Value> _<Key, Value>(params IPair<Key, Value>[] pairs) =>
            new AsMapInput<Key, Value>(pairs);

        /// <summary>
        /// LookupInput from pairs.
        /// </summary>
        public static AsMapInput<Key, Value> _<Key, Value>(IEnumerable<IPair<Key, Value>> pairs) =>
            new AsMapInput<Key, Value>(pairs);
    }
}
