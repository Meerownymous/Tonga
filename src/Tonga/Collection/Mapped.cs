

using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Collection
{
    /// <summary>
    /// A collection which is mapped to the output type.
    /// </summary>
    /// <typeparam name="In">source type</typeparam>
    /// <typeparam name="Out">target type</typeparam>
    public sealed class Mapped<In, Out> : CollectionEnvelope<Out>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source items</param>
        public Mapped(Func<In, Out> mapping, params In[] src) : this(mapping, EnumerableOf.Pipe(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source enumerator</param>
        public Mapped(Func<In, Out> mapping, IEnumerator<In> src) : this(
            mapping, Enumerable.EnumerableOf.Pipe(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source enumerable</param>
        public Mapped(Func<In, Out> mapping, IEnumerable<In> src) : this(
            mapping, new LiveCollection<In>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source collection</param>
        public Mapped(Func<In, Out> mapping, ICollection<In> src) : base(() =>
            new Enumerable.Mapped<In, Out>(mapping, src).GetEnumerator(),
            false
        )
        { }
    }

    // <summary>
    /// A collection which is mapped to the output type.
    /// </summary>
    /// <typeparam name="In">source type</typeparam>
    /// <typeparam name="Out">target type</typeparam>
    public static class Mapped
    {
        public static ICollection<Out> New<In, Out>(Func<In, Out> mapping, params In[] src) => new Mapped<In, Out>(mapping, src);

        public static ICollection<Out> New<In, Out>(Func<In, Out> mapping, IEnumerator<In> src) => new Mapped<In, Out>(mapping, src);

        public static ICollection<Out> New<In, Out>(Func<In, Out> mapping, IEnumerable<In> src) => new Mapped<In, Out>(mapping, src);

        public static ICollection<Out> New<In, Out>(Func<In, Out> mapping, ICollection<In> src) => new Mapped<In, Out>(mapping, src);
    }
}
