

using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Collection
{
    /// A <see cref="ICollection{T}"/> that is both synchronized and sticky.
    ///
    /// <para>Objects of this class are thread-safe.</para>
    ///
    public sealed class Solid<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="array">source items</param>
        public Solid(params T[] items) : this(new AsEnumerable<T>(items))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerator</param>
        public Solid(IEnumerator<T> src) : this(Enumerable.AsEnumerable._(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerable</param>
        public Solid(IEnumerable<T> src) : this(new LiveCollection<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source collection</param>
        public Solid(ICollection<T> src) : base(() =>
            new Sync<T>(
                src
            )
        )
        { }
    }

    /// A <see cref="ICollection{T}"/> that is both synchronized and sticky.
    ///
    /// <para>Objects of this class are thread-safe.</para>
    ///
    public static class Solid
    {
        public static ICollection<T> _<T>(params T[] array) => new Solid<T>(array);

        public static ICollection<T> _<T>(IEnumerator<T> src) => new Solid<T>(src);

        public static ICollection<T> _<T>(IEnumerable<T> src) => new Solid<T>(src);

        public static ICollection<T> _<T>(ICollection<T> src) => new Solid<T>(src);
    }
}
