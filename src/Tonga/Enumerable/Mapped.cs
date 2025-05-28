

using System;
using System.Collections;
using System.Collections.Generic;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Tonga.Enumerable;

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
    public Mapped(Func<In, Out> fnc, params In[] src) : this(
        (source, _) => fnc.Invoke(source),
        src.AsEnumerable()
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
public static partial class EnumerableSmarts
{
    /// <summary>
    /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
    /// </summary>
    /// <param name="src">enumerable to map</param>
    /// <param name="fnc">function used to map</param>
    public static IEnumerable<Out> AsMapped<In, Out>(this In[] src, Func<In, Out> fnc) =>
        new Mapped<In, Out>(fnc, src);

    /// <summary>
    /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see>
    ///     <cref>IBiFunc{In, Index, Out}</cref>
    /// </see>
    /// function with index.
    /// </summary>
    /// <param name="src">enumerable to map</param>
    /// <param name="fnc">function used to map</param>
    public static IEnumerable<Out> AsMapped<In, Out>(this In[] src, Func<In, int, Out> fnc) =>
        new Mapped<In, Out>(fnc, src);

    /// <summary>
    /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Out}"/> function.
    /// </summary>
    /// <param name="src">enumerable to map</param>
    /// <param name="fnc">function used to map</param>
    public static IEnumerable<Out> AsMapped<In, Out>(this IEnumerable<In> src, Func<In, Out> fnc) =>
        new Mapped<In, Out>(fnc, src);

    /// <summary>
    /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Index, Out}"/> function with index.
    /// </summary>
    /// <param name="src">enumerable to map</param>
    /// <param name="fnc">function used to map</param>
    public static IEnumerable<Out> AsMapped<In, Out>(this IEnumerable<In> src, Func<In, int, Out> fnc) =>
        new Mapped<In, Out> (fnc, src);
}
