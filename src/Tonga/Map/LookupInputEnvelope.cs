using System;

namespace Tonga.Map
{
    /// <summary>
    /// Envelope for LookupInput.
    /// </summary>
    public sealed class LookupInputEnvelope<Key,Value> : ILookupInput<Key,Value>
    {
        private readonly Func<ILookupInput<Key, Value>> origin;

        /// <summary>
        /// Envelope for LookupInput.
        /// </summary>
        public LookupInputEnvelope(Func<ILookupInput<Key,Value>> origin)
        {
            this.origin = origin;
        }

        public IMap<Key, Value> Merged(IMap<Key, Value> dict)
        {
            return this.origin().Merged(dict);
        }
    }
}

