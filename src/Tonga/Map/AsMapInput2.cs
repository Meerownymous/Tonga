

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
    public sealed class AsMapInput3<Key, Value> : ILookupInput<Key, Value>
    {
        private readonly IEnumerable<IPair<Key, Value>> pairs;

        /// <summary>
        /// LookupInput from pairs.
        /// </summary>
        public AsMapInput3(params IPair<Key, Value>[] pairs) : this(AsEnumerable._(pairs))
        { }

        /// <summary>
        /// LookupInput from pairs.
        /// </summary>
        public AsMapInput3(IEnumerable<IPair<Key, Value>> kvps)
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
    }

    /// <summary>
    /// LookupInput from pairs.
    /// </summary>
    public static class AsLookupInput
    {
        /// <summary>
        /// LookupInput from pairs.
        /// </summary>
        public static AsMapInput3<Key, Value> _<Key, Value>(params IPair<Key, Value>[] pairs) =>
            new AsMapInput3<Key, Value>(pairs);

        /// <summary>
        /// LookupInput from pairs.
        /// </summary>
        public static AsMapInput3<Key, Value> _<Key, Value>(IEnumerable<IPair<Key, Value>> pairs) =>
            new AsMapInput3<Key, Value>(pairs);
    }
}
