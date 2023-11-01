

using System.Collections.Generic;
using System.Linq;
using Tonga.Enumerable;

namespace Tonga.Collection
{
    /// <summary>
    /// Reversed collection
    /// </summary>
    public class Reversed<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// Reversed collection
        /// </summary>
        public Reversed(params T[] src) : this(AsEnumerable._(src))
        { }

        /// <summary>
        /// Reversed collection
        /// </summary>
        public Reversed(IEnumerable<T> src) : this(new AsCollection<T>(src))
        { }

        /// <summary>
        /// Reversed collection
        /// </summary>
        public Reversed(ICollection<T> src) : base(
            new AsCollection<T>(() =>
                src.Reverse()
            )
        )
        { }
    }

    /// <summary>
    /// Reversed collection
    /// </summary>
    public static class Reversed
    {
        /// <summary>
        /// Reversed collection
        /// </summary>
        public static ICollection<T> _<T>(params T[] src) => new Reversed<T>(src);

        /// <summary>
        /// Reversed collection
        /// </summary>
        public static ICollection<T> _<T>(IEnumerable<T> src) => new Reversed<T>(src);

        /// <summary>
        /// Reversed collection
        /// </summary>
        public static ICollection<T> _<T>(ICollection<T> src) => new Reversed<T>(src);
    }
}
