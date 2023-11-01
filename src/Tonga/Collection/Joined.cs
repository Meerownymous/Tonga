

using System.Collections.Generic;

namespace Tonga.Collection
{
    /// <summary>
    /// Joins collections together as one.
    /// </summary>
    public sealed class Joined<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// Joins collections together as one.
        /// </summary>
        public Joined(params IEnumerable<T>[] list) : base(
            AsCollection._(
                Enumerable.Joined._(list)
            )
        )
        { }
    }

    /// <summary>
    /// Joins collections together as one.
    /// </summary>
    public static class Joined
    {
        /// <summary>
        /// Joins collections together as one.
        /// </summary>
        public static ICollection<T> _<T>(params IEnumerable<T>[] list) => new Joined<T>(list);
    }
}
