

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
    public sealed class AsLookupInput<Key, Value> : ILookupInput<Key, Value>
    {
        private readonly IEnumerable<IPair<Key, Value>> pairs;

        /// <summary>
        /// LookupInput from pairs.
        /// </summary>
        public AsLookupInput(params IPair<Key, Value>[] pairs) : this(AsEnumerable._(pairs))
        { }

        /// <summary>
        /// LookupInput from pairs.
        /// </summary>
        public AsLookupInput(IEnumerable<IPair<Key, Value>> kvps)
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
        public static AsLookupInput<Key, Value> _<Key, Value>(params IPair<Key, Value>[] pairs) =>
            new AsLookupInput<Key, Value>(pairs);

        /// <summary>
        /// LookupInput from pairs.
        /// </summary>
        public static AsLookupInput<Key, Value> _<Key, Value>(IEnumerable<IPair<Key, Value>> pairs) =>
            new AsLookupInput<Key, Value>(pairs);
    }
}
