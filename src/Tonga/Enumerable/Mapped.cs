

using System;
using System.Collections;
using System.Collections.Generic;
using Tonga.Func;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Tonga.Enumerable
{
    /// <summary>
    /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given function.
    /// </summary>
    /// <typeparam name="In">type of input elements</typeparam>
    /// <typeparam name="Out">type of mapped elements</typeparam>
    public sealed class Mapped<In, Out> : IEnumerable<Out>
    {
        private readonly IBiFunc<In, int, Out> mapping;
        private readonly IEnumerable<In> src;
        private readonly Ternary<Out> result;

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IFunc<In, Out> fnc, params In[] src) : this(
            fnc,
            EnumerableOf.Pipe(src)
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IBiFunc{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IBiFunc<In, int, Out> fnc, params In[] src) : this(
            fnc,
            EnumerableOf.Pipe(src)
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(Func<In, Out> fnc, IEnumerable<In> src) : this(
            fnc,
            src,
            false
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(Func<In, int, Out> fnc, IEnumerable<In> src) : this(
            fnc,
            src,
            false
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IFunc<In, Out> fnc, IEnumerable<In> src) : this(
            fnc,
            src,
            false
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IBiFunc{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IBiFunc<In, int, Out> fnc, IEnumerable<In> src) : this(
            fnc,
            src,
            false
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        /// <param name="live">live or sticky</param>
        public Mapped(Func<In, Out> fnc, IEnumerable<In> src, bool live) : this(
            (In1, In2) => fnc.Invoke(In1),
            src,
            live
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        /// <param name="live">live or sticky</param>
        public Mapped(Func<In, int, Out> fnc, IEnumerable<In> src, bool live) : this(
            new BiFuncOf<In, int, Out>(fnc),
            src,
            live
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        /// <param name="live">live or sticky</param>
        public Mapped(IFunc<In, Out> fnc, IEnumerable<In> src, bool live) : this(
            new BiFuncOf<In, int, Out>((In1, In2) =>
                fnc.Invoke(In1)
            ),
            src,
            live
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IBiFunc{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        /// <param name="live">live or sticky</param>
        public Mapped(IBiFunc<In, int, Out> fnc, IEnumerable<In> src, bool live)
        {
            this.mapping = fnc;
            this.src = src;
            this.result =
                Ternary.Pipe(
                    EnumerableOf.Pipe(Produced),
                    Sticky.New(Produced),
                    live
                );
        }

        public IEnumerator<Out> GetEnumerator()
        {
            return this.result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerator<Out> Produced()
        {
            var index = 0;
            foreach(var item in this.src)
            {
                yield return this.mapping.Invoke(item, index++);
            }
        }
    }

    /// <summary>
    /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given function.
    /// </summary>
    public static class Mapped
    {
        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> New<In, Out>(IFunc<In, Out> fnc, params In[] src) => new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IBiFunc{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> New<In, Out>(IBiFunc<In, int, Out> fnc, params In[] src) => new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> New<In, Out>(Func<In, Out> fnc, IEnumerable<In> src, bool live = false) => new Mapped<In, Out>(fnc, src, live);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> New<In, Out>(Func<In, int, Out> fnc, IEnumerable<In> src, bool live = false) => new Mapped<In, Out> (fnc, src, live);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> New<In, Out>(IFunc<In, Out> fnc, IEnumerable<In> src, bool live = false) => new Mapped<In, Out>(fnc, src, live);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IBiFunc{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> New<In, Out>(IBiFunc<In, int, Out> fnc, IEnumerable<In> src, bool live = false) => new Mapped<In, Out>(fnc, src, live);
    }
}
