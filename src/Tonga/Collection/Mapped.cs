using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tonga.Enumerable;
using Tonga.List;

namespace Tonga.Collection
{
    /// <summary>
    /// A collection which is mapped to the output type.
    /// </summary>
    public sealed class Mapped<In, Out> : CollectionEnvelope<Out>
    {
        /// <summary>
        /// A collection which is mapped to the output type.
        /// </summary>
        public Mapped(Func<In, Out> mapping, params In[] src) : this(mapping, AsEnumerable._(src))
        { }

        /// <summary>
        /// A collection which is mapped to the output type.
        /// </summary>
        public Mapped(Func<In, Out> mapping, IEnumerator<In> src) : this(
            mapping, AsEnumerable._(src))
        { }

        /// <summary>
        /// A collection which is mapped to the output type.
        /// </summary>
        public Mapped(Func<In, Out> mapping, IEnumerable<In> src) : this(
            mapping, new AsCollection<In>(src))
        { }

        /// <summary>
        /// A collection which is mapped to the output type.
        /// </summary>
        public Mapped(Func<In, Out> mapping, ICollection<In> src) : base(
            AsCollection._(
                Enumerable.Mapped._(mapping, src)
            )
        )
        { }
    }

    /// <summary>
    /// A collection which is mapped to the output type.
    /// </summary>
    public static class Mapped
    {
        /// <summary>
        /// A collection which is mapped to the output type.
        /// </summary>
        public static ICollection<Out> _<In, Out>(Func<In, Out> mapping, params In[] src) => new Mapped<In, Out>(mapping, src);

        /// <summary>
        /// A collection which is mapped to the output type.
        /// </summary>
        public static ICollection<Out> _<In, Out>(Func<In, Out> mapping, IEnumerator<In> src) => new Mapped<In, Out>(mapping, src);

        /// <summary>
        /// A collection which is mapped to the output type.
        /// </summary>
        public static ICollection<Out> _<In, Out>(Func<In, Out> mapping, IEnumerable<In> src) => new Mapped<In, Out>(mapping, src);

        /// <summary>
        /// A collection which is mapped to the output type.
        /// </summary>
        public static ICollection<Out> _<In, Out>(Func<In, Out> mapping, ICollection<In> src) => new Mapped<In, Out>(mapping, src);
    }
}
