namespace Tonga.Map
{
    /// <summary>
    /// Envelope for LookupInput.
    /// </summary>
    public sealed class MapInputEnvelope<Key,Value> : IMapInput<Key,Value>
    {
        private readonly IMapInput<Key, Value> origin;

        /// <summary>
        /// Envelope for LookupInput.
        /// </summary>
        public MapInputEnvelope(IMapInput<Key,Value> origin)
        {
            this.origin = origin;
        }

        public IMap<Key, Value> Merged(IMap<Key, Value> dict)
        {
            return this.origin.Merged(dict);
        }

        public IMapInput<Key, Value> Self()
        {
            return this;
        }
    }
}

