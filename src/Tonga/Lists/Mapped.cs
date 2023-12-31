using System;
using System.Collections.Generic;

namespace Tonga.List
{
    /// <summary>
    /// Mapped list
    /// </summary>
    /// <typeparam name="In">Type of source items</typeparam>
    /// <typeparam name="Out">Type of target items</typeparam>
    public sealed class Mapped<In, Out> : ListEnvelope<Out>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source enumerator</param>
        public Mapped(IFunc<In, Out> mapping, IEnumerable<In> src) : this(mapping.Invoke, src)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source enumerator</param>
        public Mapped(Func<In, Out> mapping, IEnumerable<In> src) : base(
            AsList._(
                Collection.Mapped._(mapping, src)
            )
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source enumerator</param>
        public Mapped(Func<In, Out> mapping, ICollection<In> src) : base(
            AsList._(
                Enumerable.Mapped._(mapping, src)
            )
        )
        { }
    }

    public abstract class Mapped
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source enumerator</param>
        public static IList<Out> _<In, Out>(IFunc<In, Out> mapping, IEnumerable<In> src)
            => new Mapped<In, Out>(mapping, src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source enumerator</param>
        public static IList<Out> _<In, Out>(Func<In, Out> mapping, IEnumerable<In> src)
            => new Mapped<In, Out>(mapping, src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source enumerator</param>
        public static IList<Out> _<In, Out>(Func<In, Out> mapping, ICollection<In> src)
            => new Mapped<In, Out>(mapping, src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source enumerator</param>
        public static IList<Out> Sticky<In, Out>(IFunc<In, Out> mapping, IEnumerable<In> src)
            => List.Sticky._(new Mapped<In, Out>(mapping, src));

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source enumerator</param>
        public static IList<Out> Sticky<In, Out>(Func<In, Out> mapping, IEnumerable<In> src)
            => List.Sticky._(new Mapped<In, Out>(mapping, src));

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source enumerator</param>
        public static IList<Out> Sticky<In, Out>(Func<In, Out> mapping, ICollection<In> src)
            => List.Sticky._(new Mapped<In, Out>(mapping, src));
    }
}
