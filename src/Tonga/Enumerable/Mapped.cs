

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
    public sealed class Mapped<In, Out>(Func<In, int, Out> fnc, IEnumerable<In> src) : IEnumerable<Out>
    {
        private readonly IEnumerable<Out> result =
            new AsEnumerable<Out>(() => Produced(src, fnc));

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IFunc<In, Out> fnc, params In[] src) : this(
            fnc,
            AsEnumerable._(src)
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(Func<In, Out> fnc, params In[] src) : this(
            (source, _) => fnc.Invoke(source),
            AsEnumerable._(src)
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(Func<In, Out> fnc, IEnumerable<In> src) : this(
            (source, _) => fnc.Invoke(source),
            src
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see>
        ///     <cref>IBiFunc{In, Index, Out}</cref>
        /// </see>
        /// function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IFunc<In, int, Out> fnc, params In[] src) : this(
            fnc,
            AsEnumerable._(src)
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see>
        ///     <cref>IBiFunc{In, Index, Out}</cref>
        /// </see>
        /// function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IFunc<In, int, Out> fnc, IEnumerable<In> src) : this(
            fnc.Invoke,
            src
        )
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IFunc<In, Out> fnc, IEnumerable<In> src) : this(
            (In1, _) => fnc.Invoke(In1),
            src
        )
        { }

        public IEnumerator<Out> GetEnumerator() => result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

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
        public static IEnumerable<Out> _<In, Out>(IFunc<In, Out> fnc, params In[] src) =>
            new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see>
        ///     <cref>IBiFunc{In, Index, Out}</cref>
        /// </see>
        /// function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> _<In, Out>(IFunc<In, int, Out> fnc, params In[] src) =>
            new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> _<In, Out>(Func<In, Out> fnc, IEnumerable<In> src) =>
            new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> _<In, Out>(Func<In, int, Out> fnc, IEnumerable<In> src) =>
            new Mapped<In, Out> (fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> _<In, Out>(IFunc<In, Out> fnc, IEnumerable<In> src) =>
            new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see>
        ///     <cref>IBiFunc{In, Index, Out}</cref>
        /// </see>
        /// function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> _<In, Out>(IFunc<In, int, Out> fnc, IEnumerable<In> src) =>
            new Mapped<In, Out>(fnc, src);
    }

        /// <summary>
    /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given function.
    /// </summary>
    public static class MappedSmarts
    {
        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        public static IEnumerable<Out> AsMapped<In, Out>(this In[] src, IFunc<In, Out> fnc) =>
            new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see>
        ///     <cref>IBiFunc{In, Index, Out}</cref>
        /// </see>
        /// function with index.
        /// </summary>
        public static IEnumerable<Out> AsMapped<In, Out>(this In[] src, IFunc<In, int, Out> fnc) =>
            new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Out}"/> function.
        /// </summary>
        public static IEnumerable<Out> AsMapped<In, Out>(this IEnumerable<In> src, Func<In, Out> fnc) =>
            new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Index, Out}"/> function with index.
        /// </summary>
        public static IEnumerable<Out> AsMapped<In, Out>(this IEnumerable<In> src, Func<In, int, Out> fnc) =>
            new Mapped<In, Out> (fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> AsMapped<In, Out>(this IEnumerable<In> src, IFunc<In, Out> fnc) =>
            new Mapped<In, Out>(fnc, src);

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see>
        ///     <cref>IBiFunc{In, Index, Out}</cref>
        /// </see>
        /// function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public static IEnumerable<Out> AsMapped<In, Out>(this IEnumerable<In> src, IFunc<In, int, Out> fnc) =>
            new Mapped<In, Out>(fnc, src);
    }
}
