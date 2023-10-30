

using System.Collections.Generic;
using System.Linq;
using Tonga.Enumerable;

namespace Tonga.Collection
{
    /// Reversed collection.
    ///
    /// <para>There is no thread-safety guarantee.</para>
    ///
    public class Reversed<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src"></param>
        public Reversed(params T[] src) : this(Enumerable.AsEnumerable._(src))
        { }
        public static Reversed<T> New(params T[] src) => new Reversed<T>(src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source collection</param>
        public Reversed(IEnumerable<T> src) : this(new LiveCollection<T>(src))
        { }
        public static Reversed<T> New(IEnumerable<T> src) => new Reversed<T>(src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source collection</param>
        public Reversed(ICollection<T> src) : base(
            () => new LiveCollection<T>(
                    new LinkedList<T>(src).Reverse()
            )
        )
        { }
        public static Reversed<T> New(ICollection<T> src) => new Reversed<T>(src);
    }

    /// Reversed collection.
    public static class Reversed
    {
        public static ICollection<T> _<T>(params T[] src) => new Reversed<T>(src);

        public static ICollection<T> _<T>(IEnumerable<T> src) => new Reversed<T>(src);

        public static ICollection<T> _<T>(ICollection<T> src) => new Reversed<T>(src);
    }
}
