

using System.Collections.Generic;

namespace Tonga.Collection
{
    /// <summary>
    /// Joins collections together as one.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Joined<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">List of collections to join together</param>
        public Joined(params IEnumerable<T>[] list) : base(
            () => new LiveCollection<T>(
                    new Enumerable.Joined<T>(list)
            )
        )
        { }
    }

    public static class Joined
    {
        public static ICollection<T> _<T>(params IEnumerable<T>[] list) => new Joined<T>(list);
    }
}
