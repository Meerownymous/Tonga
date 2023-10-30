using System;

namespace Tonga.Map
{
    /// <summary>
    /// Pair that allows retrieving its value only one time.
    /// Useful for testing of sticky objects.
    /// </summary>
    public sealed class OneTimePair<K, V> : IPair<K, V>
    {
        private readonly IPair<K, V> origin;
        private int accesses;

        /// <summary>
        /// Pair that allows retrieving its value only one time.
        /// Useful for testing of sticky objects.
        /// </summary>
        public OneTimePair(IPair<K, V> origin)
        {
            this.accesses = 0;
            this.origin = origin;
        }

        public bool IsLazy() => this.origin.IsLazy();

        public K Key()
        {
            return this.origin.Key();
        }

        public V Value()
        {
            this.accesses++;
            if (this.accesses > 1)
                throw new InvalidOperationException("Value of this pair is allowed to only retrieve once, and it has already been retrieved.");

            return this.origin.Value();
        }
    }

    public static class OneTimePair
    {
        /// <summary>
        /// Pair that allows retrieving its value only one time.
        /// Useful for testing of sticky objects.
        /// </summary>
        public static OneTimePair<Key, Value> _<Key, Value>(IPair<Key, Value> origin) =>
            new OneTimePair<Key, Value>(origin);
    }
}

