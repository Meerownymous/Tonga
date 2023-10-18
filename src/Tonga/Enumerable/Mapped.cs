

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
        private readonly IEnumerable<Out> result;

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
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(Func<In, Out> fnc, params In[] src) : this(
            (source, index) => fnc.Invoke(source),
            EnumerableOf.Pipe(src)
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(Func<In, Out> fnc, IEnumerable<In> src) : this(
            (source, index) => fnc.Invoke(source),
            src
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
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IBiFunc{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IBiFunc<In, int, Out> fnc, IEnumerable<In> src) : this(
            (source, index) => fnc.Invoke(source, index),
            src
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        /// <param name="live">live or sticky</param>
        public Mapped(IFunc<In, Out> fnc, IEnumerable<In> src) : this(
            (In1, In2) => fnc.Invoke(In1),
            src
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IBiFunc{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        /// <param name="live">live or sticky</param>
        public Mapped(Func<In, int, Out> fnc, IEnumerable<In> src)
        {
            this.result = EnumerableOf.Pipe(() => Produced(src, fnc));
        }

        public IEnumerator<Out> GetEnumerator()
        {
            return this.result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private static IEnumerator<Out> Produced(IEnumerable<In> source, Func<In, int, Out> mapping)
        {
            var index = 0;
            foreach(var item in source)
            {
                yield return mapping(item, index++);
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
        public static IEnumerable<Out> Pipe<In, Out>(IFunc<In, Out> fnc, params In[] src) => new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IBiFunc{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> Pipe<In, Out>(IBiFunc<In, int, Out> fnc, params In[] src) => new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> Pipe<In, Out>(Func<In, Out> fnc, IEnumerable<In> src) => new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> Pipe<In, Out>(Func<In, int, Out> fnc, IEnumerable<In> src) => new Mapped<In, Out> (fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> Pipe<In, Out>(IFunc<In, Out> fnc, IEnumerable<In> src) => new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IBiFunc{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> Pipe<In, Out>(IBiFunc<In, int, Out> fnc, IEnumerable<In> src) => new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> Sticky<In, Out>(IFunc<In, Out> fnc, params In[] src) =>
            Enumerable.Sticky.New(new Mapped<In, Out>(fnc, src));

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IBiFunc{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> Sticky<In, Out>(IBiFunc<In, int, Out> fnc, params In[] src) =>
            Enumerable.Sticky.New(new Mapped<In, Out>(fnc, src));

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> Sticky<In, Out>(Func<In, Out> fnc, IEnumerable<In> src) =>
            Enumerable.Sticky.New(new Mapped<In, Out>(fnc, src));

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> Sticky<In, Out>(Func<In, int, Out> fnc, IEnumerable<In> src) =>
            Enumerable.Sticky.New(new Mapped<In, Out>(fnc, src));

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> Sticky<In, Out>(IFunc<In, Out> fnc, IEnumerable<In> src) =>
            Enumerable.Sticky.New(new Mapped<In, Out>(fnc, src));

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IBiFunc{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> Sticky<In, Out>(IBiFunc<In, int, Out> fnc, IEnumerable<In> src) =>
            Enumerable.Sticky.New(new Mapped<In, Out>(fnc, src));
    }
}
